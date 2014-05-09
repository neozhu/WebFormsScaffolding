using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samples.Validation
{
    public class Book : IValidatableObject
    {

        public int Id { get; set; }

        [Required(ErrorMessage="You must enter a book title.")]
        public string Title { get; set; }

        [Range(0, 500, ErrorMessage="Units in stock must be between 0 and 500.")]
        [Display(Name="Units In Stock")]
        public int UnitsInStock { get; set; }

        [Required(ErrorMessage="You must enter comments about the book.")]
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        [Display(Name="In Stock")]
        public bool InStock { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            if (!String.IsNullOrWhiteSpace(this.Comments))
            {
                if (this.Comments.Contains("great"))
                {
                    errors.Add(new ValidationResult("You can't enter the word great in comments!", new string[] {"Comments"}));
                }
            }
            return errors;
        }
    }
}