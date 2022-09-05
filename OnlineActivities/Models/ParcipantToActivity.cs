using System;
using System.Collections.Generic;

namespace OnlineActivities.Models
{
    public partial class ParcipantToActivity
    {
        public int Id { get; set; }
        public int ParticipantId { get; set; }
        public int ActivityId { get; set; }
    }
}
