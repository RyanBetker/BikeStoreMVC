﻿using BikeStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BikeStore.ViewModels
{
    public class BikeViewModel : BaseModel
    {
        public int BikeID { get; set; }
        public string ModelNo { get; set; }
        public BikeType Type { get; set; }
        public FrameSizeType FrameSize { get; set; }
        public WheelSizeType WheelSize { get; set; }

        public string Color { get; set; }
        private readonly List<Brand> _brands;

        [Display(Name = "Brand")]
        public int BrandID { get; set; }
        public string BrandName { get; set; }

        public IEnumerable<SelectListItem> AvailableBrands 
        {
            get
            {
                return new SelectList(_brands, BrandID, "BrandName");
            }

        }
    }
}