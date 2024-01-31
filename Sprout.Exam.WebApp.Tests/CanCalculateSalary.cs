using Sprout.Exam.WebApp.Models;
using Xunit;

namespace Sprout.Exam.WebApp.Tests
{
    public class CanCalculateSalary
    {
        [Fact]
        public void CalculatesContractualEmployeeSalary()
        {
            IEmployee employee = new Contractual();
            decimal workedDays = 15.5M;

            var salary = employee.GetSalary(workedDays);

            Assert.True(salary == 7750.0M);
        }

        [Fact]
        public void CalculateRegularEmployeeSalary()
        {
            IEmployee employee = new Regular();
            decimal absentDays = 1;

            var salary = employee.GetSalary(absentDays);

            Assert.True(salary == 16690.91M);
        }
    }
}
