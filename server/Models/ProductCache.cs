using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utilities;
using Utilities.Cache;

namespace WebApi.Models
{
    public class ProductCache : MemoryCache<string, Product> {
        public ProductCache()
           : base("PRODUCT_CACHE") {

        }
        public static ProductCache Instance {
            get { return SingletonProvider<ProductCache>.Instance; }
        }
    }
}
