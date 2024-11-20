using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Elecctions.DTO
{
    public class DHontResultDTO<T>
    {
        public string OrganizationName { get; set; }
        public int Votes { get; set; }
        public int Seats { get; set; }
        public T? Reference { get; set; }

        public DHontResultDTO(string listName, int votes)
        {
            OrganizationName = listName;
            Votes = votes;
            Seats = 0;
        }
        
    }
}
