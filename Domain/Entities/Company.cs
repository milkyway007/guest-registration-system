namespace Domain.Entities
{
    public class Company : Participant
    {
        public string Name { get; set; }
        public int ParticipantCount { get; set; }
        public string Description { get; set; }
    }
}
