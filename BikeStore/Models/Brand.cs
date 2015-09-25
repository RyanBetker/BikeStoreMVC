using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BikeStore.Models
{
    public class Brand : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int BrandID { get; set; }
        [Required, MaxLength(20)]
        [Index(IsUnique=true)]
        public string BrandName { get; set; }

    }
}