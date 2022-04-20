namespace Application.Events.Dtos
{
    public class CompanyDto : ParticipantDto
    {
        public string Name { get; set; }
        public int ParticipantCount { get; set; }
        public string Description { get; set; }
    }
}
