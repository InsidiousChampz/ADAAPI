using System.Collections.Generic;

namespace TEMPLETEAPI.Models.Report
{
    public class ReportHeader
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ReportDetail> ReportDetails { get; set; }
    }
}