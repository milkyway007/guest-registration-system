namespace Application.Events.Dtos
{
    public class PersonDto : ParticipantDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
    }
}
