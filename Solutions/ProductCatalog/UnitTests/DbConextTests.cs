using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utilities.ServiceLocator;
using WebApi.Models;

namespace UnitTests
{
    [TestClass]
    public class DbConextTests
    {
        [TestMethod]
        public void ProductDbOperationsOperations()
        {
            DefaultServices.RegisterDefaultServices();
            ProductCatalogContext ctx = new ProductCatalogContext();
            var result = ctx.Product.Where(p => p.Code == "Product1").ToList();
            Product product1 = CommonTestData.GetTestData();
            var addProductResult1 = ctx.Product.Add(product1);
            ctx.SaveChanges();
            var getProductResult1 = ctx.Product.Where(p => p.Code == product1.Code).ToList().FirstOrDefault();
            Assert.AreEqual(getProductResult1.Name, product1.Name);
            getProductResult1.Name = "ProductNameNew";
            ctx.Product.Update(getProductResult1);
            ctx.SaveChanges();
            var getProductResult2 = ctx.Product.Where(p => p.Code == product1.Code).ToList().FirstOrDefault();
            Assert.AreEqual(getProductResult2.Name, "ProductNameNew");
            ctx.Product.Remove(getProductResult2);
            ctx.SaveChanges();
            var finalProductResult = ctx.Product.Find(getProductResult2.Id);
            Assert.AreEqual(finalProductResult, null);
        }

    }
}
