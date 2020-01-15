using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcSite.Models
{
    public class Customer
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string CardType { get; set; }

        public bool FuturePromotions { get; set; }

        public DateTime DateCreated { get; set; }
    }
}