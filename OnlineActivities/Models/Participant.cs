using System;
using System.Collections.Generic;

namespace OnlineActivities.Models
{
    public partial class Participant
    {
        public int ParticipantId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Mail { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PasswordAgain { get; set; } = null!;
        public string JoinToAc { get; set; } = null!;
        public int? ActivityId { get; set; }
    }
}
