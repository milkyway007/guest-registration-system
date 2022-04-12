namespace Application.Events
{
    public class CompanyDto : ParticipantDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
    }
}
