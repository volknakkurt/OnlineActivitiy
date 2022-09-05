using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineActivities.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.WievModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class CompanyController : ControllerBase
    {
        
        [HttpPost]
        public IActionResult Create(CompanyWievModel company)
        {
            Company company1 = new Company();
            company1.CompanyName = company.CompanyName;
            company1.WebSiteDomain = company.WebSiteDomain;
            company1.Mail = company.Mail;
            company1.Password = company.Password;
           
            
            OnlineActivities1Context context = new OnlineActivities1Context();
            context.Add(company);
            if (company.CompanyName == company.CompanyName)
            {
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, company.CompanyName));
                claims.Add(new Claim(ClaimTypes.Role, "Company"));

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
        [HttpGet]
        [Authorize(Roles = "Participant")]
        public IActionResult Getlist()
        {
            OnlineActivities1Context context = new OnlineActivities1Context();

            List<CompanyWievModel> companies = context.Companies.Select(a => new CompanyWievModel()
            {
               CompanyName = a.CompanyName,
                WebSiteDomain = a.WebSiteDomain
            }).ToList();


            return Ok(companies);
        }
        [HttpPut("{id}")]
        public IActionResult Edit(int id , Company company)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Company company1 = context.Companies.Find(id);
            if (company1 == null ) 
            {
                return NotFound();
            }
           
            else
            {
                company1.CompanyName = company.CompanyName;
                company1.WebSiteDomain = company.WebSiteDomain;
                company1.Mail = company.Mail;
                company1.Password = company.Password;
                context.SaveChanges();
                return Ok();
            }


        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            OnlineActivities1Context context = new OnlineActivities1Context();
            Company company = context.Companies.Find(id);
            context.Companies.Remove(company);

            if (company == null)
            {
                return NotFound();
            }
            else
            {
                context.Remove(company);
                context.SaveChanges();
                return Ok();
            }
        }
    }
}
