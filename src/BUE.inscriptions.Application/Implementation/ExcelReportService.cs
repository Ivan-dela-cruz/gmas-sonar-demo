using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using Newtonsoft.Json.Serialization;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Shared.Utils;
using BUE.Inscriptions.Infrastructure.Interfaces;
using System.Text;

namespace BUE.Inscriptions.Application.Implementation
{
    public class ExcelReportService : IExcelReportService
    {
        private readonly IConfiguration _configuration;
        private readonly IStudentRepository _studentRepository;
        private readonly JsonSerializerSettings _settings;

        public ExcelReportService(IConfiguration configuration, IStudentRepository studentRepository)
        {
            _configuration = configuration;
            _studentRepository = studentRepository;
            _settings = new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                Formatting = Formatting.Indented
            };
        }

        public async Task<IBaseResponse<byte[]>> TransportExcelGenerateServiceAsync(TemplateReport templateReport)
        {
            var baseResponse = new BaseResponse<byte[]> { status = true };
            try
            {
                var requestUri = _configuration.GetSection("AppSettings:ExternalReportServer").Value + "/api/excel-generate";
                var students = await GetGeneralStudentsServiceAsync((int)templateReport.AcademicYeartId);
                var allStudentUseTransport = students.Where(x => x.InscriptionUseTransport == "SI");
                var selectedColumns = allStudentUseTransport.Select(student => new
                {
                    student.StudentExternalId,
                    student.StudentName,
                    student.StudentIdentification,
                    student.StudentLevel,
                    student.StudentCourse,
                    student.StudentAddress,
                    student.StudentSector,
                    student.InscriptionUseTransport,
                    student.PrincipalPersonIdentification,
                    student.PrincipalPersonName,
                    student.PrincipalPersonPhone,
                    student.PrincipalPersonEmail,

                }).ToList();

                var headers = new string[] { "CodigoEstudiante", "Estudiante","Identificación","Nivel","Curso","Dirección"
                    , "Sector","Transporte","Rep. Identificación","Representante","Rep. Télefono","Rep. Email" };

                var dataSet = new
                {
                    dataSet = new
                    {
                        headers = headers,
                        data = selectedColumns
                    },
                    columnFormats = new { C = "@", I = "@", K = "@" }
                };
                var jsonPayload = JsonConvert.SerializeObject(dataSet, _settings);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsByteArrayAsync();
                baseResponse.Data = result;
                return baseResponse;
            }
            catch (Exception e)
            {
                baseResponse.status = false;
                baseResponse.Message = e.Message;
                return baseResponse;
            }
        }
        public async Task<IBaseResponse<byte[]>> AutorizationsExcelGenerateServiceAsync(TemplateReport templateReport)
        {
            var baseResponse = new BaseResponse<byte[]> { status = true };
            try
            {
                var requestUri = _configuration.GetSection("AppSettings:ExternalReportServer").Value + "/api/excel-generate";
                var students = await GetGeneralStudentsServiceAsync((int)templateReport.AcademicYeartId);
                var selectedColumns = students.Select(student => new
                {
                    student.StudentExternalId,
                    student.StudentName,
                    student.StudentIdentification,
                    student.StudentLevel,
                    student.StudentCourse,
                    student.StudentEmail,
                    student.StudentPhone,
                    student.PrincipalPersonIdentification,
                    student.PrincipalPersonName,
                    student.PrincipalPersonPhone,
                    student.PrincipalPersonEmail,
                    student.InscriptionAuthorizeImageUsage,
                    student.InscriptionAuthorizeImagePublication,
                    student.InscriptionInstitutionalRepresentation,
                    student.InscriptionPedagogicalTrip,

                }).ToList();

                var headers = new string[] { "CodigoEstudiante", "Estudiante","Identificación","Nivel","Curso","Email"
                    , "Télefono","Rep. Identificación","Representante","Rep. Télefono","Rep. Email","Usar Imagen", "Publicar Imagen", "Representación Institucional","Salidas pedagógicas " };

                var dataSet = new
                {
                    dataSet = new
                    {
                        headers = headers,
                        data = selectedColumns
                    },
                    columnFormats = new { C = "@", G = "@", H = "@",J="@" }
                };
                var jsonPayload = JsonConvert.SerializeObject(dataSet, _settings);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsByteArrayAsync();
                baseResponse.Data = result;
                return baseResponse;
            }
            catch (Exception e)
            {
                baseResponse.status = false;
                baseResponse.Message = e.Message;
                return baseResponse;
            }
        }
        public async Task<IBaseResponse<byte[]>> AllStudentsExcelGenerateServiceAsync(TemplateReport templateReport)
        {
            var baseResponse = new BaseResponse<byte[]> { status = true };
            try
            {
                var requestUri = _configuration.GetSection("AppSettings:ExternalReportServer").Value + "/api/excel-generate";
                var students = await GetGeneralStudentsServiceAsync((int)templateReport.AcademicYeartId);
                var selectedColumns = students.Select(student => new
                {
                    student.StudentExternalId,
                    student.StudentName,
                    student.StudentIdentification,
                    student.StudentLevel,
                    student.StudentCourse,
                    student.StudentEmail,
                    student.StudentPhone,
                    student.PrincipalPersonIdentification,
                    student.PrincipalPersonName,
                    student.PrincipalPersonPhone,
                    student.PrincipalPersonEmail,
                    student.InscriptionAuthorizeImageUsage,
                    student.InscriptionAuthorizeImagePublication,
                    student.InscriptionInstitutionalRepresentation,
                    student.InscriptionPedagogicalTrip,

                }).ToList();

                var headers = new string[] { "CodigoEstudiante", "Estudiante","Identificación","Nivel","Curso","Email"
                    , "Télefono","Rep. Identificación","Representante","Rep. Télefono","Rep. Email","Usar Imagen", "Publicar Imagen", "Representación Institucional","Salidas pedagógicas " };

                var dataSet = new
                {
                    dataSet = new
                    {
                        headers = headers,
                        data = selectedColumns
                    },
                    columnFormats = new { C = "@", G = "@", H = "@", J = "@" }
                };
                var jsonPayload = JsonConvert.SerializeObject(dataSet, _settings);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsByteArrayAsync();
                baseResponse.Data = result;
                return baseResponse;
            }
            catch (Exception e)
            {
                baseResponse.status = false;
                baseResponse.Message = e.Message;
                return baseResponse;
            }
        }
        public async Task<IBaseResponse<IEnumerable<StudentDetails>>> GetStudentsTransportServiceAsync(int academicYearId)
        {
            var baseResponse = new BaseResponse<IEnumerable<StudentDetails>>();
            var details = await _studentRepository.GetAllStudentsAsync(academicYearId);
            if (details == null)
            {
                baseResponse.Message = MessageUtil.Instance.NotFound;
                baseResponse.status = false;
                return baseResponse;
            }
            var allStudentUseTransport = details.Where(x => x.InscriptionUseTransport == "SI");
            baseResponse.Data = allStudentUseTransport;
            return baseResponse;
        }
        public async Task<IEnumerable<StudentDetails>> GetGeneralStudentsServiceAsync(int academicYearId)
        {
           
            var details = await _studentRepository.GetAllStudentsAsync(academicYearId);
            if (details == null)
                return null;
            return details;
        }
        
    }
}
