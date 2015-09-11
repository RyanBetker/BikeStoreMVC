using BikeStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeStore.ViewModels
{
    public class BikeViewModel : IHasAuditColumns
    {
        public int BikeID { get; set; }
        [Required, MaxLength(10), Display(Name = "Model No")]
        public string ModelNo { get; set; }
        [Required, Display(Name = "Wholesale Price")]
        public decimal WholesalePrice { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public BikeType Type { get; set; }
        [Required, Display(Name = "Frame Size")]
        public FrameSizeType FrameSize { get; set; }
        [Required, Display(Name = "Wheel Size")]
        public WheelSizeType WheelSize { get; set; }

        [MaxLength(10)]
        public string Color { get; set; }
        
        /// <summary>
        /// Holds Brands view data, used by BrandSelectList
        /// </summary>
        public List<BrandViewModel> Brands { get; set; }
        
        [Display(Name = "Brand")]
        public int BrandID { get; set; }
        [Required, MaxLength(20), Display(Name = "Brand Name")]
        public string BrandName { get; set; }

        /// <summary>
        /// For UI dropdowns
        /// </summary>
        public IEnumerable<SelectListItem> BrandSelectList
        {
            get
            {
                if (Brands == null)
                {
                    throw new ArgumentNullException("Brand list data is not available");
                }
                return new SelectList(Brands, "BrandID", "BrandName");
            }
        }

        public BikeViewModel()
        {

        }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        [Display(Name = "Created Date"), DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Modified Date"), DisplayFormat(DataFormatString = "{0:g}", ApplyFormatInEditMode=true)]
        public DateTime? ModifiedDate { get; set; }
    }
}