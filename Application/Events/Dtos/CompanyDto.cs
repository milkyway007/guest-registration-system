namespace Application.Events.Dtos
{
    public class CompanyDto : ParticipantDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
    }
}
