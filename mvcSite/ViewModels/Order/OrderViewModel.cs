using System.ComponentModel.DataAnnotations;

namespace mvcSite.ViewModels.Order
{
    public class OrderViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(100)]
        public string Name { get; set; }

        [RegularExpression(@"\s*[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}\s*", ErrorMessage = "Please enter a valid email address")]
        [Required(ErrorMessage = "Please enter your email address")]
        [StringLength(254)]
        public string Email { get; set; }

        [Display(Name = "Credit card number")]
        [RegularExpression(@"\s*([0-9]\s*){15,19}", ErrorMessage = "Please enter a valid card number")]
        [Required(ErrorMessage = "Please enter your card number")]
        public string CardNumber { get; set; }

        [Display(Name = "Credit card type")]
        [Required]
        public string CardType { get; set; }

        [Required]
        public bool FuturePromotions { get; set; }
    }
}