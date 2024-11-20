using BUE.Inscriptions.Domain.Entity.DTO.storeProcedures;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using Newtonsoft.Json;
using System.Diagnostics;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Entity.DTO.EviCertia;
using BUE.Inscriptions.Domain.Entity.DTO.reports;
using iText.Kernel.Utils;
using System.Net.NetworkInformation;

namespace BUE.Inscriptions.Application.Implementation
{
    public class ReportService : IReportService
    {
        protected readonly IConfiguration _configuration;
        private IPortalRequestRepository _requestRep;
        private IGenaralPortalRepository _repGen;
        private readonly IAwsS3UploaderRepository _awsS3;
        private readonly IFireBaseRepository _fireBase;

        public ReportService(IGenaralPortalRepository repGen, IConfiguration configuration, IPortalRequestRepository requestRep, IAwsS3UploaderRepository awsS3, IFireBaseRepository fireBase)
        {
            _configuration = configuration;
            _requestRep = requestRep;
            _awsS3 = awsS3;
            _fireBase = fireBase;
            _repGen = repGen;
        }

        public byte[] ReadFileBytes(string filePath)
        {
            byte[] numArray = null;
            try
            {
                numArray = File.ReadAllBytes(filePath);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error al leer el archivo: " + ex.Message);
            }
            return numArray;
        }
        #region evicertia

        public async Task<IBaseResponse<string>> SendServiceContractServiceAsync(SingFileDTO modelDto)
        {
            byte[] fileBytes = null;
            string domainEviCertia = this._configuration.GetSection("AppSettings:EviCertiaDomain").Value;
            string EviCertiaUser = this._configuration.GetSection("AppSettings:EviCertiaUser").Value;
            string EviCertiaPassword = this._configuration.GetSection("AppSettings:EviCertiaPassword").Value;
            string EviCertiaTimeToLive = this._configuration.GetSection("AppSettings:EviCertiaTimeToLive").Value;
            string pathReports = this._configuration.GetSection("AppSettings:pathTempReport").Value;
            string url = domainEviCertia + "/api/EviSign/Submit";
            string fileNamePath = pathReports + "\\" + modelDto.FileName;
            fileBytes = modelDto.File != null ? modelDto.File : this.ReadFileBytes(fileNamePath);
            SigningPartieDTO signingPartieDTO = new SigningPartieDTO()
            {
                Name = modelDto.Name,
                Address = modelDto.Address,
                SigningMethod = modelDto.SigningMethod
            };
            OptionsDTO optionsDTO = new OptionsDTO()
            {
                TimeToLive = EviCertiaTimeToLive,
                CommitmentOptions = "Accept"
            };
            EviCertiaDataDTO payload = new EviCertiaDataDTO()
            {
                LookupKey = "Evi",
                Subject = modelDto.Subject,
                Document = fileBytes,
                SigningParties = signingPartieDTO,
                Options = optionsDTO
            };
            string json = JsonConvert.SerializeObject(payload);
            BaseResponse<string> baseResponse = new BaseResponse<string>();
            baseResponse.status = true;
            using (HttpClient client = new HttpClient())
            {
                string authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(EviCertiaUser + ":" + EviCertiaPassword));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("Accept", "application/json");
                request.Content = (HttpContent)new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.SendAsync(request);


                string responseContent = await response.Content.ReadAsStringAsync();



                if (!response.IsSuccessStatusCode)
                {
                    baseResponse.status = false;
                    baseResponse.Message = "ERROR";
                }
                var certiaResponse = JsonConvert.DeserializeObject<ResponsePostEviCertiaDTO>(responseContent);
                var action = modelDto.typeContract != null ? modelDto.typeContract : "Services";
                var modelRequestDTO = new PortalRequestDTO()
                {
                    code = new int?(modelDto.requestCode)
                };
                if (action == "Services")
                    modelRequestDTO.IdSingService = certiaResponse.uniqueId;
                if (action == "Transport")
                    modelRequestDTO.IdSingTransport = certiaResponse.uniqueId;
                if (action == "Banks")
                    modelRequestDTO.IdBankSingService = certiaResponse.uniqueId;
                string subPathS3 = string.Format("EviCertia/{0}/files", (object)modelDto.currentSchoolYear);
                string s3Result = await _awsS3.UploadBucketFileAsync(subPathS3, modelDto.FileName, fileBytes);
                if (s3Result != "")
                {
                    if (action == "Services")
                        modelRequestDTO.UrlSingService = s3Result;
                    if (action == "Transport")
                        modelRequestDTO.UrlSingTransport = s3Result;
                    if (action == "Banks")
                        modelRequestDTO.UrlBankSingService = s3Result;
                }
                var resultUpdate = await _requestRep.UpdateAsync(modelDto.requestCode, modelRequestDTO);
                baseResponse.Data = responseContent;
            }
            IBaseResponse<string> baseResponse1 = baseResponse;
            return baseResponse1;
        }
        public async Task<IBaseResponse<EviQuerySingDTO>> GetDocumentFromEviCertiaAsync(string uniqueKey, bool includeDocument = false)
        {
            try
            {
                string domainEviCertia = this._configuration.GetSection("AppSettings:EviCertiaDomain").Value;
                string EviCertiaUser = this._configuration.GetSection("AppSettings:EviCertiaUser").Value;
                string EviCertiaPassword = this._configuration.GetSection("AppSettings:EviCertiaPassword").Value;
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

        #endregion
        //public async Task<IBaseResponse<ReportParameterDTO>> GetReportPDFServiceAsync(ReportParameterDTO parameters)
        //{
        //    var baseResponse = new BaseResponse<ReportParameterDTO>();
        //    var typeReport = parameters.reportType is null ? ".pdf" : "." + parameters.reportType;
        //    var pathReports = _configuration.GetSection("AppSettings:pathTempReport").Value;
        //    var domain = _configuration.GetSection("AppSettings:Domain").Value;
        //    var fileName = parameters.reportName + "_" + parameters.userCode + "_" + parameters.studentCodeSchoolYear;
        //    var fileNameFin = parameters.reportName + "_" + parameters.userCode + "_" + parameters.studentCodeSchoolYear + "_FIN";
        //    var pathConfig = pathReports + @"\" + fileName + ".json";


        //    var result = await _repGen.getRecordStudentReportAsync((int)parameters.currentYearSchool, (int)parameters.studentCodeSchoolYear, (int)parameters.requestCode);

        //    var firstResult = result.First()


        //    if (System.IO.File.Exists(pathConfig))
        //    {
        //        System.IO.File.Delete(pathConfig);
        //    }
        //    var jsonString = JsonConvert.SerializeObject(parameters);
        //    System.IO.File.WriteAllText(pathConfig, jsonString);
        //    Process myProcess = new Process();
        //    var rutaRobot = _configuration.GetSection("AppSettings:RobotReport").Value;
        //    myProcess.StartInfo.Arguments = fileName + ".json";
        //    myProcess.StartInfo.RedirectStandardOutput = true;
        //    myProcess.StartInfo.UseShellExecute = false;
        //    myProcess.StartInfo.FileName = rutaRobot;
        //    myProcess.StartInfo.CreateNoWindow = false;
        //    myProcess.Start();
        //    string line = "";
        //    while (!myProcess.StandardOutput.EndOfStream)
        //    {
        //        line = myProcess.StandardOutput.ReadLine();
        //    }
        //    myProcess.WaitForExit();
        //    var reportResponse = parameters;
        //    reportResponse.reportStatus = line;
        //    reportResponse.reportResult = domain + "/Resources/Temp/" + fileName + typeReport;
        //    if (parameters.reportName == "RPTSTUDENTREGISTER")
        //    {
        //        if (typeReport == ".pdf")
        //        {
        //            //var finalPath = pathReports + @"\" + fileNameFin + typeReport;
        //            var finalPath = pathReports + @"\" + fileNameFin + typeReport;
        //            List<string> steps = new List<string>();
        //            var sept1 = pathReports + @"\RPTSTUDENTREGISTER_" + parameters.userCode + "_" + parameters.studentCodeSchoolYear + typeReport;
        //            steps.Add(sept1);
        //            var sept2 = pathReports + @"\STEP2_" + parameters.userCode + "_" + parameters.studentCodeSchoolYear + typeReport;
        //            steps.Add(sept2);
        //            if (merge(steps, reportResponse.reportResult, finalPath))
        //                reportResponse.reportResult = domain + "/Resources/Temp/" + fileNameFin + typeReport;
        //        }
        //    }
        //    baseResponse.Message = MessageUtil.Instance.Created;
        //    baseResponse.status = false;
        //    baseResponse.Data = reportResponse;
        //    return baseResponse;
        //}
        public async Task<IBaseResponse<ReportParameterDTO>> GetReportPDFServiceAsync(ReportParameterDTO parameters)
        {
            var baseResponse = new BaseResponse<ReportParameterDTO>();
            var typeReport = parameters.reportType is null ? ".pdf" : "." + parameters.reportType;
            var pathReports = _configuration.GetSection("AppSettings:pathTempReport").Value;
            var domain = _configuration.GetSection("AppSettings:Domain").Value;
            var fileName = parameters.reportName + "_" + parameters.userCode + "_" + parameters.studentCodeSchoolYear;
            var fileNameFin = parameters.reportName + "_" + parameters.userCode + "_" + parameters.studentCodeSchoolYear + "_FIN";
            var pathConfig = pathReports + @"\" + fileName + ".json";
            if (System.IO.File.Exists(pathConfig))
            {
                System.IO.File.Delete(pathConfig);
            }
            var jsonString = JsonConvert.SerializeObject(parameters);
            System.IO.File.WriteAllText(pathConfig, jsonString);
            Process myProcess = new Process();
            var rutaRobot = _configuration.GetSection("AppSettings:RobotReport").Value;
            myProcess.StartInfo.Arguments = fileName + ".json";
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = rutaRobot;
            myProcess.StartInfo.CreateNoWindow = false;
            myProcess.Start();
            string line = "";
            while (!myProcess.StandardOutput.EndOfStream)
            {
                line = myProcess.StandardOutput.ReadLine();
            }
            myProcess.WaitForExit();
            var reportResponse = parameters;
            reportResponse.reportStatus = line;
            reportResponse.reportResult = domain + "/Resources/Temp/" + fileName + typeReport;
            if (parameters.reportName == "RPTSTUDENTREGISTER")
            {
                if (typeReport == ".pdf")
                {
                    var finalPath = pathReports + @"\" + fileNameFin + typeReport;
                    List<string> steps = new List<string>();
                    var sept1 = pathReports + @"\RPTSTUDENTREGISTER_" + parameters.userCode + "_" + parameters.studentCodeSchoolYear + typeReport;
                    steps.Add(sept1);
                    var sept2 = pathReports + @"\STEP2_" + parameters.userCode + "_" + parameters.studentCodeSchoolYear + typeReport;
                    steps.Add(sept2);
                    if (merge(steps, reportResponse.reportResult, finalPath))
                        reportResponse.reportResult = domain + "/Resources/Temp/" + fileNameFin + typeReport;
                }
            }
            baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.status = false;
            baseResponse.Data = reportResponse;
            return baseResponse;
        }

        public bool merge(List<string> steps, string baseReport, string fileNameFin)
        {
            try
            {

                PdfDocument pdf = new PdfDocument(new PdfWriter(fileNameFin));
                PdfMerger merger = new PdfMerger(pdf);

                foreach (var item in steps)
                {
                    if (File.Exists(item))
                    {

                        PdfDocument firstSourcePdf = new PdfDocument(new PdfReader(item));
                        merger.Merge(firstSourcePdf, 1, firstSourcePdf.GetNumberOfPages());
                        firstSourcePdf.Close();

                    }
                }
                pdf.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<IBaseResponse<ReportParameterDTO>> GetReportPDF2ServiceAsync(ReportParameterDTO parameters)
        {
            BaseResponse<ReportParameterDTO> baseResponse = new BaseResponse<ReportParameterDTO>();
            string domain = this._configuration.GetSection("AppSettings:Domain").Value;
            string fileName = parameters.reportName + ".pdf";
            Process myProcess = new Process();
            string rutaRobot = this._configuration.GetSection("AppSettings:RobotReportConvertPdf").Value;
            myProcess.StartInfo.Arguments = parameters.Instruction;
            myProcess.StartInfo.RedirectStandardOutput = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = rutaRobot;
            myProcess.StartInfo.CreateNoWindow = false;
            myProcess.StartInfo.Verb = "runas";
            myProcess.Start();
            string line = " ";
            while (!myProcess.StandardOutput.EndOfStream)
                line += myProcess.StandardOutput.ReadLine();
            myProcess.WaitForExit();
            ReportParameterDTO reportResponse = parameters;
            reportResponse.reportStatus = line;
            reportResponse.reportResult = domain + "/Resources/Temp/" + fileName;
            baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.status = false;
            baseResponse.Data = reportResponse;
            IBaseResponse<ReportParameterDTO> pdF2ServiceAsync = baseResponse;
            return pdF2ServiceAsync;
        }
        public string bat()
        {
            string str = "C:\\inetpub\\wwwroot\\BUE_WAPI\\Resources\\Temp\\Contracts\\report.bat";
            Process process = new Process();
            try
            {
                process.StartInfo.FileName = str;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
                string end = process.StandardOutput.ReadToEnd();
                Console.WriteLine(end);
                return end;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al ejecutar el archivo .bat: " + ex.Message);
                return "Error al ejecutar el archivo .bat: " + ex.Message;
            }
            finally
            {
                process.Close();
            }
        }

        public async Task<IBaseResponse<ReportParameterDTO>> GenerateContractServiceAsync(ReportParameterDTO parameters)
        {
            string pathReports = this._configuration.GetSection("AppSettings:pathTempReport").Value;
            string domain = this._configuration.GetSection("AppSettings:Domain").Value;
            BaseResponse<ReportParameterDTO> baseResponse = new BaseResponse<ReportParameterDTO>();
            string pathPdf = pathReports + "\\Contracts\\" + parameters.reportName + ".pdf";
            ReportParameterDTO reportResponse = parameters;
            reportResponse.reportResult = domain + "/Resources/Temp/Contracts/" + parameters.reportName + ".pdf";
            if (File.Exists(pathPdf))
            {
                byte[] fileBytes = this.ReadFileBytes(pathPdf);
                if (fileBytes != null)
                    reportResponse.File = fileBytes;
            }
            baseResponse.Message = MessageUtil.Instance.Created;
            baseResponse.status = false;
            baseResponse.Data = reportResponse;
            IBaseResponse<ReportParameterDTO> contractServiceAsync = baseResponse;
            return contractServiceAsync;
        }

        public async Task<IBaseResponse<ReportParameterDTO>> GenerateTaskContractServiceAsync(ReportParameterDTO parameters)
        {
            string fireBaseNode = this._configuration.GetSection("AppSettings:FireBaseNode").Value;
            string resultFireBase = await _fireBase.setValue(fireBaseNode + "/" + parameters.code, parameters.reportName);
            if (resultFireBase == null)
                return null;
            BaseResponse<ReportParameterDTO> baseResponse = new BaseResponse<ReportParameterDTO>();
            ReportParameterDTO newResultDto = parameters;
            newResultDto.reportResult = resultFireBase;
            baseResponse.status = true;
            baseResponse.Data = newResultDto;
            return baseResponse;
        }
        public async Task<IBaseResponse<SingFileDTO>> GenerateBankContractServiceAsync(SingFileDTO parameters)
        {
            string tempReportPath = _configuration.GetSection("AppSettings:pathTempReport").Value + "\\Contracts\\" + parameters.FileName;
            CultureInfo cultureInfo = new CultureInfo("es-ES");
            DateTime now = DateTime.Now;
            string formattedDate = now.ToString("d 'de' MMMM 'del' yyyy", cultureInfo);

            try
            {
                if (File.Exists(tempReportPath))
                {
                    File.Delete(tempReportPath);
                }

                using (var writer = new PdfWriter(tempReportPath))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var document = new Document(pdf);
                        document.SetMargins(80f, 72f, 72f, 92f);

                        PdfFont font = PdfFontFactory.CreateFont("Helvetica");
                        float fontSize = 10f;
                        float titleFontSize = 12f;

                        AddParagraph(document, "Lugar y fecha: " + parameters.Bank.City + " a " + formattedDate + "\n\n\n\n\n", font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "AUTORIZACIÓN DE DÉBITO \n\n", font, titleFontSize, TextAlignment.CENTER, true);
                        AddParagraph(document, $"Por medio del presente y en virtud del contrato de servicios que tengo suscrito con la empresa {parameters.Bank.Vendor} (Cliente cobrador), autorizo(amos) de manera incondicional e irrevocable al BANCO PICHINCHA C.A. (Institución cobradora), debitar de mi(nuestra) Cuenta Ahorros o Corriente N° {parameters.Bank.Account} del Banco o Institución Financiera {parameters.Bank.BankName}, de la cual soy(somos) titular(es), los valores correspondientes por dichos servicios.\n", font, fontSize, TextAlignment.JUSTIFIED, false, 12f);
                        AddParagraph(document, "Me(nos) comprometo(comprometemos) a mantener los fondos necesarios, para que el BANCO PICHINCHA C.A., pueda realizar los débitos de la cuenta en mención, de manera efectiva.", font, fontSize, TextAlignment.JUSTIFIED, false, 12f);
                        AddParagraph(document, "\nAtentamente,\n", font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "\nNombre del cliente Pagador:\n", font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "\nEn caso que sea persona natural:\n", font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "Nombre: " + parameters.Bank.Customer, font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "CI., PA: " + parameters.Bank.Identification, font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "\nEn caso que sea persona jurídica:\n", font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "Nombre: ", font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "CI., PA: ", font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "Representante Legal", font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "Nombre de la empresa: ", font, fontSize, TextAlignment.LEFT);
                        AddParagraph(document, "RUC: ", font, fontSize, TextAlignment.LEFT);

                        document.Close();
                    }
                }

                var response = new BaseResponse<SingFileDTO>
                {
                    status = true,
                    Data = parameters
                };

                if (File.Exists(tempReportPath))
                {
                    response.Data.File = await ReadFileBytesAsync(tempReportPath);
                }
                else
                {
                    response.status = false;
                }

                return response;
            }
            catch (Exception ex)
            {
                // Log exception
                return new BaseResponse<SingFileDTO>
                {
                    status = false,
                    Message = ex.Message,
                  
                };
            }
        }

        private void AddParagraph(Document document, string text, PdfFont font, float fontSize, TextAlignment alignment, bool isBold = false, float spacingRatio = 0f)
        {
            var paragraph = new Paragraph(text).SetFont(font).SetFontSize(fontSize).SetTextAlignment(alignment);

            if (isBold)
            {
                paragraph.SetBold();
            }

            if (spacingRatio > 0f)
            {
                paragraph.SetSpacingRatio(spacingRatio);
            }

            document.Add(paragraph);
        }

        private async Task<byte[]> ReadFileBytesAsync(string filePath)
        {
            return await File.ReadAllBytesAsync(filePath);
        }
        public Task<IBaseResponse<SingFileDTO>> GenerateDataContractServiceAsync(SingFileDTO parameters)
        {
            try
            {
                string str1 = this._configuration.GetSection("AppSettings:pathTempReport").Value;
                string str2 = this._configuration.GetSection("AppSettings:MarkWather").Value;
                string str3 = str1 + "\\Contracts\\" + parameters.FileName;
                string str4 = str1 + "\\Contracts\\p1-" + parameters.FileName;
                DateTime now = DateTime.Now;
                now.ToString("d 'de' MMMM 'del' yyyy");
                string str5 = now.Day.ToString() ?? "";
                now.ToString("MMMM");
                if (File.Exists(str4))
                    File.Delete(str4);
                PdfDocument pdfDocument1 = new PdfDocument(new PdfWriter(str4));
                Document document = new Document(pdfDocument1);
                document.SetMargins(80f, 72f, 72f, 92f);
                PdfFont font = PdfFontFactory.CreateFont("Helvetica");
                float num1 = 10f;
                float num2 = 12f;
                Paragraph paragraph1 = ((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)new Paragraph(" ")).SetFont(font)).SetBold()).SetFontSize(num2)).SetTextAlignment(new TextAlignment?((TextAlignment)1));
                Paragraph paragraph2 = ((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)new Paragraph("\n Acuerdo de uso de datos para representantes legales \n\n")).SetFont(font)).SetBold()).SetFontSize(num2)).SetTextAlignment(new TextAlignment?((TextAlignment)1));
                Paragraph paragraph3 = ((ElementPropertyContainer<Paragraph>)((BlockElement<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)new Paragraph("Yo," + parameters.Bank.Customer + " con número de cédula  " + parameters.Bank.Identification.Trim() + " respectivamente, en mi calidad de representante legal de " + parameters.Name + ", estudiante en  SERVICIOS EDUCACIONALES MARTIMCERERÉ S.A., con número de RUC 1790510972001, doy mi consentimiento de manera voluntaria, libre, específica, informada e inequívoca para el uso y tratamiento de datos personales en el marco de la relación contractual de prestación de servicios educativos entre ambas partes según las siguientes estipulaciones:\n")).SetFont(font)).SetFontSize(num1)).SetSpacingRatio(12f)).SetTextAlignment(new TextAlignment?((TextAlignment)3));
                Paragraph paragraph4 = ((ElementPropertyContainer<Paragraph>)((BlockElement<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)new Paragraph("Declaro y acepto que los datos a ser tratados por el SERVICIOS EDUCACIONALES MARTIMCERERÉ S.A. de mi representado menor de edad son: nombre completo, fecha de nacimiento, sexo, género, nacionalidad, dirección domicilio, correo electrónico, teléfono, domicilio, personas con las que vive, fotografía, antecedentes clínicos, tipo de sangre, alergias, información relativa a particularidades cognitivas, trastornos y problemas psicológicos. \n")).SetFont(font)).SetFontSize(num1)).SetSpacingRatio(12f)).SetTextAlignment(new TextAlignment?((TextAlignment)3));
                Paragraph paragraph5 = ((ElementPropertyContainer<Paragraph>)((BlockElement<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)new Paragraph("Así mismo, acepto y declaro que se traten por parte de SERVICIOS EDUCACIONALES MARTIMCERERÉ S.A., la siguiente información de su representante legal y su representante económico: nombres completos, cédula, fecha de nacimiento, estado civil, correo electrónico, teléfono celular, dirección de domicilio, profesión, lugar de trabajo, teléfono de trabajo, datos de su empleador, cargo que ocupa, dirección de trabajo. \n")).SetFont(font)).SetFontSize(num1)).SetSpacingRatio(12f)).SetTextAlignment(new TextAlignment?((TextAlignment)3));
                Paragraph paragraph6 = ((ElementPropertyContainer<Paragraph>)((BlockElement<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)new Paragraph("Estos datos serán tratados según la Política de Protección de Datos de la institución y en cumplimiento de la Ley Orgánica de Protección de Datos Personales para las siguientes finalidades: \n")).SetFont(font)).SetFontSize(num1)).SetSpacingRatio(12f)).SetTextAlignment(new TextAlignment?((TextAlignment)3));
                List list = ((ElementPropertyContainer<List>)((ElementPropertyContainer<List>)((ElementPropertyContainer<List>)new List().SetSymbolIndent(12f)).SetFont(font)).SetFontSize(num1)).SetTextAlignment(new TextAlignment?((TextAlignment)3));
                list.Add(new ListItem("Mantener un registro y comunicación de las tareas, exámenes, pruebas y demás actividades derivadas de la prestación del servicio educativo a través de sistemas especializados o de modo directo por parte de SERVICIOS EDUCACIONALES MARTIMCERERÉ S.A."));
                list.Add(new ListItem("Garantizar la integridad y seguridad del estudiante y de sus bienes mediante el uso del sistema de CCTV dentro de las instalaciones de SERVICIOS EDUCACIONALES MARTIMCERERÉ S.A. Respetando el derecho a la intimidad personal y conforma a lo que indica el artículo 11 del Código de la Niñez y Adolescencia y el artículo 7 numeral 6 y numeral 8 de la LOPDP."));
                list.Add(new ListItem("Garantizar la integridad y seguridad del estudiante y de sus bienes mediante el uso del sistema de CCTV dentro de las instalaciones de SERVICIOS EDUCACIONALES MARTIMCERERÉ S.A. Respetando el derecho a la intimidad personal y conforme a lo que indica el artículo 11 del Código de la Niñez y Adolescencia y el artículo 7 numeral 6 y numeral 8 de la LOPDP."));
                ((RootElement<Document>)document).Add((IBlockElement)paragraph1);
                PdfDocument pdfDocument2 = new PdfDocument(new PdfReader(str2));
                pdfDocument2.GetNumberOfPages();
                for (int index = 1; index <= pdfDocument1.GetNumberOfPages(); ++index)
                    new PdfCanvas(pdfDocument1.GetPage(index)).AddXObject((PdfXObject)pdfDocument2.GetPage(index).CopyAsFormXObject(pdfDocument1));
                ((RootElement<Document>)document).Add((IBlockElement)paragraph2);
                ((RootElement<Document>)document).Add((IBlockElement)paragraph3);
                ((RootElement<Document>)document).Add((IBlockElement)paragraph4);
                ((RootElement<Document>)document).Add((IBlockElement)paragraph5);
                ((RootElement<Document>)document).Add((IBlockElement)paragraph6);
                ((RootElement<Document>)document).Add((IBlockElement)list);
                ((RootElement<Document>)document).Close();
                string p2 = this.GenerateP2(parameters);
                this.MergePDFs(str4, p2, str3);
                SingFileDTO singFileDto = parameters;
                BaseResponse<SingFileDTO> result = new BaseResponse<SingFileDTO>();
                result.status = true;
                if (File.Exists(str3))
                {
                    byte[] numArray = this.ReadFileBytes(str3);
                    singFileDto.File = numArray;
                    result.status = false;
                }
                if (File.Exists(str4))
                    File.Delete(str4);
                if (File.Exists(p2))
                    File.Delete(p2);
                result.Data = singFileDto;
                return Task.FromResult<IBaseResponse<SingFileDTO>>(result);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string GenerateP2(SingFileDTO parameters)
        {
            try
            {
                string str1 = this._configuration.GetSection("AppSettings:pathTempReport").Value;
                string str2 = this._configuration.GetSection("AppSettings:MarkWather").Value;
                string path = str1 + "\\Contracts\\p2-" + parameters.FileName;
                CultureInfo provider = new CultureInfo("es-ES");
                DateTime now = DateTime.Now;
                now.ToString("d 'de' MMMM 'del' yyyy", (IFormatProvider)provider);
                string str3 = now.Day.ToString() ?? "";
                now.ToString("MMMM");
                if (File.Exists(path))
                    File.Delete(path);
                PdfDocument pdfDocument1 = new PdfDocument(new PdfWriter(path));
                Document document = new Document(pdfDocument1);
                document.SetMargins(80f, 72f, 72f, 92f);
                PdfFont font = PdfFontFactory.CreateFont("Helvetica");
                float num1 = 10f;
                float num2 = 12f;
                Paragraph paragraph1 = ((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)new Paragraph(" ")).SetFont(font)).SetBold()).SetFontSize(num2)).SetTextAlignment(new TextAlignment?((TextAlignment)1));
                List list = ((ElementPropertyContainer<List>)((ElementPropertyContainer<List>)((ElementPropertyContainer<List>)new List().SetSymbolIndent(12f)).SetFont(font)).SetFontSize(num1)).SetTextAlignment(new TextAlignment?((TextAlignment)3));
                list.Add(new ListItem("Gestionar la relación entre SERVICIOS EDUCACIONALES MARTIMCERERÉ S.A., sus docentes y empleados con los alumnos y alumnas, así como los representantes legales."));
                list.Add(new ListItem("Atender las necesidades del estudiante en el ámbito de acción del Departamento de Consejería Estudiantil (DECE) según la legislación vigente y comunicar al cuerpo docente en caso de que el estudiante requiera un tratamiento especial en el ámbito académico."));
                list.Add(new ListItem("Brindar asistencia médica limitada en caso de requerirla."));
                list.Add(new ListItem("Llevar un registro de requerimientos de asistencia médica para un control de los servicios y respaldo."));
                list.Add(new ListItem("Reportar al/los representante/s legal/es o la persona designada en caso de ser necesario."));
                list.Add(new ListItem("Comunicar a prestadores de servicios de salud especializados, bróker o aseguradora en caso de ser necesario."));
                list.Add(new ListItem("Participar en actividades extracurriculares relacionadas a la prestación del servicio educativo como paseos, eventos deportivos, eventos académicos, conferencias y charlas para la formación del estudiante."));
                list.Add(new ListItem("Cumplir con las obligaciones derivadas de la prestación del servicio educativo y objeto social de SERVICIOS EDUCACIONALES MARTIMCERERÉ S.A."));
                Paragraph paragraph2 = ((ElementPropertyContainer<Paragraph>)((BlockElement<Paragraph>)((ElementPropertyContainer<Paragraph>)((ElementPropertyContainer<Paragraph>)new Paragraph("Conozco que SERVICIOS EDUCACIONALES MARTIMCERERÉ S.A.. en cumplimiento con la Ley Orgánica de Protección de Datos Personales, el Código de la Niñez y Adolescencia y las buenas prácticas usará los datos solo para fines justificados y que en cualquier momento podré hacer ejercicio de los derechos ARCO y de portabilidad. \n")).SetFont(font)).SetFontSize(num1)).SetSpacingRatio(12f)).SetTextAlignment(new TextAlignment?((TextAlignment)3));
                PdfDocument pdfDocument2 = new PdfDocument(new PdfReader(str2));
                pdfDocument2.GetNumberOfPages();
                ((RootElement<Document>)document).Add((IBlockElement)paragraph1);
                for (int index = 1; index <= pdfDocument1.GetNumberOfPages(); ++index)
                    new PdfCanvas(pdfDocument1.GetPage(index)).AddXObject((PdfXObject)pdfDocument2.GetPage(index).CopyAsFormXObject(pdfDocument1));
                ((RootElement<Document>)document).Add((IBlockElement)list);
                ((RootElement<Document>)document).Add((IBlockElement)paragraph2);
                ((RootElement<Document>)document).Close();
                return path;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void MergeDocumentsPDFs(List<PdfDocument> pdfDocuments, string outputFilePath)
        {
            PdfDocument pdfDocument1 = new PdfDocument(new PdfWriter(outputFilePath));
            try
            {
                foreach (PdfDocument pdfDocument2 in pdfDocuments)
                {
                    int numberOfPages = pdfDocument2.GetNumberOfPages();
                    for (int index = 1; index <= numberOfPages; ++index)
                    {
                        PdfPage page = pdfDocument2.GetPage(index);
                        pdfDocument1.AddPage(page.CopyTo(pdfDocument1));
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                foreach (PdfDocument pdfDocument3 in pdfDocuments)
                    pdfDocument3.Close();
                pdfDocument1.Close();
            }
        }

        public void MergePDFs(string filePath1, string filePath2, string outputFilePath)
        {
            try
            {

                PdfDocument pdfDocument1 = new PdfDocument(new PdfWriter(outputFilePath));
                List<PdfDocument> pdfDocumentList = new List<PdfDocument>();
                try
                {
                    pdfDocumentList.Add(new PdfDocument(new PdfReader(filePath1)));
                    pdfDocumentList.Add(new PdfDocument(new PdfReader(filePath2)));
                    foreach (PdfDocument pdfDocument2 in pdfDocumentList)
                    {
                        int numberOfPages = pdfDocument2.GetNumberOfPages();
                        for (int index = 1; index <= numberOfPages; ++index)
                        {
                            PdfPage page = pdfDocument2.GetPage(index);
                            pdfDocument1.AddPage(page.CopyTo(pdfDocument1));
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    foreach (PdfDocument pdfDocument3 in pdfDocumentList)
                        pdfDocument3.Close();
                    pdfDocument1.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


    }
}
