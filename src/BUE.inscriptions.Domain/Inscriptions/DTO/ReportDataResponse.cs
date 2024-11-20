using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUE.Inscriptions.Domain.Inscriptions.DTO
{
 
    public class ReportDataResponse
    {
        public string UrlReport { get; set; }
    }

    public class ApiReportResponse
    {
        public ReportDataResponse Data { get; set; }
    }
}
