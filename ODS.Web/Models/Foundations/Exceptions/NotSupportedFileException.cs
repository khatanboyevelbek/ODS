using Xeptions;

namespace ODS.Web.Models.Foundations.Exceptions
{
    public class NotSupportedFileException : Xeption 
    {
        public NotSupportedFileException()
            : base(message: "File type is not supported. Try again")
        { }
    }
}
