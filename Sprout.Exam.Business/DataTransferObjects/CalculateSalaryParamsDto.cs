using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class CalculateSalaryParamsDto
    {
        public int id { get; set; }
        public decimal absentDays { get; set; }
        public decimal workedDays { get; set; }
    }
}
