using AspNetCore.Reporting;
//using ProjModels;
using System.Text;



namespace ReportServiceProject.Service
{
    public class ReportService : IReportService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ReportService(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        public byte[] CreateReportFile(string pathRdlc)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            LocalReport report = new LocalReport(_hostingEnvironment.ContentRootPath + "\\Reports\\Report2.rdlc");
            List<Class1> lst = new List<Class1>();
            lst.Add(new Class1
            {
                FirstName = "maeeeeeehesh",
                LastName = "mohan",
                Email = "m@m.com",
                Phone = "11111111"
            });

            report.AddDataSource("DataSet1", lst);
            var result = report.Execute(RenderType.Pdf, 1);
            return result.MainStream;
        }


        
    }
    public class Class1
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
