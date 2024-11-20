using BUE.Inscriptions.Domain.Inscriptions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Inscriptions.DTO
{
    public class StudentFamiliesDTO
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int PersonId { get; set; }
        public int RelationTypeId { get; set; }
        public bool IsPrincipalRepresentative { get; set; }
        public bool IsEconomicRepresentative { get; set; }
        public bool IsLegalRepresentative { get; set; }
        public bool IsResponsibleRepresentative { get; set; }
        public bool LivesWithStudent { get; set; }
        public virtual Person? Person { get; set; }
    }
}
