using System;
using System.Collections.Generic;

namespace OnlineActivities.Models
{
    public partial class CompanyToActivity
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int ActivityId { get; set; }
    }
}
