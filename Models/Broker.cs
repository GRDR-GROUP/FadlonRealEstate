using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FadlonRealEstate.Models
{
    public class Broker
    {
        [Required]
        public int BrokerID { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Broker Name")]
        public string BrokerName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Password")]
        public string BrokerPassword { get; set; }

    }
}