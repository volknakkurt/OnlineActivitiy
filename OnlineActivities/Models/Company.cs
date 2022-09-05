using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace OnlineActivities.Models
{
    public partial class Company
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string WebSiteDomain { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public ClaimsIdentity? Name { get; internal set; }
    }
}
