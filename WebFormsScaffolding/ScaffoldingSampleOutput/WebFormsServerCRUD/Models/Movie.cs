using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebFormsServerCRUD.Models
{
    public class Movie
    {
        public int Id { get; set; }

        
        [Required(ErrorMessage="You must enter a movie title.")]
        [StringLength(30, ErrorMessage="Movie title maximum length is 30 chracters.")]
        public string Title { get; set; }

        [Required(ErrorMessage="You must enter a movie director.")]
        public string Director { get; set; }

        [Display(Name="Ticket Price")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage="You must enter a ticket price.")]
        public decimal TicketPrice { get; set; }
    }
}