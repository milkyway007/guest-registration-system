namespace Domain.Interfaces
{
    public interface IAddress
    {
        int Id { get; set; }
        string Line1 { get; set; }
        string Line2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Zip { get; set; }
        string Country { get; set; }
        ICollection<IEvent> Events { get; set; }
    }
}
