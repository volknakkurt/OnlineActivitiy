using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineActivities.Models;
using System.Data;
using WebApplication1.WievModels;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        
        [HttpPost]
        public IActionResult Create(Category category)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Category category1 = new Category();
            category1.CategoryName = category.CategoryName;
            context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = "Participant")]
        [Authorize(Roles = "Organizer")]
        public IActionResult Getlist()
        {
            OnlineActivities1Context context = new OnlineActivities1Context();

            List<CategoryWievModel> categories = context.Categories.Select(a => new CategoryWievModel()
            {
               CategoryName = a.CategoryName,
              
               
            }).ToList();


            return Ok(categories);
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id , Category category)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Category category1 = context.Categories.Find(id);
            if (category1 == null)
            {
                return NotFound();
            }
            else
            {
                category1.CategoryName = category.CategoryName;                
                context.SaveChanges();
                return Ok();
            }


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Category category = context.Categories.Find(id);
            context.Categories.Remove(category);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                context.Remove(category);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
