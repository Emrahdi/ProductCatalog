using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Helpers;
using WebApi.Models;
using Microsoft.Extensions.Options;
using log4net;
using System.Reflection;
using WebApi.Entities;
using WebApi.Dtos;

namespace WebApi.Controllers {
    [Authorize]
    [Route("[controller]")]
    public class ProductController : Controller {

        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly AppSettings appSettings;
        private readonly ProductCatalogContext productCatalogcontext;
        public ProductController(
           IOptions<AppSettings> appSettings) {
            this.appSettings = appSettings.Value;
            productCatalogcontext = new ProductCatalogContext();
        }

        public string ExecutingUserName {
            get {
                return ((Microsoft.AspNetCore.Http.DefaultHttpContext)((Microsoft.AspNetCore.Http.Internal.DefaultHttpRequest)this.Request).HttpContext).User.Identities.First().Name;
            }
        }

        //[HttpPost("/product/GetProduct")]
        //public IActionResult GetProducts([FromBody]Product productDto) {

        //    //string productCode = productDto.Code;
        //    //List<Product> lk = new List<Product>();
        //    //Product pp = new Product() { Code = "1", Name = "123123", key = "1", LastUpdatedDate = DateTime.Now };
        //    //Product pp1 = new Product() { Code = "2", Name = "123123", key = "2", LastUpdatedDate = DateTime.Now };
        //    //Product pp2 = new Product() { Code = "3", Name = "123123", key = "3", LastUpdatedDate = DateTime.Now };
        //    //lk.Add(pp);
        //    //lk.Add(pp1);
        //    //lk.Add(pp2);
        //    //return GenerateProductResult(lk);
        //    //string productCode = string.IsNullOrEmpty(productDto.Code) ? string.Empty : productDto.Code;
        //    //if (appSettings.IsMemoryCachingEnabled) {
        //    //    Product product;
        //    //    if (ProductCache.Instance.TryGetValue(productCode, out product)) {
        //    //        return Ok(product);
        //    //    } else {
        //    //        var products = GetProductFromDb(productCode);
        //    //        if (products != null) {
        //    //            foreach (var p in products) {
        //    //                ProductCache.Instance.Add(p.Code, p);
        //    //            }
        //    //        }
        //    //        return GenerateProductResult(products);
        //    //    }
        //    //} else {
        //    //    var products = GetProductFromDb(productDto.Code);
        //    //    return GenerateProductResult(products);
        //    //}
        //}
        [HttpPost("/product/SearchProducts")]
        public IActionResult SearchProducts([FromBody]Product productDto) {
            List<Product> products = new List<Product>();
            var cleanProductName = GetCleanString(productDto.Name);
            var cleanProductCode = GetCleanString(productDto.Code);
            if (string.IsNullOrEmpty(cleanProductName) && string.IsNullOrEmpty(cleanProductCode))
                return NoContent();
            if (appSettings.IsMemoryCachingEnabled) {
                foreach (var key in ProductCache.Instance.Keys) {
                    if (ProductCache.Instance[key].Name.ToLowerInvariant().Trim().Contains(cleanProductName) ||
                       (key.ToLowerInvariant().Trim().Contains(cleanProductCode))) {
                        products.Add(ProductCache.Instance[key]);
                    }
                }
                if (products == null || products.Count == 0) {
                    products = SearchProductsFromDb(cleanProductCode, cleanProductName);
                    if (appSettings.IsMemoryCachingEnabled && products != null) {
                        foreach (var p in products) {
                            ProductCache.Instance.Add(p.Code, p);
                        }
                    }
                }
                return GenerateProductResult(products);
            } else {
                products = SearchProductsFromDb(cleanProductCode, cleanProductName);
                return GenerateProductResult(products);
            }
        }
        List<Product> SearchProductsFromDb(string productCode, string productName) {
            return productCatalogcontext.Product.Where(p => (p.Name.ToLowerInvariant().Trim().Contains(productName)) ||
                                                                       (p.Code.ToLowerInvariant().Trim().Contains(productCode))).ToList();
        }
        [HttpPost("/product/SaveProduct")]
        public IActionResult SaveProduct([FromBody]Product productDto, [FromHeader] HeaderDto headerDto) {

            productDto.LastUpdatedUser = ExecutingUserName;
            if (productDto.RowStatus == "New") {
                return AddProduct(productDto);
            } else {
                return EditProduct(productDto);
            }
        }
        [HttpPost("/product/RemoveProduct")]
        public IActionResult RemoveProduct([FromBody]Product productDto) {
            string productCode = productDto.Code;
            try {
                productCatalogcontext.Database.BeginTransaction();
                Product product = null;
                if (appSettings.IsMemoryCachingEnabled) {
                    if (ProductCache.Instance.ContainsKey(productCode)) {
                        product = ProductCache.Instance[productCode];
                        ProductCache.Instance.Remove(productDto.Code);
                    }
                }

                if (product == null) {
                    product = GetProductFromDb(productCode).FirstOrDefault();
                }
                productCatalogcontext.Product.Remove(product);
                productCatalogcontext.SaveChanges();
                productCatalogcontext.Database.CommitTransaction();
                return Ok();
            }
            catch (Exception ex) {
                productCatalogcontext.Database.RollbackTransaction();
                log.ErrorFormat("RemoveProduct-ProductCode:{0},Error:{1}", productCode, ex);
                throw;
            }
        }
        List<Product> GetProductFromDb(string productCode) {
            return productCatalogcontext.Product.Where(p => p.Code == productCode).ToList();
        }
        IActionResult GenerateProductResult(List<Product> products) {
            if (products == null || products.ToList().Count == 0) {
                return NotFound(products);
            } else {
                return Ok(products);
            }
        }
        IActionResult AddProduct(Product product) {
            try {
                productCatalogcontext.Database.BeginTransaction();
                product.LastUpdatedDate = DateTime.Now;
                product.Id = 0;
                var productResult = productCatalogcontext.Product.Add(product).Entity;
                if (appSettings.IsMemoryCachingEnabled) {
                    ProductCache.Instance.Add(product.Code, product);
                }
                productCatalogcontext.SaveChanges();
                productCatalogcontext.Database.CommitTransaction();
                return Ok(productResult);
            }
            catch (Exception ex) {
                productCatalogcontext.Database.RollbackTransaction();
                log.ErrorFormat("AddProduct-ProductCode:{0},ProductName:{1},Error:{2}", product.Code, product.Name, ex);
                throw;
            }
        }
        IActionResult EditProduct(Product product) {
            try {
                productCatalogcontext.Database.BeginTransaction();
                var productResult = productCatalogcontext.Product.Update(product).Entity;
                if (appSettings.IsMemoryCachingEnabled) {
                    ProductCache.Instance.Update(product.Code, product);
                }
                productCatalogcontext.SaveChanges();
                productCatalogcontext.Database.CommitTransaction();
                return Ok(productResult);
            }
            catch (Exception ex) {
                productCatalogcontext.Database.RollbackTransaction();
                log.ErrorFormat("UpdateProduct-ProductCode:{0},ProductName:{1},Error:{2}", product.Code, product.Name, ex);
                throw;
            }
        }
        string GetCleanString(string str) {
            if (string.IsNullOrEmpty(str)) {
                return string.Empty;
            }
            return str.ToLowerInvariant().Trim();
        }
    }
}

