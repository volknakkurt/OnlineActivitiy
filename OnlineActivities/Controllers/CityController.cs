using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineActivities.Models;
using OnlineActivities.WievModels;
using System.Data;
using WebApplication1.WievModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnlineActivities.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CityController : ControllerBase
    {
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(City city)

        {

            OnlineActivities1Context context = new OnlineActivities1Context();
            City city1 = new City();
            city1.CityName = city.CityName;
            context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = "Participant")]
        [Authorize(Roles = "Organizer")]
        public IActionResult Getlist()
        {
            OnlineActivities1Context context = new OnlineActivities1Context();

            List<CityWievModel> cities = context.Cities.Select(a => new CityWievModel()
            {
                CityName = a.CityName,


            }).ToList();


            return Ok(cities);
        }
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id , City city)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            City city1 = context.Cities.Find(id);
            if (city1 == null)
            {
                return NotFound();
            }
            else
            {                
                city1.CityName = city.CityName;
               
                context.SaveChanges();
                return Ok();
            }


        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            City city = context.Cities.Find(id);
            context.Cities.Remove(city);

            if (city == null)
            {
                return NotFound();
            }
            else
            {
                context.Remove(city);
                context.SaveChanges();
                return Ok();
            }
            
        }
    }
}

