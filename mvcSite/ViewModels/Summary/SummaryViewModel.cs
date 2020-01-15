using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using mvcSite.ViewModels.Summary;

namespace mvcSite.ViewModels.Summary
{
    public class SummaryViewModel
    {
        public enum YesNo
        {
            Yes,
            No
        }

        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string CardNumber { get; set; }

        [Required]
        public string CardType { get; set; }

        public YesNo FuturePromotions { get; set; }

        public IEnumerable<SummaryRow> SummaryRows { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Total { get; set; }
    }
}