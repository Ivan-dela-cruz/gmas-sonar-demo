namespace BUE.Inscriptions.Shared.Utils
{
    public class MessageUtil
    {
        public static MessageUtil Instance { get; } = new MessageUtil();
        public string Success { get; set; } = "Proceso completado exitosamente";
        public string Identification_Already_Exist { get; set; } = "El número de identificación actualmente ya esta registrado";
        public string Error { get; set; } = "Se ha producido en error";
        public string Warning { get; set; } = "Existe incosistencias en los datos recibidos";
        public string Info { get; set; } = "Registros recuperados exitosamente";
        public string Created { get; set; } = "Registro creado exitosamente";
        public string Updated { get; set; } = "Registro actualizado exitosamente";
        public string Deleted { get; set; } = "Registro eliminado exitosamente";
        public string NotFound { get; set; } = "Recurso no encontrado";
        public string Found { get; set; } = "Recurso recuperado exitosamnete";
        public string Empty { get; set; } = "No se encontro un valor válido, posiblemente su contenido esta vacio o nulo";
        public string UserNotFound { get; set; } = "Usuario no encontrado";
        public string FileNotUpload { get; set; } = "No se pudo cargar el archivo, al repositorio remoto";
        public string FileUpload { get; set; } = "Archivo cargado exitosamente al repositorio remoto";
        public string FileNotFound { get; set; } = "No se encontró el archivo";
        public string EmailExist { get; set; } = "El email ingresado actualmente ya esta en uso";
        public string PasswordInvalid { get; set; } = "El Email o la contraseña no coinciden con nuestros registros";
        public string USER_ALREADY_EXIST { get; set; } = "USER_ALREADY_EXIST";
        public string EMAIL_ALREADY_EXIST { get; set; } = "EMAIL_ALREADY_EXIST";
        public string USER_NOT_FOUND { get; set; } = "USER_NOT_FOUND";
        public string USER_NOT_CREATED { get; set; } = "USER_NOT_CREATED";
        public string WRONG_PASSWORD_OR_EMAIL { get; set; } = "WRONG_PASSWORD_OR_EMAIL";
        public string TOKEN_EXPIRED { get; set; } = "TOKEN_EXPIRED";
        public string TOKEN_NOT_VALID { get; set; } = "TOKEN_NOT_VALID";
        public string ERROR_DATA { get; set; } = "ERROR_DATA";
        public string RECORD_NOT_FOUND { get; set; } = "RECORD_NOT_FOUND";
        public string RECORD_NOT_CREATED { get; set; } = "RECORD_NOT_CREATED";
        public string FILE_NOT_UPLOAD { get; set; } = "FILE_NOT_UPLOAD";
        public string FILE_UPLOAD { get; set; } = "FILE_UPLOAD";
        public string FILE_NOT_FOUND { get; set; } = "FILE_NOT_FOUND";
        public string FILE_FOUND { get; set; } = "FILE_FOUND";
        public string UNIQUE_KEY { get; set; } = "UNIQUE_KEY";
        public string FOREIGN_KEY { get; set; } = "FOREIGN_KEY";
        public string SUCCESS { get; set; } = "SUCCESS";
        public string UniqueKey { get; set; } = "A record with these unique data already exists in this company.";
        public string SqlUpdateError { get; set; } = "An error occurred while saving changes to the database.  ";
        public string SqlForeignKeyError { get; set; } = "An error occurred while saving changes to the database due to a foreign key constraint violation. Please ensure that all related records exist and try again.";

    }

}
