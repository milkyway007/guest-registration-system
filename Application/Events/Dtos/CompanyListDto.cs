namespace Application.Events.Dtos
{
    public class CompanyListDto
    {
        public string EventName { get; set; }
        public List<CompanyDto> Participants { get; set; }
    }
}
