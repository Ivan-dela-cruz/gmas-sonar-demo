using AutoMapper;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Paging;
using System.DirectoryServices.Protocols;


namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class InscriptionRepository : BaseRepository, IInscriptionRepository
    {
        private readonly PortalMatriculasDBContext _db;
        private readonly IAwsS3UploaderRepository _awsS3;
        private readonly IMapper _mapper;
        public string Message { get; set; }
        public string StatusCode { get; set; }

        public InscriptionRepository(PortalMatriculasDBContext db, IMapper mapper, IAwsS3UploaderRepository awsS3)
        {
            _db = db;
            _mapper = mapper;
            _awsS3 = awsS3;
        }

        public async Task<InscriptionDTO> CreateAsync(InscriptionDTO entity)
        {
            try
            {
                Inscription inscription = _mapper.Map<InscriptionDTO, Inscription>(entity);
                _db.Inscription.Add(inscription);
                await _db.SaveChangesAsync();
                return _mapper.Map<Inscription, InscriptionDTO>(inscription);

            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                Inscription inscription = await _db.Inscription.FindAsync(id);
                if (inscription == null)
                {
                    return false;
                }
                _db.Inscription.Remove(inscription);
                await _db.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<InscriptionDTO>> GetAsync()
        {
            var inscriptions = await _db.Inscription.ToListAsync();
            return _mapper.Map<IEnumerable<InscriptionDTO>>(inscriptions);
        }
        public async Task<IEnumerable<StudentDTO>> GetByAcademicYearAsync(PagingQueryParameters paging)
        {
            if (paging.RequestStatus == null)
            {
              var allStudents = await _db.Student
             .Include(x => x.StudentFamilies)
             .Include(x => x.Inscriptions)
             .ThenInclude(i => i.Level)
             .Include(x => x.Inscriptions)
             .ThenInclude(i => i.CourseGrade)
             .Where(x => x.Inscriptions.Any(i => i.AcademicYearId == paging.AcademicYearId))
             .ToListAsync();
                return _mapper.Map<IEnumerable<StudentDTO>>(allStudents);
            }

            var students = await _db.Student
                .Include(x => x.StudentFamilies)
                .Include(x => x.Inscriptions)
                .ThenInclude(i => i.Level)
                .Include(x => x.Inscriptions)
                .ThenInclude(i => i.CourseGrade)
                .Where(x => x.Inscriptions.Any(i => i.AcademicYearId == paging.AcademicYearId) && x.Inscriptions.Any(i => i.RequestStatus == paging.RequestStatus))
                .ToListAsync();
            return _mapper.Map<IEnumerable<StudentDTO>>(students);
        }

        public async Task<InscriptionDTO> GetByIdAsync(int id)
        {
            var inscription = await _db.Inscription
                .FirstOrDefaultAsync(e => e.Id == id);
            return _mapper.Map<InscriptionDTO>(inscription);
        }

        public async Task<InscriptionDTO> UpdateAsync(int id, InscriptionDTO entity)
        {
            try
            {
                var inscription = _mapper.Map<InscriptionDTO, Inscription>(entity);
                var existingInscription = await _db.Inscription.FindAsync(id);

                if (existingInscription == null)
                {
                    throw new NullReferenceException(MessageUtil.Instance.NotFound);
                }
                var currentInscription = MapProperties(inscription, existingInscription);
                _db.Inscription.Update(currentInscription);
                await _db.SaveChangesAsync();
                return _mapper.Map<Inscription, InscriptionDTO>(currentInscription);
            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
        }
        public async Task<InscriptionDTO> UpdateSingsAsync(int id, InscriptionDTO entity, string type = "")
        {
            try
            {
                var inscription = _mapper.Map<InscriptionDTO, Inscription>(entity);
                var existingInscription = await _db.Inscription.FindAsync(id);

                if (existingInscription == null)
                {
                    throw new NullReferenceException(MessageUtil.Instance.NotFound);
                }
                var currentInscription = MapProperties(inscription, existingInscription);

                switch (type)
                {
                    case "transport":

                        currentInscription.SingTransportId = entity.SingTransportId;
                        break;
                    case "transporte":
                        currentInscription.SingTransportId = entity.SingTransportId;

                        break;
                    case "services":
                        currentInscription.SingServiceId = entity.SingServiceId;

                        break;
                }
                _db.Inscription.Update(currentInscription);
                await _db.SaveChangesAsync();
                return _mapper.Map<Inscription, InscriptionDTO>(currentInscription);
            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
        }
        public async Task<bool> UpdateStatusAsync(RequestStatusChange requestStatus)
        {
            try
            {
                var existingInscription = await _db.Inscription
                    .Where(x => requestStatus.Ids.Contains(x.Id)).ToListAsync();

                foreach (var item in existingInscription)
                {
                    item.RequestStatus = requestStatus.Status;
                    item.Comment1 = requestStatus.Comment;
                }

                _db.Inscription.UpdateRange(existingInscription);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return false;
            }
        }
        public async Task<InscriptionDTO> UpdateProcessAsync(int id, InscriptionDTO entity)
        {
            try
            {

                var existingInscription = await _db.Inscription.FindAsync(id);

                if (existingInscription == null)
                {
                    throw new NullReferenceException(MessageUtil.Instance.NotFound);
                }
                existingInscription.EnrollmentProcess = entity.EnrollmentProcess;
                _db.Inscription.Update(existingInscription);
                await _db.SaveChangesAsync();
                return _mapper.Map<Inscription, InscriptionDTO>(existingInscription);
            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
        }
        public async Task<InscriptionDTO> UpdateSingContractAsync(SingFileDTO modelDto, byte[] fileBytes, string IdEvicertia)
        {
            try
            {
                string subPathS3 = $"EviCertia/{modelDto.currentSchoolYear}/files";
                string s3Result = await _awsS3.UploadBucketFileAsync(subPathS3, modelDto.FileName, fileBytes);

                var existingInscription = await _db.Inscription.FindAsync(modelDto.requestCode);

                if (existingInscription == null)
                {
                    throw new NullReferenceException(MessageUtil.Instance.NotFound);
                }
                switch (modelDto.typeContract.Trim().ToLower())
                {
                    case "services":
                        existingInscription.SingServiceUrl = s3Result;
                        existingInscription.SingServiceId = IdEvicertia;
                        break;
                    case "transport":
                        existingInscription.SingTransportUrl = s3Result;
                        existingInscription.SingTransportId = IdEvicertia;
                        break;
                    case "banks":
                        existingInscription.BankSingServiceUrl = s3Result;
                        existingInscription.BankSingServiceId = IdEvicertia;
                        break;

                }
                _db.Inscription.Update(existingInscription);
                await _db.SaveChangesAsync();
                return _mapper.Map<Inscription, InscriptionDTO>(existingInscription);
            }
            catch (Exception ex)
            {
                HandlerOperationException(ex);
                Message = this.MessageException;
                StatusCode = this.CodeException;
                return null;
            }
        }

    }
}

