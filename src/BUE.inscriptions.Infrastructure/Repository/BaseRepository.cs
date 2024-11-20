using BUE.Inscriptions.Shared.Utils;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace BUE.Inscriptions.Infrastructure.Repository
{
    public class BaseRepository
    {
        public string MessageException { get; set; }
        public string CodeException { get; set; }
        public static T MapProperties<T>(T newEntity, T olderEntity)
        {
            if (newEntity == null || olderEntity == null)
            {
                throw new ArgumentNullException("Las entidades no pueden ser nulas");
            }

            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object newValue = property.GetValue(newEntity);
                object oldValue = property.GetValue(olderEntity);
                if (newValue != null && !newValue.Equals(property.PropertyType.GetDefaultMembers()))
                {
                    property.SetValue(olderEntity, newValue);
                }
            }
            return olderEntity;
        }
        public  void HandlerOperationException(Exception ex)
        {
            MessageException = MessageUtil.Instance.SqlUpdateError + ex.Message;
            CodeException = MessageUtil.Instance.ERROR_DATA;
            if (ex.InnerException != null)
                MessageException = MessageUtil.Instance.SqlUpdateError + ex.InnerException.Message;
            if (ex.InnerException.Message.Contains("FOREIGN KEY"))
            {
                MessageException = MessageUtil.Instance.SqlForeignKeyError;
                CodeException = MessageUtil.Instance.FOREIGN_KEY;
            }
            if (ex.InnerException.Message.Contains("UNIQUE KEY"))
            {
                MessageException = MessageUtil.Instance.UniqueKey;
                CodeException = MessageUtil.Instance.UNIQUE_KEY;
            }
        }
    }
}
