using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Reports
{
    public class RecordAuthPeople
    {
        public string? apellidos { get; set; }
        public string? nombres { get; set; }
        public string? email { get; set; }
        public string? direccion { get; set; }
        public string? codigoPostal { get; set; }
        public string? identificacion { get; set; }
        public string? telefonoCelular { get; set; }
        public string? urlImage { get; set; }
        public string? parentesco { get; set; }
        public int? codigoSolicitud { get; set; }
    }

}
