using Sprout.Exam.Common.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Sprout.Exam.WebApp.Models
{
    public class WorkEmployee : ISoftDelete
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Tin { get; set; }
        public int TypeId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
    }

    public interface IEmployee
    {
        public decimal GetSalary(decimal days);
    }

    public class Regular : IEmployee
    {
        const int BASE_SALARY = 20000;
        const decimal TAX_DEDUCTION = 0.12M;

        decimal IEmployee.GetSalary(decimal absentDays) => salary.ComputeSalary(absentDays, TAX_DEDUCTION);

        Salary salary => new Salary(BASE_SALARY);
    }

    public class Contractual : IEmployee
    {
        const int BASE_SALARY = 500;
        decimal IEmployee.GetSalary(decimal workedDays) => salary.ComputeSalary(workedDays);

        Salary salary => new Salary(BASE_SALARY);
    }

    public class EmployeeFactory
    {
        public static IEmployee GetEmployee(EmployeeType type)
        {
            return type switch
            {
                EmployeeType.Regular => new Regular(),
                EmployeeType.Contractual => new Contractual(),
                _ => throw new ArgumentException("Employee Type not avaiable"),
            };
        }
    }

    public interface ISoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }

        public void Undo()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
