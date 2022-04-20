namespace Application.Events.Dtos
{
    public class PersonListDto
    {
        public string EventName { get; set; }
        public List<PersonDto> Participants { get; set; }
    }
}
