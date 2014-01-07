using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebFormsServerCRUDBootstrap.Models
{
    /// <summary>
    /// This class is created by the user and it drives the scaffolding. Notice that you can use
    /// both data annotation attributes and the IValidatableObject interface.
    /// </summary>

    public class Movie : IValidatableObject
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "You must enter a movie title.")]
        [StringLength(30, ErrorMessage = "Movie title maximum length is 30 chracters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You must enter a movie director.")]
        public string Director { get; set; }

        [Display(Name = "Ticket Price")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "You must enter a ticket price.")]
        public decimal TicketPrice { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if (this.Director == "Stephen")
            {
                results.Add(new ValidationResult("Director named Stephen not allowed!"));
            }
            return results;
        }
    }
}