using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AntAdmin.Web.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DefinitionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Definitions/GetApplications")]
        public IEnumerable<Application> GetApplications()
        {
            List<Application> applications = new List<Application>();
            applications.Add(new Application() { Code = "ESB", Description = "Enterprise Service Bus", IsActive = true });
            applications.Add(new Application() { Code = "NOTIFICATON", Description = "Notification Uygulama Tanımı", IsActive = true });
            applications.Add(new Application() { Code = "MVT", Description = "Pusula Uygulama Tanımı", IsActive = true });
            applications.Add(new Application() { Code = "INTERFACE", Description = "Temel Bankacılık Uygulama Tanımı", IsActive = true });
            applications.Add(new Application() { Code = "FRAUD", Description = "Fraud Uygulama Tanımı", IsActive = true });
            return applications;
        }
    }

    public class Application
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}