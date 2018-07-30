using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class SampleDataController : Controller
    {
        class Card
        {
            public string Name { get; set; }
        }
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }

        [HttpGet("/sampledata/cards")]
        public IActionResult Cards()
        {
            var cards = new List<Card>
            {
                new Card() {Name = "Card 1"},
                new Card() {Name = "Card 2"},
                new Card() {Name = "Card 3"},
                new Card() {Name = "Card 4"},
                new Card() {Name = "Card 5"}
            };
            return Ok(cards);
        }

        [HttpGet("/sampledata/weather")]
        public IActionResult Weather()
        {
            var rng = new Random();
            var data = Enumerable.Range(1, 50).Select(index => new WeatherForecast
            {
                DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
            return Ok(data);
        }
    }
}