using Moq;
using ODS.Web.Brokers.Loggings;
using ODS.Web.Brokers.Storages;
using ODS.Web.Models.Foundations;
using ODS.Web.Services.Foundations.Employees;
using Tynamix.ObjectFiller;

namespace ODS.Tests.Unit.Services.Foundations.Employees
{
    public partial class EmployeeServiceTest
    {
        public readonly Mock<IStorageBroker> storageBrokerMock;
        public readonly IEmployeeService employeeServiceMock;

        public EmployeeServiceTest()
        {
            this.storageBrokerMock = new Mock<IStorageBroker>();

            this.employeeServiceMock = 
                new EmployeeService(this.storageBrokerMock.Object);
        }

        private static Employee CreateRandomEmployee() =>
            CreateEmployeeFiller(date: GetRandomDateTimeOffset()).Create();

        private static DateTimeOffset GetRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static Filler<Employee> CreateEmployeeFiller(DateTimeOffset date)
        {
            var filler = new Filler<Employee>();
            filler.Setup().OnType<DateTimeOffset>().Use(date);

            return filler;
        }
    }
}
