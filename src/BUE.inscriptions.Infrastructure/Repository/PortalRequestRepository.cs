using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using BUE.Inscriptions.Domain.Entity.DTO.storeProcedures;
using System.Data;
using BUE.Inscriptions.Domain.Paging;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Net.Mail;
using Newtonsoft.Json;
using System.Reflection;
using BUE.Inscriptions.Domain.Request;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class PortalRequestRepository : BaseRepository, IPortalRequestRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly BueDBContext _dbBue;

        private IMapper _mapper;
        protected readonly IConfiguration _configuration;
        private readonly IAwsS3UploaderRepository _awsS3;
        private readonly IStudentRepresentativeRepository _studentRepresentatives;


        public PortalRequestRepository(IStudentRepresentativeRepository studentRepresentatives, IAwsS3UploaderRepository awsS3, PortalMatriculasDBContext db, IMapper mapper, IConfiguration configuration, BueDBContext dbBue)
        {
            _db = db;
            _mapper = mapper;
            _configuration = configuration;
            _awsS3 = awsS3;
            _dbBue = dbBue;
            _studentRepresentatives = studentRepresentatives;
        }
        #region PORTAL REQUEST V1
        public async Task<PortalRequestDTO> CreateAsync(PortalRequestDTO entity)
        {
            PortalRequest request = _mapper.Map<PortalRequestDTO, PortalRequest>(entity);
            _db.PortalRequests.Add(request);
            await _db.SaveChangesAsync();
            try
            {
                await AttachAuthPeople((int)entity.userCode, (int)request.code, (int)request.studentCodeSchoolYear, (int)request.currentSchoolYear);
            }
            catch (Exception) { }
            return _mapper.Map<PortalRequest, PortalRequestDTO>(request);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                PortalRequest? request = await _db.PortalRequests.FirstOrDefaultAsync(x => x.code == id);
                if (request is null)
                {
                    return false;
                }
                request.DeletedAt = DateTime.Now;
                _db.PortalRequests.Update(request);
                //_db.Role.Remove(request);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<PortalRequestDTO>> GetBuilderAsync(string search)
        {
            var courses = await _db.CourseGrades.ToListAsync();

            var levels = await _db.Levels.ToListAsync();
            int? status = 0;
            int? currentYearSchool = 9;
            string? terms = "";
            if (search is not null)
            {
                var filters = search.Split('&');
                foreach (var filter in filters)
                {
                    var filterValues = filter.Split('=');
                    if (filterValues[0].Contains("currentYearSchool"))
                    {
                        currentYearSchool = Convert.ToInt32(filterValues[1]);
                    }
                    if (filterValues[0].Contains("status"))
                    {
                        status = Convert.ToInt32(filterValues[1]);
                    }
                    if (filterValues[0].Contains("search"))
                    {
                        terms = filterValues[1];
                    }

                }
            }

            var requestsQuery = _db.PortalRequests
               .Include(p => p.FirstContact)
               .Include(p => p.SecondContact)
               .Include(p => p.StudentPortal)
               .Where(x => x.currentSchoolYear == currentYearSchool);
            if (!string.IsNullOrEmpty(terms))
            {
                requestsQuery = requestsQuery.Where(x =>
                    x.StudentPortal.completeName.Contains(terms) ||
                    x.StudentPortal.documentNumber.Contains(terms));
            }
            if (status > 0)
                requestsQuery = requestsQuery.Where(x => x.requestStatus == status);


            var requests = await requestsQuery
             .OrderBy(x => x.StudentPortal.completeName)
             .ToListAsync();

            foreach (var item in requests)
            {
                if (item.StudentPortal is not null)
                {
                    if (item.StudentPortal.levelCode is not null)
                    {
                        var currenLevel = levels.FirstOrDefault((x) => x.Code == item.StudentPortal.levelCode);
                        item.StudentPortal.level = currenLevel;
                    }
                    if (item.StudentPortal.courseGradeCode is not null)
                    {
                        var currentCourse = courses.FirstOrDefault((x) => x.Code == item.StudentPortal.courseGradeCode);
                        item.StudentPortal.courseGrade = currentCourse;
                    }
                    item.StudentPortal.dataEnrollmentApp = null;
                }
            }
            var response = _mapper.Map<IEnumerable<PortalRequestDTO>>(requests);
            return response;
        }

        public async Task<IEnumerable<PortalRequestDTO>> GetWithRepresentativeAsync(string search)
        {
            int? status = 1;
            int? currentYearSchool = 8;
            string? terms = "";
            if (search is not null)
            {
                var filters = search.Split('&');
                foreach (var filter in filters)
                {
                    var filterValues = filter.Split('=');
                    if (filterValues[0] == "currentYearSchool")
                    {
                        currentYearSchool = Convert.ToInt32(filterValues[1]);
                    }
                    if (filterValues[0] == "status")
                    {
                        status = Convert.ToInt32(filterValues[1]);
                    }
                    if (filterValues[0] == "search")
                    {
                        terms = filterValues[1];
                    }
                }
            }
            var resuests = await _db.PortalRequests
            .Include(p => p.StudentPortal)
            .ThenInclude(st => st.courseGrade)
            .ThenInclude(cg => cg.level)
            .Include(p => p.FirstContact)
            .Include(p => p.SecondContact)
            .Where(x => x.contactCodeFirst != null && x.statusFirstContact == status && x.currentSchoolYear == currentYearSchool && (x.StudentPortal.completeName.Contains(terms) || x.StudentPortal.documentNumber.Contains(terms)))
            .ToListAsync();

            var response = _mapper.Map<IEnumerable<PortalRequestDTO>>(resuests);
            return response;
        }
        public async Task<IEnumerable<PortalRequestDTO>> GetWithSecondRepresentativeAsync(string search)
        {
            int? status = 1;
            int? currentYearSchool = 8;
            string? terms = "";
            if (search is not null)
            {
                var filters = search.Split('&');
                foreach (var filter in filters)
                {
                    var filterValues = filter.Split('=');
                    if (filterValues[0] == "currentYearSchool")
                    {
                        currentYearSchool = Convert.ToInt32(filterValues[1]);
                    }
                    if (filterValues[0] == "status")
                    {
                        status = Convert.ToInt32(filterValues[1]);
                    }
                    if (filterValues[0] == "search")
                    {
                        terms = filterValues[1];
                    }

                }
            }
            var resuests = await _db.PortalRequests
            .Include(p => p.SecondContact)
            .Where(x => x.contactCodeSecond != null && x.statusSecondContact == status && x.currentSchoolYear == currentYearSchool && (x.StudentPortal.completeName.Contains(terms) || x.StudentPortal.documentNumber.Contains(terms)))
            .ToListAsync();
            var response = _mapper.Map<IEnumerable<PortalRequestDTO>>(resuests);
            return response;
        }
        public async Task<IEnumerable<PortalRequestDTO>> GetAsync()
        {

            var resuests = await _db.PortalRequests
            .Include(p => p.StudentPortal)
            .ThenInclude(st => st.courseGrade)
            .ThenInclude(cg => cg.level)
            .ToListAsync();
            var response = _mapper.Map<IEnumerable<PortalRequestDTO>>(resuests);
            return response;
        }

        public async Task<IEnumerable<PortalRequestDTO>> GetByFilterServiceAsync(int status = 1, int currentYearSchool = 1, PagingQueryParameters parameters = null)
        {
            try
            {
                string terms = "";
                int courseGrade = 0;
                if (parameters is not null)
                {
                    terms = parameters.search != null ? parameters.search : "";
                    courseGrade = parameters.courseCode is not null ? (int)parameters.courseCode : 0;
                }
                var resuests = await _db.PortalRequests
                .Include(p => p.StudentPortal)
                .ThenInclude(st => st.courseGrade)
                .ThenInclude(cg => cg.level)
                .Include(p => p.FirstContact)
               .Where(x => x.photoStatus == status && x.currentSchoolYear == currentYearSchool
                //&& ( x.StudentPortal.completeName.Normalize(NormalizationForm.FormD).IndexOf(terms.Normalize(NormalizationForm.FormD), StringComparison.OrdinalIgnoreCase) >= 0 ||
                //            x.StudentPortal.documentNumber.Contains(terms))
                && (x.StudentPortal.completeName.Contains(terms) || x.StudentPortal.documentNumber.Contains(terms))
                && (courseGrade == 0 || x.StudentPortal.courseGradeCode == courseGrade)
                )
               .OrderBy(x => x.StudentPortal.completeName)
               .ToListAsync();
                var response = _mapper.Map<IEnumerable<PortalRequestDTO>>(resuests);
                return response;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<IEnumerable<PortalRequestDTO>> GetByUserAsync(int userCode = 1, int currentYearSchool = 1)
        {
            var resuests = await _db.PortalRequests
                            .Include(p => p.StudentPortal)
                            .ThenInclude(st => st.courseGrade)
                            .ThenInclude(cg => cg.level)
                            .Include(p => p.FirstContact)
                            .Include(p => p.SecondContact)
                            .Where(x => x.userCode == userCode && x.currentSchoolYear == currentYearSchool).ToListAsync();
            var response = _mapper.Map<IEnumerable<PortalRequestDTO>>(resuests);
            return response;
        }
        public async Task<IEnumerable<PortalRequestDTO>> GetStudentsByCodeAsync(IEnumerable<HelperFieldValidate> helperFields)
        {
            var codes = helperFields.Select(x => x.Code).ToList();
            var codesText = helperFields.Select(x => x.CodeText.Trim()).ToList();
            var rquestCodesAndIdentifications = await _db.PortalRequests.Include(p => p.StudentPortal)
                            .ThenInclude(st => st.courseGrade)
                            .ThenInclude(cg => cg.level)
                            .Include(p => p.FirstContact)
                            .Include(p => p.SecondContact)
                            .Where(request =>
                            (codes.Contains(request.StudentPortal != null ? request.StudentPortal.studentCode : null) ||
                             codesText.Contains(request.StudentPortal != null ? request.StudentPortal.documentNumber : null))
                            ).ToListAsync();
            var resultDTO = _mapper.Map<IEnumerable<PortalRequestDTO>>(rquestCodesAndIdentifications);
            return resultDTO;
        }


        public async Task<PortalRequestDTO> GetByIdAsync(int id)
        {
            var request = await _db.PortalRequests
            .Include(p => p.StudentPortal)
            .ThenInclude(st => st.courseGrade)
            .ThenInclude(cg => cg.level)
            .Include(p => p.FirstContact)
            .Include(p => p.SecondContact)
            .Include(p => p.FinanceInformation)
            .Include(p => p.AuthorizePeople)
            //.Include(p => p.MedicalRecord)
            .FirstOrDefaultAsync(x => x.code == id);
            List<AuthorizePeople> tempAuthorizePeople = new List<AuthorizePeople>();
            var financeInformation = await _db.FinanceInformation.FirstOrDefaultAsync(x => x.requestCode == request.code);
            if (financeInformation != null) financeInformation.PortalRequest = null;
            var medicalRecord = await _db.MedicalRecords.FirstOrDefaultAsync(x => x.RequestId == request.code);
            if (medicalRecord != null) medicalRecord.PortalRequest = null;

            foreach (var item in request.AuthorizePeople)
            {
                item.photo = null;
                item.documentFile = null;
                tempAuthorizePeople.Add(item);
            }
            request.AuthorizePeople = tempAuthorizePeople;
            request.FinanceInformation = financeInformation;
            request.MedicalRecord = medicalRecord;

            var requestDTO = _mapper.Map<PortalRequestDTO>(request);
            return requestDTO;

        }

        public async Task<PortalRequestDTO> UpdateAsync(int id, PortalRequestDTO entity)
        {
            PortalRequest request = _mapper.Map<PortalRequestDTO, PortalRequest>(entity);
            var requestAfter = await _db.PortalRequests.AsNoTracking().FirstOrDefaultAsync(x => x.code == id);
            if (requestAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            if (entity.FileJustification is not null && (entity.FileJustification.Length > 0))
            {
                string subPathDocS3 = String.Format(@"files/justifications");
                string nameFile = requestAfter.code + "_file_justification" + ".pdf";
                string s3Result = await _awsS3.UploadBucketFileAsync(subPathDocS3, nameFile, entity.FileJustification);
                request.UrlFileJustification = s3Result;
            }
            var requestMapper = MapProperties(request, requestAfter);
            _db.PortalRequests.Update(requestMapper);
            await _db.SaveChangesAsync();
            return _mapper.Map<PortalRequest, PortalRequestDTO>(requestMapper);
        }
        public async Task<PortalRequestDTO> UpdateUrlReportCompleteAsync(int id, string urlComplete)
        {

            var requestAfter = await _db.PortalRequests.AsNoTracking().FirstOrDefaultAsync(x => x.code == id);
            if (requestAfter is null)
            {
                throw new NullReferenceException(MessageUtil.Instance.NotFound);
            }
            requestAfter.UrlReportComplete = urlComplete;
            _db.PortalRequests.Update(requestAfter);
            await _db.SaveChangesAsync();
            return _mapper.Map<PortalRequest, PortalRequestDTO>(requestAfter);
        }

        public async Task<IEnumerable<PortalRequestDTO>> UpdateAnyAsync(IEnumerable<PortalRequestDTO> entities)
        {
            List<PortalRequestDTO> list = new List<PortalRequestDTO>();
            foreach (var item in entities)
            {
                PortalRequest request = _mapper.Map<PortalRequestDTO, PortalRequest>(item);
                var requestAfter = await _db.PortalRequests.AsNoTracking().FirstOrDefaultAsync(x => x.code == request.code);
                if (requestAfter is null)
                {
                    continue;
                }
                var requestMapper = MapProperties(request, requestAfter);
                _db.PortalRequests.Update(requestMapper);
                await _db.SaveChangesAsync();
                list.Add(_mapper.Map<PortalRequest, PortalRequestDTO>(requestMapper));
            }
            return list;
        }
        public async Task<IEnumerable<PortalRequestDTO>> UpdateStatusAnyAsync(IEnumerable<PortalRequestDTO> entities)
        {
            var updatedRequests = new List<PortalRequestDTO>();
            try
            {
                var requestIds = entities.Select(e => (int)e.code).ToList();
                var existingRequests = await _db.PortalRequests
                    .Where(x => requestIds.Contains((int)x.code))
                    .ToListAsync();
                foreach (var entity in entities)
                {
                    var requestId = (int)entity.code;
                    var existingRequest = existingRequests.FirstOrDefault(x => x.code == requestId);
                    if (existingRequest != null)
                    {
                        entity.FirstContact = null;
                        entity.SecondContact = null;
                        entity.StudentPortal = null;
                        entity.MedicalRecord = null;
                        entity.AuthorizePeople = null;
                        var request = _mapper.Map<PortalRequestDTO, PortalRequest>(entity);
                        request.requestBeforeStatus = existingRequest.requestStatus;
                        var requestMapper = MapProperties(request, existingRequest);
                        MapProperties(existingRequest, requestMapper);
                        existingRequest.FirstContact = null;
                        existingRequest.SecondContact = null;
                        existingRequest.StudentPortal = null;
                        existingRequest.MedicalRecord = null;
                        updatedRequests.Add(_mapper.Map<PortalRequest, PortalRequestDTO>(existingRequest));
                    }
                }
                _db.PortalRequests.UpdateRange(existingRequests);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
            }
            return updatedRequests;
        }
        public async Task sendNotificationToRequest(PortalRequestDTO model, IEnumerable<PortalRequestDTO> entities)
        {
            var currentReport = entities.FirstOrDefault(x => x.code == model.code);
            string template = "";

            if (model.requestStatus == 1)
                template = "SINTRATAR";
            if (model.requestStatus == 7)
                template = "ACEPTADA";
            if (model.requestStatus == 14)
                template = "CONFIRMADA";
            if (template != "")
            {
                try
                {
                    var emailNoti = new MailNotificactionDTO()
                    {
                        student = currentReport.StudentPortal.completeName,
                        emails = currentReport.FirstContact.email,
                        lang = "ES",
                        course = currentReport.StudentPortal.courseGrade.Name,
                        schoolYear = currentReport.PortalSchoolYear.name,
                        template = template
                    };

                }
                catch (Exception ex)
                {
                    LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
                }
            }


        }
        public async Task<IEnumerable<MailNotificactionDTO>> BuildSendNotification(IEnumerable<PortalRequestDTO> models)
        {
            List<int> ids = new List<int>();
            List<MailNotificactionDTO> mailsNotificactions = new List<MailNotificactionDTO>();
            foreach (var model in models)
            {
                ids.Add((int)model.code);
            }
            var requestList = await GetListNotificationAsync(ids);
            foreach (var item in requestList)
            {
                string template = "";
                var currentModel = models.FirstOrDefault(x => x.code == item.code);
                if (currentModel.requestStatus == 1)
                    template = "SINTRATAR";
                if (currentModel.requestStatus == 7)
                    template = "ACEPTADA";
                if (currentModel.requestStatus == 14)
                    template = "CONFIRMADA";
                if (currentModel.requestStatus == 5)
                    template = "RECHAZADA";
                if (template != "")
                {
                    try
                    {
                        var emailNoti = new MailNotificactionDTO()
                        {
                            student = item.StudentPortal.completeName,
                            emails = item.FirstContact.email,
                            lang = "ES",
                            course = item.StudentPortal.courseGrade.Name,
                            schoolYear = item.PortalSchoolYear.name,
                            template = template,
                            requestId = $"{item.code}",
                        };
                        mailsNotificactions.Add(emailNoti);
                    }
                    catch (Exception ex)
                    {
                        LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
                    }
                }
            }
            return mailsNotificactions;

        }
        public async Task<IEnumerable<PortalRequestDTO>> UpdateRestoreStatusAnyAsync(IEnumerable<PortalRequestDTO> entities)
        {
            List<PortalRequestDTO> list = new List<PortalRequestDTO>();
            foreach (var item in entities)
            {
                PortalRequest request = _mapper.Map<PortalRequestDTO, PortalRequest>(item);
                var requestAfter = await _db.PortalRequests.AsNoTracking().FirstOrDefaultAsync(x => x.code == request.code);
                if (requestAfter is null)
                {
                    continue;
                }
                request.requestBeforeStatus = requestAfter.requestStatus;
                var requestMapper = MapProperties(request, requestAfter);
                _db.PortalRequests.Update(requestMapper);
                await _db.SaveChangesAsync();
                list.Add(_mapper.Map<PortalRequest, PortalRequestDTO>(requestMapper));
            }
            return list;
        }

        public async Task<IEnumerable<PortalRequestDTO>> UpdateNotesAnyAsync(IEnumerable<PortalRequestDTO> entities)
        {
            List<PortalRequestDTO> list = new List<PortalRequestDTO>();
            foreach (var item in entities)
            {
                PortalRequest request = _mapper.Map<PortalRequestDTO, PortalRequest>(item);
                if (request.additionalInformation != null || request.notes != null)
                {
                    var requestAfter = await _db.PortalRequests.AsNoTracking().FirstOrDefaultAsync(x => x.code == request.code);
                    if (requestAfter is null)
                    {
                        continue;
                    }
                    var requestMapper = MapProperties(request, requestAfter);
                    _db.PortalRequests.Update(requestMapper);
                    await _db.SaveChangesAsync();
                    list.Add(_mapper.Map<PortalRequest, PortalRequestDTO>(requestMapper));
                }
            }
            return list;
        }

        public async Task<int> CreateIntegrationAsync(IntegrationRegisterDTO entity)
        {
            var sql = String.Format("EXECUTE [gmas_RegistrarIntegraciones] {0},'{1}',{2}", entity.currentYearSchool, entity.requestCodes, entity.userCode);
            var res = await _db.Database.ExecuteSqlRawAsync(sql);
            return res;
        }
        public async Task<int> CreateIntegrationFirstContactAsync(IntegrationRegisterDTO entity)
        {
            var sql = String.Format("EXECUTE [gmas_RegistrarIntegracionesContacto] {0},'{1}',{2}", entity.currentYearSchool, entity.requestCodes, entity.userCode);
            var res = await _db.Database.ExecuteSqlRawAsync(sql);
            return res;
        }
        public async Task<int> CreateIntegrationSecondContactAsync(IntegrationRegisterDTO entity)
        {
            var sql = String.Format("EXECUTE [gmas_RegistrarIntegracionesContactoSegundo] {0},'{1}',{2}", entity.currentYearSchool, entity.requestCodes, entity.userCode);
            var res = await _db.Database.ExecuteSqlRawAsync(sql);
            return res;
        }
        public async Task<int> CreateIntegrationAutotizationPeopleAsync(IntegrationRegisterDTO entity)
        {
            var sql = String.Format("EXECUTE [gmas_RegistrarIntegracionesPersonasAutorizadas] {0},'{1}',{2}", entity.currentYearSchool, entity.requestCodes, entity.userCode);
            var res = await _db.Database.ExecuteSqlRawAsync(sql);
            return res;
        }
        public async Task<int> ExecuteIntegrationStudentsAsync(IntegrationRegisterDTO entity)
        {
            var sql = String.Format("EXECUTE [gmas_IntegracionPortalBue] {0}", entity.currentYearSchool);
            var res = await _db.Database.ExecuteSqlRawAsync(sql);
            return res;
        }
        public async Task<int> ExecuteIntegrationContactsAsync(IntegrationRegisterDTO entity)
        {
            var sql = String.Format("EXECUTE [gmas_IntegracionPortalBueContactos] {0}", entity.currentYearSchool);
            var res = await _db.Database.ExecuteSqlRawAsync(sql);
            return res;
        }

        public async Task<int> ExecuteIntegrationAutorizationPeopleAsync(IntegrationRegisterDTO entity)
        {
            var sql = String.Format("EXECUTE [gmas_IntegracionPortalBuePersonasAutorizadas] {0}", entity.currentYearSchool);
            var res = await _db.Database.ExecuteSqlRawAsync(sql);
            return res;
        }


        public async Task<IEnumerable<DashBoardChartDTO>> GetDashboardAsync(IntegrationRegisterDTO entity)
        {
            var res = await _db.DashBoardChart.FromSqlInterpolated($"EXEC [dbo].[gmas_rptDashborad] @codigoAnioLectivo = {entity.currentYearSchool}")
            .AsNoTracking()
            .ToListAsync();
            var response = _mapper.Map<IEnumerable<DashBoardChartDTO>>(res);
            return response;
        }
        public async Task<IEnumerable<PortalRequestDTO>> GetListNotificationAsync(List<int> ids)
        {
            try
            {
                var years = await _db.SchoolYear.AsNoTracking().ToListAsync();
                var courses = await _db.CourseGrades.AsNoTracking().ToListAsync();
                var request = await _db.PortalRequests.AsNoTracking()
                                .Include(p => p.StudentPortal)
                                .Include(p => p.FirstContact)
                                .Where(x => ids.Contains((int)x.code))
                                .ToListAsync();
                foreach (var item in request)
                {
                    item.PortalSchoolYear = years.FirstOrDefault(x => x.code == (int)item.currentSchoolYear);
                    item.StudentPortal.courseGrade = courses.FirstOrDefault(x => x.Code == (int)item.StudentPortal.courseGradeCode);
                }
                var response = _mapper.Map<IEnumerable<PortalRequestDTO>>(request);
                return response;

            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
                return null;
            }
        }

        public async Task AttachAuthPeople(int userId, int requestCode, int studentCodeSchoolYear, int currentYearSchool)
        {
            try
            {
                var request = await _db.PortalRequests
               .Include(p => p.AuthorizePeople)
               .FirstOrDefaultAsync(x => x.userCode == userId && x.code != requestCode && x.currentSchoolYear == currentYearSchool);
                if (request == null) return;

                var authPeople = request.AuthorizePeople;
                if (authPeople == null) return;
                var prePost = _mapper.Map<IEnumerable<AuthorizePeopleDTO>>(authPeople);
                foreach (var item in prePost)
                {
                    item.code = null;
                    item.portalRequestCode = requestCode;
                    item.studentCodeSchoolYear = studentCodeSchoolYear;
                    item.currentSchoolYear = currentYearSchool;
                    //item.PortalRequest = null;
                }
                var commitToPost = _mapper.Map<IEnumerable<AuthorizePeople>>(prePost);
                _db.People.AddRange(commitToPost);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
            }

        }

        #endregion
        public async Task<IEnumerable<WelcomeRequestDTO>> GetStudentsAndRequestsByUserAsync(int userId, int currentSchoolYear)
        {
            try
            {
                List<WelcomeRequestDTO> WelcomeRequests = new List<WelcomeRequestDTO>();
                var user = await _db.Users.FindAsync(userId);
                var requests = await _db.PortalRequests.Where(x => x.userCode == userId)
                    .Include(x => x.StudentPortal)
                    .ToListAsync();
                var studentsBue = await _studentRepresentatives.getContactStudentsAsyncBUE((int)user.ContactCode, currentSchoolYear);
                foreach (var row in studentsBue)
                {
                    var existRequest = requests.Where(x => x.StudentPortal.studentCode == row.studentCode || x.StudentPortal.documentNumber.Trim().ToLower() == row.documentNumber.Trim().ToLower()).FirstOrDefault();
                    if (existRequest != null)
                        WelcomeRequests.Add(MapRequestDTO(existRequest));
                    else
                    {
                        var portalRequest = await _db.PortalRequests.Where(x => x.StudentPortal.documentNumber.Trim().ToLower() == row.documentNumber.Trim().ToLower())
                                .Include(p => p.StudentPortal)
                                .FirstOrDefaultAsync();
                        if (portalRequest != null)
                            WelcomeRequests.Add(MapRequestDTO(portalRequest));
                        else
                            WelcomeRequests.Add(MapRequestBUE((int)row.studentCode, row.photo, row.completeName));
                    }
                }

                foreach (var row in requests)
                {
                    var existStudent = studentsBue.Where(x => x.studentCode == row.StudentPortal.studentCode || x.documentNumber.Trim().ToLower() == row.StudentPortal.documentNumber.Trim().ToLower()).FirstOrDefault();
                    if (existStudent != null)
                    {
                        var rowStudentExisted = WelcomeRequests.Where(x => x.StudentCode == existStudent.studentCode);
                        if (rowStudentExisted == null)
                            WelcomeRequests.Add(MapRequestBUE((int)existStudent.studentCode, existStudent.photo, existStudent.completeName));
                    }
                    else
                    {
                        var rowExistedRequest = WelcomeRequests.Where(x => x.StudentCode == row.StudentPortal.studentCode);
                        if (rowExistedRequest == null)
                            WelcomeRequests.Add(MapRequestDTO(row));
                    }
                }
                return WelcomeRequests;
            }
            catch (Exception ex)
            {
                LogManagement.Instance.writeAssembly(MethodBase.GetCurrentMethod().DeclaringType, $"StackTrace: {ex.StackTrace} Error: {ex.Message}", ".Error");
                return null;
            }

        }
        private WelcomeRequestDTO MapRequestDTO(PortalRequest request)
        {
            return new WelcomeRequestDTO()
            {
                RequestId = (int)request.code,
                UserId = request.userCode,
                RequestStatus = request.requestStatus,
                StudentCodeSchoolYear = (int)request.StudentPortal.studentCodeSchoolYear,
                StudentCode = (int)request.StudentPortal.studentCode,
                YearSchool = (int)request.currentSchoolYear,
                UrlImage = request.StudentPortal.urlImage,
                Name = request.StudentPortal.completeName
            };
        }
        private WelcomeRequestDTO MapRequestBUE(int StudentCode, byte[] Image, string Name)
        {
            return new WelcomeRequestDTO()
            {
                StudentCode = StudentCode,
                Image = Image,
                Name = Name
            };
        }
    }
}
