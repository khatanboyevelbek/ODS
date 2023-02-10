using System.Xml.Serialization;
using ExcelDataReader;
using ODS.Web.Brokers.Loggings;
using ODS.Web.Models.Foundations;
using ODS.Web.Models.Foundations.Exceptions;
using ODS.Web.Models.Orchestrations.Files;
using ODS.Web.Services.Foundations.Employees;

namespace ODS.Web.Services.Processings.Employees
{
    public class EmployeeProcessingService : IEmployeeProcessingService
    {
        private readonly IEmployeeService employeeService;
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly ILoggingBroker loggingBroker;

        public EmployeeProcessingService(IEmployeeService employeeService,
            IWebHostEnvironment hostingEnvironment, ILoggingBroker loggingBroker)
        {
            this.employeeService = employeeService;
            this.hostingEnvironment = hostingEnvironment;
            this.loggingBroker = loggingBroker;
        }

        public async Task<int> ImportExternalFileToTable(IFormFile postedFile)
        {
            string uploadedFilePath = await UploadFileAndGetFilePath(postedFile);

            using(var stream = File.Open(uploadedFilePath, FileMode.Open, FileAccess.Read))
            {
                using(var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while(reader.Read())
                    {
                        var employee = new Employee()
                        {
                            Id = Guid.NewGuid(),

                            PayrollNumber = 
                                string.IsNullOrEmpty(reader.GetString(0)) ? string.Empty : reader.GetString(0),

                            FirstName = 
                                string.IsNullOrEmpty(reader.GetString(1)) ? string.Empty : reader.GetString(1),

                            LastName = 
                                string.IsNullOrEmpty(reader.GetString(2)) ? string.Empty : reader.GetString(2),

                            DateOfBirth = reader.GetDateTime(3),

                            PhoneNumber = 
                                string.IsNullOrEmpty(reader.GetValue(4).ToString()) ? string.Empty : reader.GetValue(4).ToString(),

                            Address1 = 
                                string.IsNullOrEmpty(reader.GetString(5)) ? string.Empty : reader.GetString(5),

                            Address2 = 
                                string.IsNullOrEmpty(reader.GetString(6)) ? string.Empty : reader.GetString(6),

                            PostalCode = 
                                string.IsNullOrEmpty(reader.GetValue(7).ToString()) ? string.Empty : reader.GetValue(7).ToString(),

                            Email = 
                                string.IsNullOrEmpty(reader.GetString(8)) ? string.Empty : reader.GetString(8),

                            StartedDate = reader.GetDateTime(9),
                            CreatedDate = DateTimeOffset.Now,
                            UpdatedDate = DateTimeOffset.Now,
                        };

                        await this.employeeService.AddEmployeeAsync(employee);
                    }

                    return reader.RowCount;
                }
            }
        }

        private async Task<string> UploadFileAndGetFilePath(IFormFile postedFile)
        {
            string pathOfFile = string.Empty;
            List<string> supportedTypes = new() { ".xls", ".xlsx" };
            FileInfo fileInfo = new FileInfo(postedFile.FileName);

            if (supportedTypes.Contains(fileInfo.Extension))
            {
                string fileName = $"{Guid.NewGuid()}{postedFile.FileName}";

                pathOfFile = 
                    Path.Combine(this.hostingEnvironment.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(pathOfFile, FileMode.CreateNew, FileAccess.Write))
                {
                    await postedFile.CopyToAsync(stream);
                }

                return pathOfFile;
            }

            throw new NotSupportedFileException();
        }

        public FileConfiguration ConvertSqlDataToXmlFile()
        {
            string generateXmlFileName = string.Format("{0}.xml", Guid.NewGuid());

            string pathOfXmlFile =
                Path.Combine(this.hostingEnvironment.WebRootPath, @"downloads\xml", generateXmlFileName);

            IQueryable<Employee> employees = 
                this.employeeService.RetrieveAllEmployees();

            using(var streamWriter = new StreamWriter(pathOfXmlFile))
            {
                var serializer = new XmlSerializer(employees.GetType());
                serializer.Serialize(streamWriter, employees);
            }

            return new FileConfiguration 
            { 
                FileName = generateXmlFileName, 
                FilePath = pathOfXmlFile 
            };
        }

        public IQueryable<Employee> RetrieveAllEmployees() =>
            this.employeeService.RetrieveAllEmployees();
    }
}
