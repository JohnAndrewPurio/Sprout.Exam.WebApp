using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using System.Linq;

namespace Sprout.Exam.WebApp.Data
{
    public class EmployeesInitializer
    {
        public static void Initialize(EmployeeContext context)
        {
            context.Database.EnsureCreated();

            if (context.WorkEmployees.Any())
            {
                return;
            }

            var employees = new WorkEmployee[] {
                new WorkEmployee
            {
                Birthdate = "1993-03-25",
                FullName = "Jane Doe",
                Tin = "123215413",
                TypeId = 1
            },
                new WorkEmployee
            {
                Birthdate = "1993-05-28",
                FullName = "John Doe",
                Tin = "957125412",
                TypeId = 2
            }
            };

            context.WorkEmployees.AddRange(employees);
            context.SaveChanges();
        }
    }
}
