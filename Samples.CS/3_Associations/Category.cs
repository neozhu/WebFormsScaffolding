using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samples.Associations
{
    public class Category
    {
        public int Id { get; set; }

        [Display]  // Use the display attribute to indicate the data text field for the DropDownList
        public string Name { get; set; }
    }
}