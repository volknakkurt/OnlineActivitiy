namespace WebApplication1.WievModels
{
    public class ActivityWievModel
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public DateTime Date { get; set; }
        public DateTime Deadline { get; set; }
        public string? Detail { get; set; }
        public string Adress { get; set; }
        public int Availability { get; set; }
        public double? Cost { get; set; }
        public int CityId { get; set; }
        public int CategoryId { get; set; }
        public int OrganizerId { get; set; }


    }
}
