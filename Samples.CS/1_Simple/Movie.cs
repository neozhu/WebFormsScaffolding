using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samples.Simple
{
    public class Movie
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Movie title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Movie director is required.")]
        public string Director { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }
    }

}