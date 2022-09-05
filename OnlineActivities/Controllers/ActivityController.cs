using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineActivities.Models;
using OnlineActivities.WievModels;
using System.Data;
using System.Diagnostics;
using WebApplication1.WievModels;
using Activity = OnlineActivities.Models.Activity;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ActivityController : ControllerBase
    {
        
        
        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public IActionResult Create(Activity activity)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Activity activity1 = new Activity();
            activity1.ActivityName = activity.ActivityName;
            activity1.Date = activity.Date;
            activity1.Deadline = activity.Deadline;
            activity1.Detail = activity.Detail;
            activity1.Adress = activity.Adress;
            activity1.Availability = activity.Availability;
            activity1.Cost = activity.Cost;
            activity1.CityId = activity.CityId;
            activity1.CategoryId = activity.CategoryId;


            //?????

            context.SaveChanges();
            return Ok();
        }
        [HttpGet]
        [Authorize(Roles = "Participant")]
        [Authorize(Roles = "Company")]

        public IActionResult Getlist()
        {
            OnlineActivities1Context context = new OnlineActivities1Context();

            List<ActivityWievModel> activities = context.Activities.Select(a => new ActivityWievModel()
            {
                ActivityName = a.ActivityName,
                Date = a.Date,
                Deadline = a.Deadline,
                Adress = a.Adress,
                Cost = a.Cost,
                Detail = a.Detail,
                CategoryId = a.CategoryId
                
            }).ToList();


            return Ok(activities);
        }
        [HttpGet("id")]

        public IActionResult Filter(int id, ActivityWievModel activity)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Activity activityWiev = context.Activities.Find(id);
            List<CategoryWievModel> categories = context.Categories.Select(a => new CategoryWievModel()
            {
                CategoryName = a.CategoryName,
            }).ToList();
            List<CityWievModel> cities = context.Cities.Select(a => new CityWievModel()
            {
                CityName = a.CityName,
            }).ToList();
            return Ok(categories);
            return Ok(cities);

        }

        [HttpPut("id")]
        public IActionResult Edit(int id , ActivityWievModel activity)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();

            Activity activityWiev = context.Activities.Find(id);
            if (DateTime.Now.AddDays(5) < activity.Deadline)
            {
                if (activityWiev == null)
                {
                    return NotFound();
                }
                else
                {
                    activityWiev.ActivityName = activity.ActivityName;
                    activityWiev.Date = activity.Date;
                    activityWiev.Deadline = activity.Deadline;
                    activityWiev.Adress = activity.Adress;

                    activityWiev.Cost = activity.Cost;



                    context.SaveChanges();
                    return Ok();
                }
            }
            else
            {
                return BadRequest();
            }

        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Activity activity = context.Activities.Find(id);
            context.Activities.Remove(activity);
            if (DateTime.Now.AddDays(5) < activity.Deadline)
            {
                

                if (activity == null)
                {
                    return NotFound();
                }
                else
                {
                    context.Remove(activity);
                    context.SaveChanges();
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
