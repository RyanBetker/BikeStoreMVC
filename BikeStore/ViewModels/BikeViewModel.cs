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
        [Required, MaxLength(10)]
        public string ModelNo { get; set; }
        [Required, Display(Name = "Wholesale Price")]
        public decimal WholesalePrice { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public BikeType Type { get; set; }
        [Required]
        public FrameSizeType FrameSize { get; set; }
        [Required]
        public WheelSizeType WheelSize { get; set; }

        [MaxLength(10)]
        public string Color { get; set; }
        private readonly List<Brand> _brands;

        [Display(Name = "Brand")]
        public int BrandID { get; set; }
        [Required, MaxLength(20)]
        public string BrandName { get; set; }

        public IEnumerable<SelectListItem> AvailableBrands 
        {
            get
            {
                if (_brands == null)
                {
                    throw new ArgumentNullException("Brand list data is not available");
                }
                return new SelectList(_brands, BrandID, "BrandName");
            }
        }

        //public BikeViewModel()
        //{

        //}

        //public BikeViewModel(List<Brand> availableBrands)
        //{
        //    _brands = availableBrands;
        //}

        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}