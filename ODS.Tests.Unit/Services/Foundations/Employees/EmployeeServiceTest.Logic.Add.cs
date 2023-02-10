using FluentAssertions;
using Force.DeepCloner;
using Moq;
using ODS.Web.Models.Foundations;
using Xunit;

namespace ODS.Tests.Unit.Services.Foundations.Employees
{
    public partial class EmployeeServiceTest
    {
        [Fact]
        public async Task ShouldAddEmployeeAsync()
        {
            //given
            Employee randomEmployee = CreateRandomEmployee();
            Employee inputEmployee = randomEmployee;
            Employee storageEmployee = inputEmployee;
            Employee expectedStorageEmployee = storageEmployee.DeepClone();

            this.storageBrokerMock.Setup(broker =>
                broker.InsertEmployeeAsync(inputEmployee)).ReturnsAsync(storageEmployee);
            
            //when
            Employee actualEmployee = 
                await this.employeeServiceMock.AddEmployeeAsync(inputEmployee);

            //then
            actualEmployee.Should().BeEquivalentTo(expectedStorageEmployee);

            this.storageBrokerMock.Verify(broker => 
                broker.InsertEmployeeAsync(It.IsAny<Employee>()), Times.Once());

            this.storageBrokerMock.VerifyNoOtherCalls();
        }
    }
}
