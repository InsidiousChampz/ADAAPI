namespace TEMPLETEAPI.Models.Report
{
    public class ReportDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //ReportHeaderID is a FK from Report Header ===
        public int ReportHeaderId { get; set; }
        public ReportHeader ReportHeader { get; set; }
        //==============================================
    }
}