using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BUE.Inscriptions.Domain.Entity
{

    public class RolePermission
    {

        public int? CodigoRol { get; set; }
        public string? NombreRol { get; set; }
        public string? DescripcionRol { get; set; }
        public bool? EstadoRol { get; set; }
        public DateTime? CreatedAtRol { get; set; }
        public DateTime? UpdatedAtRol { get; set; }
        public DateTime? DeletedAtRol { get; set; }
        public int? codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Idioma { get; set; }
        public bool? Estado { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Filtro { get; set; }
        public string? TipoObjeto { get; set; }
        public string? Objeto { get; set; }
        public string? Configuracion { get; set; }
    }
}
