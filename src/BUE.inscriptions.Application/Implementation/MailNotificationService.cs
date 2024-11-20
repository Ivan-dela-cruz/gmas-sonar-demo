using BUE.Inscriptions.Infrastructure.Interfaces;
using BUE.Inscriptions.Domain.Entity.DTO;
using BUE.Inscriptions.Domain.Response;
using BUE.Inscriptions.Application.Interfaces;
using BUE.Inscriptions.Shared.Utils;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BUE.Inscriptions.Application.Implementation
{
    public class MailNotificationService : IMailNotification
    {
        protected readonly IConfiguration _configuration;
        private INotificationRepository _notificationRep;
        private readonly IUserService _userSer;
        public MailNotificationService(INotificationRepository notificationRep, IConfiguration configuration, IUserService userSer)
        {
            _configuration = configuration;
            _notificationRep = notificationRep;
            _userSer = userSer;
        }
        public async Task<bool> senMailNoticitionRegister(UserDTO user)
        {
            try
            {
                NotificationDTO notification = await _notificationRep.GetByTemplateAsync("REGISTER_NOTIFICATION", user.LanguagueCode);
                if (notification is null)
                {
                    return false;
                }
                string routeActivation = _configuration.GetSection("AppSettings:RouteAcivation").Value;
                var baseResponse = new BaseResponse<UserDTO>();
                string template = notification.Template.Replace("{prefix}", user.Prefix);
                template = template.Replace("{names}", user.FirstLastName + " " + user.Names);
                template = template.Replace("{routeActivation}", routeActivation + "?tokenActivateAccount=" + user.RememberToken);

                var UMail = _configuration.GetSection("MailProvider:Email").Value;
                var Password = _configuration.GetSection("MailProvider:Password").Value;
                var Puerto = Convert.ToInt32(_configuration.GetSection("MailProvider:Port").Value);
                var servidor = _configuration.GetSection("MailProvider:SmtpServer").Value;
                var subject = notification.Subject;
                {
                    var mail = new MailMessage()
                    {
                        IsBodyHtml = true,
                        From = new MailAddress(UMail),
                        Subject = subject,
                        Body = template
                    };
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    AlternateView html = AlternateView.CreateAlternateViewFromString(template, Encoding.UTF8, MediaTypeNames.Text.Html);
                    mail.IsBodyHtml = true;
                    mail.AlternateViews.Add(html);
                    mail.To.Add(user.Email.Trim());
                    SmtpClient smtpCliente = new SmtpClient(servidor, Puerto);
                    smtpCliente.EnableSsl = Boolean.Parse(_configuration.GetSection("MailProvider:RequiereSSL").Value);
                    smtpCliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpCliente.Timeout = 50000;
                    smtpCliente.UseDefaultCredentials = false;
                    smtpCliente.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = UMail,
                        Password = Password
                    };
                    smtpCliente.Send(mail);
                }
                baseResponse.Message = "Noticación Enviada con exito";
                baseResponse.Data = user;
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }
        public async Task<bool> senMailNoticitionResetPassword(UserDTO user)
        {
            try
            {
                var userFound = await _userSer.GetByMailOrIdidentificationAsync(user.Email);

                NotificationDTO notification = await _notificationRep.GetByTemplateAsync("RESET_PASSWORD", userFound.Data.LanguagueCode);
                if (notification is null)
                {
                    return false;
                }
                string routeDomain = _configuration.GetSection("AppSettings:PortalDomain").Value;
                var baseResponse = new BaseResponse<UserDTO>();
                string template = notification.Template.Replace("{prefix}", userFound.Data.Prefix);
                template = template.Replace("{names}", userFound.Data.FirstLastName + " " + userFound.Data.Names);
                template = template.Replace("{routeActivation}", routeDomain + "/reset-password/" + userFound.Data.Code);

                var UMail = _configuration.GetSection("MailProvider:Email").Value;
                var Password = _configuration.GetSection("MailProvider:Password").Value;
                var Puerto = Convert.ToInt32(_configuration.GetSection("MailProvider:Port").Value);
                var servidor = _configuration.GetSection("MailProvider:SmtpServer").Value;
                var subject = notification.Subject;
                {
                    var mail = new MailMessage()
                    {
                        IsBodyHtml = true,
                        From = new MailAddress(UMail),
                        Subject = subject,
                        Body = template
                    };
                    mail.BodyEncoding = Encoding.UTF8;
                    mail.SubjectEncoding = Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    AlternateView html = AlternateView.CreateAlternateViewFromString(template, Encoding.UTF8, MediaTypeNames.Text.Html);
                    mail.IsBodyHtml = true;
                    mail.AlternateViews.Add(html);
                    mail.To.Add(userFound.Data.Email.Trim());
                    SmtpClient smtpCliente = new SmtpClient(servidor, Puerto);
                    smtpCliente.EnableSsl = Boolean.Parse(_configuration.GetSection("MailProvider:RequiereSSL").Value);
                    smtpCliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpCliente.Timeout = 50000;
                    smtpCliente.UseDefaultCredentials = false;
                    smtpCliente.Credentials = new System.Net.NetworkCredential()
                    {
                        UserName = UMail,
                        Password = Password
                    };
                    smtpCliente.Send(mail);
                }
                baseResponse.Message = "Noticación Enviada con exito";
                baseResponse.Data = userFound.Data;
                return true;
            }
            catch (Exception)
            {
                return true;
            }

        }

        public async Task<bool> sendMailNotication(MailNotificactionDTO mailNotificaction)
        {
            try
            {
                var listMails = mailNotificaction.emails.Split(',');
                NotificationDTO notification = await _notificationRep.GetByTemplateAsync("PHOTO_REVIEW", "EN");
                if (notification is null)
                {
                    return false;
                }
                string routeActivation = _configuration.GetSection("AppSettings:RouteAcivation").Value;
                var baseResponse = new BaseResponse<MailNotificactionDTO>();
                string template = notification.Template.Replace("{body}", mailNotificaction.body);

                var UMail = _configuration.GetSection("MailProvider:Email").Value;
                var Password = _configuration.GetSection("MailProvider:Password").Value;
                var Puerto = Convert.ToInt32(_configuration.GetSection("MailProvider:Port").Value);
                var servidor = _configuration.GetSection("MailProvider:SmtpServer").Value;
                var subject = mailNotificaction.subject;
                var mail = new MailMessage()
                {
                    IsBodyHtml = true,
                    From = new MailAddress(UMail),
                    Subject = subject,
                    Body = template
                };
                mail.BodyEncoding = Encoding.UTF8;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                AlternateView html = AlternateView.CreateAlternateViewFromString(template, Encoding.UTF8, MediaTypeNames.Text.Html);
                mail.IsBodyHtml = true;
                mail.AlternateViews.Add(html);
                foreach (var item in listMails)
                {
                    mail.To.Add(item.Trim());
                }
                SmtpClient smtpCliente = new SmtpClient(servidor, Puerto);
                smtpCliente.EnableSsl = Boolean.Parse(_configuration.GetSection("MailProvider:RequiereSSL").Value);
                smtpCliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpCliente.Timeout = 50000;
                smtpCliente.UseDefaultCredentials = false;
                smtpCliente.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = UMail,
                    Password = Password
                };
                smtpCliente.Send(mail);
                baseResponse.Message = "Noticación Enviada con exito";
                baseResponse.Data = mailNotificaction;
                return true;
            }
            catch (Exception e)
            {
                return true;
            }

        }
        public async Task<bool> sendMailNoticationWithTreating(MailNotificactionDTO? mailNotificaction = null)
        {
            try
            {
                var listMails = mailNotificaction.emails.Split(',');
                NotificationDTO notification = await _notificationRep.GetByTemplateAsync(mailNotificaction.template, mailNotificaction.lang);
                if (notification is null)
                {
                    return false;
                }
                string routeActivation = _configuration.GetSection("AppSettings:RouteAcivation").Value;
                var baseResponse = new BaseResponse<MailNotificactionDTO>();
                string template = notification.Template.Replace("{student}", mailNotificaction.student);
                template = template.Replace("{class}", mailNotificaction.course);
                template = template.Replace("{year}", mailNotificaction.schoolYear);

                var UMail = _configuration.GetSection("MailProvider:Email").Value;
                var Password = _configuration.GetSection("MailProvider:Password").Value;
                var Puerto = Convert.ToInt32(_configuration.GetSection("MailProvider:Port").Value);
                var servidor = _configuration.GetSection("MailProvider:SmtpServer").Value;
                var subject = notification.Subject;
                var mail = new MailMessage()
                {
                    IsBodyHtml = true,
                    From = new MailAddress(UMail),
                    Subject = subject,
                    Body = template
                };
                mail.BodyEncoding = Encoding.UTF8;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                AlternateView html = AlternateView.CreateAlternateViewFromString(template, Encoding.UTF8, MediaTypeNames.Text.Html);
                mail.IsBodyHtml = true;
                mail.AlternateViews.Add(html);
                foreach (var item in listMails)
                {
                    mail.To.Add(item.Trim());
                }
                SmtpClient smtpCliente = new SmtpClient(servidor, Puerto);
                smtpCliente.EnableSsl = Boolean.Parse(_configuration.GetSection("MailProvider:RequiereSSL").Value);
                smtpCliente.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpCliente.Timeout = 50000;
                smtpCliente.UseDefaultCredentials = false;
                smtpCliente.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = UMail,
                    Password = Password
                };
                smtpCliente.Send(mail);
                baseResponse.Message = "Noticación Enviada con exito";
                baseResponse.Data = mailNotificaction;
                try
                {
                    var jsonItem = JsonConvert.SerializeObject(mailNotificaction);
                    LogManagement.Instance.write("", "ENTITY", jsonItem + "", $"BUE.Inscriptions.Application.EmailSend.{mailNotificaction.requestId}");
                }
                catch (Exception) { }
                return true;
            }
            catch (Exception)
            {
                try
                {
                    var jsonItem = JsonConvert.SerializeObject(mailNotificaction);
                    LogManagement.Instance.write("", "ENTITY", jsonItem + "", $"BUE.Inscriptions.Application.EmailSend.Error");
                }
                catch (Exception) { }
                return true;
            }

        }
    }
}
