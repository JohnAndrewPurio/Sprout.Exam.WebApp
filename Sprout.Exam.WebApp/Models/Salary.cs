using System;

namespace Sprout.Exam.WebApp.Models
{
    public class Salary
    {
        public const int MONTH_WORK_DAYS = 22;

        public Salary(int baseSalary)
        {
            salary = baseSalary;
        }

        public decimal ComputeSalary(decimal workedDays)
        {
            var remainingSalary = salary * workedDays;

            return Math.Round(remainingSalary, 2);
        }

        public decimal ComputeSalary(decimal absentDays, decimal taxDeduction)
        {
            var workPercentage = absentDays / MONTH_WORK_DAYS;
            var remainingSalary = salary * (1 - workPercentage - taxDeduction);

            return Math.Round(remainingSalary, 2);
        }

        private readonly decimal salary;
    }
}
