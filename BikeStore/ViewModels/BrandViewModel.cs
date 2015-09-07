using BikeStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeStore.ViewModels
{
    public class BrandViewModel : IHasAuditColumns
    {
        public int BrandID { get; set; }

        [Required(ErrorMessage="Brand name is required and must be 20 characters or less")]
        [MinLength(2), MaxLength(20)]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date"), DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Modified Date"), DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime? ModifiedDate { get; set; }
    }
}