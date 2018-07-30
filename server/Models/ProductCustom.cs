using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public partial class Product {

        public Product() {
            RowStatus = "NoChange";
        }
        [NotMapped]
        public string RowStatus { get; set; }
    }
}
