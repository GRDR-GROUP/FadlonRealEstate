using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FadlonRealEstate.Models
{
    public class Property
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Property Name")]
        public int PropertyID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Property Name")]
        public string PropertyName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Type")]
        public string PropertyType { get; set; }
        [Required]
        public int Stock { get; set; }

        public virtual ICollection<Deal> Deals { get; set; }


    }
}