namespace ReportServiceProject.Service
{
    public interface IReportService
    {
        byte[] CreateReportFile(string pathRdlc);
    }
}
