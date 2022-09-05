using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineActivities.Models;
using OnlineActivities.WievModels;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using WebApplication1.WievModels;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ParticipantController : ControllerBase
    {
       
        [HttpPost]
        public IActionResult Create(Participant participant)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Participant participant1 = new Participant();
            participant1.Name = participant.Name;
            participant1.Surname = participant.Surname;
            participant1.Mail = participant.Mail;
            participant1.Password = participant.Password;
            participant1.PasswordAgain = participant.PasswordAgain;
            participant1.RoleId = participant.RoleId;

            if (participant.RoleId == 1)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, participant.Name));
                claims.Add(new Claim(ClaimTypes.Role, "Participant"));

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
        [HttpPatch("{id}")] 
        public IActionResult EditPartial(int id, ParticipantWievModel participant)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Participant participant1 = context.Participants.Find(id);
            participant1.Name = participant.Name;
            participant1.Surname = participant.Surname;
            participant1.Mail = participant.Mail;
            participant1.Password = participant.Password;
            participant1.PasswordAgain = participant.PasswordAgain;
            context.SaveChanges();

            return Ok();
        }


        [HttpPut("{id}")]
        public IActionResult Edit(int id ,ParticipantWievModel participant)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Participant participant1 = context.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }
            else
            {
                participant1.Name = participant1.Name;
                participant1.Surname = participant.Surname;
                participant1.Mail = participant.Mail;
                participant1.Password = participant.Password;
                participant1.PasswordAgain = participant.PasswordAgain;



                context.SaveChanges();
                return Ok();
            }


        }

        [HttpDelete("{ParticipantId}")]
        public IActionResult Delete(int ParticipantId)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Participant participant = context.Participants.Find(ParticipantId);
            context.Participants.Remove(participant);
            

            if (participant == null)
            {
                return NotFound();
            }
            else
            {
                context.Remove(participant);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
