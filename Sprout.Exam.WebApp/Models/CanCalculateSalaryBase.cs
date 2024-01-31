using Xunit;

namespace Sprout.Exam.WebApp.Models.UnitTests
{
    public class CanCalculateSalaryBase
    {

        [Fact]
        public void CalculateRegularEmployeeSalary()
        {
            Employee employee = new Regular();

            Assert.True(employee.GetSalary(1) == 16690.91M);
        }
    }
}