using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FadlonRealEstate.Models
{
    public class Deal
    {
        public int DealID { get; set; }

        public int CustomerID { get; set; }
        public int PropertyID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Property Property { get; set; }

        public bool Active { get; set; }

    }
}