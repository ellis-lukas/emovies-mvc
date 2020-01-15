using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcSite.ViewModels.Order
{
    public class OrderViewModel
    {
        public enum YesNo
        {
            Yes,
            No
        }

        public enum CardTypes
        {
            Mastercard,
            Visa,
            [Display(Name = "American Express")]
            AmericanExpress,
            Discover
        }

        [RegularExpression(@"\s*[a-zA-Z]+(\.){0,1}((\s+[a-zA-Z]+(-){0,1}[a-zA-Z]+)|(\s+[a-zA-Z]+))*\s*", ErrorMessage = "Please enter a valid name")]
        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(100)]
        public string Name { get; set; }

        [RegularExpression(@"\s*[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}\s*", ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "Please enter your email address")]
        [StringLength(254)]
        public string Email { get; set; }

        [RegularExpression(@"\s*([0-9]\s*){15,19}", ErrorMessage = "Please enter a valid card number")]
        [Required(ErrorMessage = "Please enter your card number")]
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        [Required]
        [Display(Name = "Card Type")]
        public CardTypes CardType { get; set; }

        [Required]
        [Display(Name = "Card Type")]
        public bool FuturePromotions { get; set; }
    }
}