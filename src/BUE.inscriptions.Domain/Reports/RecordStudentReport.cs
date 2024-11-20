using BUE.Inscriptions.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Reports
{
    public class RecordStudentReport
    {
        // Properties from EstudianteAnioLectivoPM
        public int codigoEstudianteAnioLectivo { get; set; }
        public int codigoAnioLectivo { get; set; }
        public int codigoEstudiante { get; set; }
        public string? nombreCompleto { get; set; }
        public string? identificacion { get; set; }
        public byte[]? foto { get; set; }
        public string direccion { get; set; }
        public DateTime? fechaMatricula { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string? ciudadNacimiento { get; set; }
        public string? cursoGrado { get; set; }
        public string? nivel { get; set; }
        public string? sexo { get; set; }
        public string? anioLectivo { get; set; }
        public string? nacionalidad { get; set; }
        public string? paisNacimiento { get; set; }
        public string? parroquia { get; set; }
        public string? canton { get; set; }
        public int? codigoMatricula { get; set; }
        public string? urlImagen { get; set; }
        public string? autorizaPublicarImagen { get; set; }
        public string? nombreEstudiante { get; set; }
        public string? esRepetidor { get; set; }
        public string? otraNacionalidad { get; set; }
        public string? pagador { get; set; }
        public string? cursoGradoAnterior { get; set; }
        public string? registroRepresentante2 { get; set; }
        public string? razonNoRegistro { get; set; }
        public string? autorizaSalida { get; set; }
        public string? noAutorizadoSalirSolo { get; set; }
        public string? autorizacionSalidaHorasLibres { get; set; }
        public string? apellidosEstudiante { get; set; }
        public string? edad { get; set; }
        public string? tomarTransporte { get; set; }
        public string? preguntaInstitucion { get; set; }
        public string? preguntaRetiro { get; set; }
        public string? tipoSangre { get; set; }
        public string? contactoEmergencia { get; set; }
        public string? nombreIdiomaAlterno { get; set; }
        public string? nombreCompletoCursoGrado { get; set; }
        public string? urlJustificateNoRegistro { get; set; }



        // Properties from Parentesco (Alias 'PR')
        public int? pR_CodigoEstudianteAnioLectivo { get; set; }
        public string? pR_Identificacion { get; set; }
        public string? pR_Nombres { get; set; }
        public DateTime? pR_FechaNacimiento { get; set; }
        public string? pR_Nacionalidad { get; set; }
        public string? pR_EstadoCivil { get; set; }
        public string? pR_PROFESION { get; set; }
        public string? pR_OCUPACION { get; set; }
        public string? pR_LugarTrabajo { get; set; }
        public string? pR_DireccionTrabajo { get; set; }
        public string? pR_TelefonoTrabajo { get; set; }
        public string? pR_TelefonoCasa { get; set; }
        public string? pR_Direccion { get; set; }
        public string? pR_TelefonoCelular { get; set; }
        public string? pR_Email { get; set; }
        public string? pR_RetirarEst { get; set; }
        public string? pR_TipoRepresentante { get; set; }
        public string? pR_Parentesco { get; set; }
        public string? pR_UrlImage { get; set; }
        public string? pR_Apellidos { get; set; }
        public string? pR_CiudadNacimiento { get; set; }


        // Properties from Parentesco (Alias 'SR')
        public int? sR_CodigoEstudianteAnioLectivo { get; set; }
        public string? sR_Identificacion { get; set; }
        public string? sR_Nombres { get; set; }
        public DateTime? sR_FechaNacimiento { get; set; }
        public string? sR_Nacionalidad { get; set; }
        public string? sR_EstadoCivil { get; set; }
        public string? sR_PROFESION { get; set; }
        public string? sR_OCUPACION { get; set; }
        public string? sR_LugarTrabajo { get; set; }
        public string? sR_DireccionTrabajo { get; set; }
        public string? sR_TelefonoTrabajo { get; set; }
        public string? sR_Direccion { get; set; }
        public string? sR_TelefonoCelular { get; set; }
        public string? sR_TelefonoCasa { get; set; }
        public string? sR_Email { get; set; }
        public string? sR_RetirarEst { get; set; }
        public string? sR_TipoRepresentante { get; set; }
        public string? sR_Parentesco { get; set; }
        public string? sR_UrlImage { get; set; }
        public string? sR_Apellidos { get; set; }
        public string? sR_CiudadNacimiento { get; set; }

        // Properties from ContactoPM (Alias 'PRL')
        public int? pRL_CodigoEstudianteAnioLectivo { get; set; }
        public string? pRL_Identificacion { get; set; }
        public string? pRL_Nombres { get; set; }
        [NotMapped]
        public IEnumerable<RecordAuthPeople>? personasAutorizadas { get; set; } 
        [NotMapped]
        public MedicalRecord? medicalRecord { get; set; }
    }

}
