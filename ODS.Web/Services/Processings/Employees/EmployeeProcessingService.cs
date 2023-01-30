using ODS.Web.Services.Foundations.Employees;

namespace ODS.Web.Services.Processings.Employees
{
    public class EmployeeProcessingService : IEmployeeProcessingService
    {
        private readonly IEmployeeService employeeService;
        private readonly IWebHostEnvironment hostingEnvironment;

        public EmployeeProcessingService(IEmployeeService employeeService,
            IWebHostEnvironment hostingEnvironment)
        {
            this.employeeService = employeeService;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async ValueTask UploadFileAndGetFilePath(IFormFile postedFile)
        {
            string pathOfFile = String.Empty;

            if (postedFile.Length > 0)
            {
                pathOfFile = Path.Combine(this.hostingEnvironment.WebRootPath, "uploads", postedFile.FileName);

                using (var stream = new FileStream(pathOfFile, FileMode.CreateNew, FileAccess.Write))
                {
                    await postedFile.CopyToAsync(stream);
                }
            }
        }
    }
}
