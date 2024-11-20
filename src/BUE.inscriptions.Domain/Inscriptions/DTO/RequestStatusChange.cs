using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Inscriptions.DTO
{
    public class RequestStatusChange
    {
        public List<int> Ids { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }

    }
}
