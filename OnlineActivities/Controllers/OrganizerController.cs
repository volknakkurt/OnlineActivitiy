using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineActivities.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        
        [HttpPost]
        
        public IActionResult Create(Organizer organizer)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Organizer organizer1 = new Organizer();
            organizer1.Name = organizer.Name;
            organizer1.Surname = organizer.Surname;
            organizer1.Mail = organizer.Mail;
            organizer1.Password = organizer.Password;
            organizer1.PasswordAgain = organizer.PasswordAgain;
            organizer1.RoleId = organizer.RoleId;
            
            if (organizer.RoleId == 2)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, organizer.Name));
                claims.Add(new Claim(ClaimTypes.Role, "Organizer"));

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gaussgaussgaussgaussgaussgaussga"));
                SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "www.gauss.com",
                    audience: "www.gauss.com",
                    claims: claims,
                    signingCredentials: signingCredentials,
                    expires: DateTime.Now.AddMinutes(30)
                );

                string jwt = handler.WriteToken(token);
                return Ok(jwt);
            }
            else
            {
                return NotFound("Kullanıcı bulunamadı");
            }
            


            context.SaveChanges();
            return Ok();
        }
        
        [HttpPut("{id}")]
        public IActionResult Edit(int id ,Organizer organizer)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Organizer organizer1 = context.Organizers.Find(id);
            if (organizer1 == null)
            {
                return NotFound();
            }
            else
            {
                organizer1.Name = organizer.Name;
                organizer1.Surname = organizer.Surname;
                organizer1.Mail = organizer.Mail;
                organizer1.Password = organizer.Password;
                organizer1.PasswordAgain = organizer.PasswordAgain;
                context.SaveChanges();
                return Ok();
            }


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Organizer organizer = context.Organizers.Find(id);
            context.Organizers.Remove(organizer);

            if (organizer == null)
            {
                return NotFound();
            }
            else
            {
                context.Remove(organizer);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
