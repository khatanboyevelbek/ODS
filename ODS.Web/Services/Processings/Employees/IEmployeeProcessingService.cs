namespace ODS.Web.Services.Processings.Employees
{
    public interface IEmployeeProcessingService
    {
        ValueTask UploadFileAndGetFilePath(IFormFile postedFile);
    }
}
