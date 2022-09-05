using System;
using System.Collections.Generic;

namespace OnlineActivities.Models
{
    public partial class Activity
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; } = null!;
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public string? Detail { get; set; }
        public string Adress { get; set; } = null!;
        public int Availability { get; set; }
        public double? Cost { get; set; }
        public int CityId { get; set; }
        public int CategoryId { get; set; }
        public int OrganizerId { get; set; }
    }
}
