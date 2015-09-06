using BikeStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikeStore.ViewModels
{
    public class BrandViewModel : BaseModel
    {
        public int BrandID { get; set; }

        [Required(ErrorMessage="Brand name is required and must be 20 characters or less"), MinLength(2), MaxLength(20)]
        public string BrandName { get; set; }
    }
}