using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Samples.Enumerations
{
    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Url)]
        [Display(Name="Home Page URL:")]
        public string HomePage { get; set; }

        [Required(ErrorMessage="You must enter a salary level")]
        public SalaryLevel Salary { get; set; }

        // selecting a region is optional
        public Region? Region { get; set; }
    }
}