using BUE.Inscriptions.Domain.Entity.DTO;

namespace BUE.Inscriptions.Application.Interfaces
{
    public interface IMailNotification
    {
        Task<bool> senMailNoticitionRegister(UserDTO user);
        Task<bool> sendMailNotication(MailNotificactionDTO mailNotificaction);
        Task<bool> sendMailNoticationWithTreating(MailNotificactionDTO mailNotificaction);
        Task<bool> senMailNoticitionResetPassword(UserDTO user);
    }
}
