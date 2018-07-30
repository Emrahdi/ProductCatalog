using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public partial class Product
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        //public byte[] Photo { get; set; }
        public decimal Price { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastUpdatedDate { get; set; }
        public string LastUpdatedUser { get; set; }
    }
}
