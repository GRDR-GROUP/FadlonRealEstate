using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FadlonRealEstate.Models
{
    public class Customer
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Client Name")]
        public int CustomerID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string CustomerFirstName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string CustomerLastName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PhoneNumber")]
        public int PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        public string Age { get; set; }

        public virtual ICollection<Deal> Deals { get; set; }

    }
}