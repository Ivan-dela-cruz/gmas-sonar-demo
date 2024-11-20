using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.reports;
using BUE.Inscriptions.Domain.Inscriptions.DTO;
using Newtonsoft.Json.Serialization;
using BUE.Inscriptions.Domain.Inscriptions.Entities;
using BUE.Inscriptions.Shared.Utils;
using BUE.Inscriptions.Domain.Entity.DTO.EviCertia;

namespace BUE.Inscriptions.Application.Implementation
{
    public class ContractService : IContractService
    {
        private readonly IConfiguration _configuration;
        private readonly IAwsS3UploaderRepository _awsS3;
        private readonly IInscriptionRepository _inscriptionRepository;
        private readonly IStudentService _studentService;

        public ContractService(IStudentService studentService, IConfiguration configuration, IInscriptionRepository inscriptionRepository, IAwsS3UploaderRepository awsS3)
        {
            _configuration = configuration;
            _inscriptionRepository = inscriptionRepository;
            _awsS3 = awsS3;
            _studentService = studentService;
        }

        public async Task<IBaseResponse<string>> ContractGenerateServiceAsync(TemplateReport templateReport)
        {
            var baseResponse = new BaseResponse<string> { status = true };
            try
            {
                var requestUri = _configuration.GetSection("AppSettings:ExternalReportServer").Value + "/api/contracts/generate";

                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                };

                var client = new HttpClient();
                var detailsResult = await _studentService.GetStudentDetailsServiceAsync(templateReport.StudentId, (int)templateReport.AcademicYeartId);
                if (detailsResult == null)
                {
                    baseResponse.Message = MessageUtil.Instance.NotFound;
                    baseResponse.statusCode = MessageUtil.Instance.RECORD_NOT_FOUND;
                    baseResponse.status = false;
                    return baseResponse;
                }
                var currentInscription = await _inscriptionRepository.GetByIdAsync((int)detailsResult.Data.InscriptionId);
                if (templateReport.IsUpdate == null)
                {
                    switch (templateReport.Template.ToLower())
                    {
                        case "transport":
                            if (currentInscription.UseTransport == true && currentInscription.SingTransportId != null)
                            {
                                baseResponse.Data = currentInscription.SingTransportUrl;
                                return baseResponse;
                            }
                            break;
                        case "transporte":
                            if (currentInscription.UseTransport == true && currentInscription.SingTransportId != null)
                            {
                                baseResponse.Data = currentInscription.SingTransportUrl;
                                return baseResponse;
                            }
                            break;
                        case "services":
                            if (currentInscription.SingServiceId != null)
                            {
                                baseResponse.Data = currentInscription.SingServiceUrl;
                                return baseResponse;
                            }
                            break;
                    }
                }
                var payload = new TemplateReportRequest<StudentDetails>();
                payload.Client = templateReport.Client;
                payload.Template = templateReport.Template;
                payload.Data = detailsResult.Data;
                var jsonPayload = JsonConvert.SerializeObject(payload, settings);

                var request = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
                };
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                ApiReportResponse apiResponse = JsonConvert.DeserializeObject<ApiReportResponse>(result);
                baseResponse.Data = apiResponse.Data.UrlReport;

                switch (templateReport.Template.ToLower())
                {
                    case "transport":
                        if (currentInscription.UseTransport == true && currentInscription.SingTransportId == null)
                            currentInscription.SingTransportUrl = apiResponse.Data.UrlReport;
                        if (templateReport.IsUpdate != null)
                        {
                            SaveLogSing(templateReport, currentInscription.SingTransportId, currentInscription.Id);
                            currentInscription.SingTransportId = null;
                            currentInscription.SingTransportUrl = apiResponse.Data.UrlReport;
                        }

                        break;
                    case "transporte":
                        if (currentInscription.UseTransport == true && currentInscription.SingTransportId == null)
                            currentInscription.SingTransportUrl = apiResponse.Data.UrlReport;
                        if (templateReport.IsUpdate != null)
                        {
                            SaveLogSing(templateReport, currentInscription.SingTransportId, currentInscription.Id);
                            currentInscription.SingTransportId = null;
                            currentInscription.SingTransportUrl = apiResponse.Data.UrlReport;
                        }
                        break;
                    case "services":
                        if (currentInscription.SingServiceId == null)
                            currentInscription.SingServiceUrl = apiResponse.Data.UrlReport;
                        if (templateReport.IsUpdate != null)
                        {
                            SaveLogSing(templateReport, currentInscription.SingServiceId, currentInscription.Id);
                            currentInscription.SingServiceId = null;
                            currentInscription.SingServiceUrl = apiResponse.Data.UrlReport;
                        }
                        break;
                }
                if (apiResponse.Data.UrlReport != null)
                {
                    string action = templateReport.IsUpdate != null ? templateReport.Template.ToLower() : "";
                    await _inscriptionRepository.UpdateSingsAsync((int)detailsResult.Data.InscriptionId, currentInscription, action);
                }
                return baseResponse;
            }
            catch (Exception e)
            {
                baseResponse.status = false;
                baseResponse.Message = e.Message;
                return baseResponse;
            }
        }

        public byte[] ReadFileBytes(string filePath, bool externalRead = false)
        {
            try
            {
                if (externalRead == true)
                {
                    var external = _awsS3.GetFileBytesFromS3Async(filePath).Result;
                    return external;
                }

                return File.ReadAllBytes(filePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
                return null;
            }
        }

        public async Task<IBaseResponse<string>> SendServiceContractServiceAsync(SingFileDTO modelDto)
        {
            var baseResponse = new BaseResponse<string> { status = true };
            try
            {
                var currentInscription = await _inscriptionRepository.GetByIdAsync(modelDto.requestCode);
                if (currentInscription != null)
                {
                    switch (modelDto.typeContract.Trim().ToLower())
                    {
                        case "services":
                            if (currentInscription.SingServiceId != null)
                            {
                                baseResponse.Data = currentInscription.SingServiceId;
                                return baseResponse;
                            }
                            break;
                        case "transport":
                            if (currentInscription.SingTransportId != null)
                            {
                                baseResponse.Data = currentInscription.SingTransportId;
                                return baseResponse;
                            }
                            break;
                        case "transporte":
                            if (currentInscription.SingTransportId != null)
                            {
                                baseResponse.Data = currentInscription.SingTransportId;
                                return baseResponse;
                            }
                            break;
                        case "banks":
                            if (currentInscription.BankSingServiceId != null)
                            {
                                if (modelDto.IsResend != true)
                                {
                                    baseResponse.Data = currentInscription.BankSingServiceId;
                                    return baseResponse;
                                }
                            }
                            break;

                    }
                }
                string fileNamePath = modelDto.ExternalRead == true ? modelDto.FileName : GetFilePath(modelDto.FileName);
                byte[] fileBytes = ReadFileBytes(fileNamePath, (bool)modelDto.ExternalRead);
                var payload = CreatePayload(modelDto, fileBytes);

                var responseContent = await SendRequestAsync(payload);

                var certiaResponse = JsonConvert.DeserializeObject<ResponsePostEviCertiaDTO>(responseContent);
                if (certiaResponse.uniqueId != null)
                {
                    var certiaQuery = await GetDocumentFromEviCertiaAsync(certiaResponse.uniqueId, true);
                    if (certiaQuery.Data != null)
                    {
                        byte[] documentBytes = Convert.FromBase64String(certiaQuery.Data.document);
                        fileBytes = documentBytes;
                    }
                    if (modelDto.ExternalRead == true)
                    {
                        modelDto.FileName = GetFileNameExternal(modelDto.FileName);
                    }
                    await _inscriptionRepository.UpdateSingContractAsync(modelDto, fileBytes, certiaResponse.uniqueId);
                }


                baseResponse.Data = responseContent;
            }
            catch (Exception ex)
            {
                baseResponse.status = false;
                baseResponse.Message = ex.Message;
            }
            return baseResponse;
        }
        private void SaveLogSing(TemplateReport templateReport, string DocumentId, int InscriptionId)
        {
            try
            {
                var jsonItem = JsonConvert.SerializeObject(templateReport);
                LogManagement.Instance.write("CONTRACT", "SaveLogSing", $" [InscriptionId = {InscriptionId}] => [DocumentId = {DocumentId}] => [ " + jsonItem + " ]", "BUE.Inscriptions.Application.Implementation.Contracts");

            }
            catch (Exception) { }
        }
        private string GetFileNameExternal(string path)
        {
            try
            {
                Uri uri = new Uri(path);
                string fileName = Path.GetFileName(uri.LocalPath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(uri.LocalPath);
                string fileExtension = Path.GetExtension(uri.LocalPath);
                return fileNameWithoutExtension + "." + fileExtension;

            }
            catch (Exception)
            {
                return path;
            }
        }


        private string GetFilePath(string fileName)
        {
            string pathContracts = _configuration.GetSection("AppSettings:pathTempContract").Value;
            return $"{pathContracts}\\{fileName}";
        }

        private EviCertiaDataDTO CreatePayload(SingFileDTO modelDto, byte[] fileBytes)
        {
            string eviCertiaTimeToLive = _configuration.GetSection("AppSettings:EviCertiaTimeToLive").Value;
            var signingPartieDTO = new SigningPartieDTO
            {
                Name = modelDto.Name,
                Address = modelDto.Address,
                SigningMethod = modelDto.SigningMethod
            };
            var optionsDTO = new OptionsDTO
            {
                TimeToLive = eviCertiaTimeToLive,
                CommitmentOptions = "Accept"
            };
            return new EviCertiaDataDTO
            {
                LookupKey = "Evi",
                Subject = modelDto.Subject,
                Document = fileBytes,
                SigningParties = signingPartieDTO,
                Options = optionsDTO
            };
        }

        private async Task<string> SendRequestAsync(EviCertiaDataDTO payload)
        {
            string domainEviCertia = _configuration.GetSection("AppSettings:EviCertiaDomain").Value;
            string eviCertiaUser = _configuration.GetSection("AppSettings:EviCertiaUser").Value;
            string eviCertiaPassword = _configuration.GetSection("AppSettings:EviCertiaPassword").Value;
            string url = $"{domainEviCertia}/api/EviSign/Submit";

            using (HttpClient client = new HttpClient())
            {
                string authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{eviCertiaUser}:{eviCertiaPassword}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                string responseContent = await response.Content.ReadAsStringAsync();
                try
                {
                    var newPayload = payload;
                    newPayload.Document = new byte[0];
                    var jsonItem = JsonConvert.SerializeObject(newPayload);
                    LogManagement.Instance.write("EVICERTA", "SendRequestAsync", $"[{payload.SigningParties.Address}] => [ " + jsonItem + " ]", "BUE.Inscriptions.Application.Implementation.Evicertia");
                    LogManagement.Instance.write("EVICERTA", "Response", $"[{payload.SigningParties.Address}] => [ " + responseContent + " ]", "BUE.Inscriptions.Application.Implementation.Evicertia");
                }
                catch (Exception) { }
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Error sending request to EviCertia: " + responseContent);
                }

                return responseContent;
            }
        }
        public async Task<IBaseResponse<EviQuerySingDTO>> GetDocumentFromEviCertiaAsync(string uniqueKey, bool includeDocument = false)
        {
            try
            {
                string domainEviCertia = _configuration.GetSection("AppSettings:EviCertiaDomain").Value;
                string EviCertiaUser = _configuration.GetSection("AppSettings:EviCertiaUser").Value;
                string EviCertiaPassword = _configuration.GetSection("AppSettings:EviCertiaPassword").Value;
                string url = domainEviCertia + "/api/EviSign/Query?WithUniqueIds=" + uniqueKey + "&Format=json";
                if (includeDocument)
                    url = url + "&IncludeDocumentOnResult=" + includeDocument.ToString();
                BaseResponse<EviQuerySingDTO> baseResponse = new BaseResponse<EviQuerySingDTO>();
                baseResponse.status = true;
                using (HttpClient client = new HttpClient())
                {
                    string authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(EviCertiaUser + ":" + EviCertiaPassword));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Add("Accept", "application/json");
                    HttpResponseMessage response = await client.SendAsync(request);
                    string responseContent = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        baseResponse.status = false;
                        baseResponse.Message = "ERROR";
                    }
                    ResponseQueryEviDTO certiaResponse = JsonConvert.DeserializeObject<ResponseQueryEviDTO>(responseContent);
                    if (certiaResponse != null && certiaResponse.results != null && certiaResponse.results.Count > 0)
                        baseResponse.Data = certiaResponse.results[0];
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                Exception e = ex;
                return null;
            }
        }
        public async Task<IBaseResponse<string>> CancelContractAsync(EvicertiaCancelDTO payload)
        {
            var baseResponse = new BaseResponse<string> { status = true };
            string domainEviCertia = _configuration.GetSection("AppSettings:EviCertiaDomain").Value;
            string eviCertiaUser = _configuration.GetSection("AppSettings:EviCertiaUser").Value;
            string eviCertiaPassword = _configuration.GetSection("AppSettings:EviCertiaPassword").Value;
            string url = $"{domainEviCertia}/api/EviSign/Cancel";

            using (HttpClient client = new HttpClient())
            {
                string authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{eviCertiaUser}:{eviCertiaPassword}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string json = JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);

                string responseContent = await response.Content.ReadAsStringAsync();
                try
                {
                    LogManagement.Instance.write("EVICERTA", "CancelContractAsync", $"[{payload.uniqueid}] => [ " + json + " ]", "BUE.Inscriptions.Application.Implementation.Evicertia.Cancel");
                    LogManagement.Instance.write("EVICERTA", "Response", $"[{payload.uniqueid}] => [ " + responseContent + " ]", "BUE.Inscriptions.Application.Implementation.Evicertia.Cancel");
                }
                catch (Exception) { }
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Error sending request to EviCertia: " + responseContent);
                }
                var certiaResponse = JsonConvert.DeserializeObject<ResponsePostEviCertiaDTO>(responseContent);
                baseResponse.Data = certiaResponse.uniqueId;
                return baseResponse;
            }
        }
    }
}
