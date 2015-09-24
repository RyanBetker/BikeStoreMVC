using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BikeStore.Models
{
    public enum WheelSizeType
    {
        [Display(Name="25")]
        TwentyFive = 25,
        [Display(Name = "27")]
        TwentySeven = 27,
        [Display(Name = "29")]
        TwentyNine = 29
    }

    public enum BikeType
    {
        Mountain = 1,
        Road = 2,
        Hybrid = 3,
        Recumbant = 4
    }

    public enum FrameSizeType
    {
        Small, Medium, Large, XLarge
    }

    public class Bike : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BikeID { get; set; }
        [Required, MaxLength(10)]
        public string ModelNo { get; set; }
        [Required]
        public decimal WholesalePrice { get; set; }
        [Required]
        public decimal Price { get; set; }
        public BikeType Type { get; set; }
        public FrameSizeType FrameSize { get; set; }
        public WheelSizeType WheelSize { get; set; }

        public byte[] DisplayImage { get; set; }
        [MaxLength(10)]
        public string Color { get; set; }

        [Required]
        public int BrandID { get; set; }

        [ForeignKey("BrandID")]
        public Brand Brand { get; set; }
    }
}