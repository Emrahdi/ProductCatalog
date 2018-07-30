using System;

using WebApi.Models;

namespace UnitTests {
    public static class CommonTestData {
        public static Product GetTestData() {
            return new Product() { Code = "ProductCode1",
                Name = "ProductName1",
                LastUpdatedDate = DateTime.Now,
                Price = 10,
                LastUpdatedUser = "EMRAHDI" };
        }
        public static Product GetTestData(string key) {
            return new Product() { Code = string.Concat("ProductCode",key),
                Name = string.Concat("ProductCode", key),
                LastUpdatedDate = DateTime.Now,
                Price = 10,
                LastUpdatedUser = "EMRAHDI" };
        }
    }
}
