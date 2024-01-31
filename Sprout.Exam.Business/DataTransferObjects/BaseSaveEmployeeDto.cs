using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sprout.Exam.Business.DataTransferObjects
{
    public class BaseSaveEmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Tin { get; set; }
        public DateTime Birthdate { get; set; }
        public int TypeId { get; set; }
    }
}
