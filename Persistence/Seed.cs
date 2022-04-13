using Constants;
using Domain.Entities;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedDataAsync(DataContext context)
        {
            if (!context.Addresses.Any() &&
                !context.Participants.Any() &&
                !context.Events.Any() &&
                !context.EventParticipants.Any())
            {
                var addresses = new List<Address>
                {
                    new Address
                    {
                        Line1 = "Banyan Tree str 1",
                        Line2 = "Banyan Tree Hall",
                        City =  "Tallinn",
                        State = "Harjumaa",
                        Zip = "123ABC",
                        Country = "Estonia",
                    },
                    new Address
                    {
                        Line1 = "Neem Tree str 2",
                        Line2 = "Neem Tree Hall",
                        City =  "Tartu",
                        State = "Tartumaa",
                        Zip = "124ABC",
                        Country = "Estonia"
                    },
                    new Address
                    {
                        Line1 = "Aloe Vera Tree str 3",
                        Line2 = "Aloe Vera Tree Hall",
                        City =  "Parnu",
                        State = "Parnumaa",
                        Zip = "125ABC",
                        Country = "Estonia"
                    },
                };

                var companies = new List<Company>
                {
                    new Company
                    {
                        Name = "Rose company",
                        Code = "123456789",
                        Description = "Very good company",
                        ParticipantCount = 50,
                        PaymentMethod = Enums.PaymentMethod.BankTransfer,
                    },
                    new Company
                    {
                        Name = "Lily company",
                        Code = "123456781",
                        Description = "Very good company",
                        ParticipantCount = 150,
                        PaymentMethod = Enums.PaymentMethod.BankTransfer,
                    },
                    new Company
                    {
                        Name = "Tulip company",
                        Code = "123456782",
                        Description = "Very good company",
                        ParticipantCount = 25,
                        PaymentMethod = Enums.PaymentMethod.BankTransfer,
                    }
                };

                var persons = new List<Person>
                {
                    new Person
                    {
                        FirstName = "Maria",
                        LastName = "Mets",
                        Code = "41234567890",
                        Description = "Very good person",
                        PaymentMethod = Enums.PaymentMethod.Cash,
                    },
                     new Person
                    {
                        FirstName = "Erkki",
                        LastName = "Jogi",
                        Code = "31234567890",
                        Description = "Very good person",
                        PaymentMethod = Enums.PaymentMethod.Cash,
                    },
                    new Person
                    {
                        FirstName = "Aleksander",
                        LastName = "Pold",
                        Code = "31234567891",
                        Description = "Very good person",
                        PaymentMethod = Enums.PaymentMethod.BankTransfer,
                    }
                };

                var events = new List<Event>
                {
                    new Event
                    {
                        Name = "Mary J. Blige songs",
                        Occurrence = new DateTime(2021, 12, 15),
                        Description = "Very good event",
                        Participants = new List<EventParticipant>
                        {
                            new EventParticipant
                            {
                                Participant = companies[0],
                            },
                            new EventParticipant
                            {
                                Participant = persons[0],
                            },
                        },
                        Address = addresses[0],
                    },
                    new Event
                    {
                        Name = "Steven Tyler songs",
                        Occurrence = new DateTime(2021, 11, 15),
                        Description = "Very good event",
                        Participants = new List<EventParticipant>
                        {
                            new EventParticipant
                            {
                                Participant = companies[1],
                            },
                            new EventParticipant
                            {
                                Participant = persons[1],
                            },
                        },
                        Address = addresses[1],
                    },
                   new Event
                    {
                        Name = "Stevie Nicks songs",
                        Occurrence = new DateTime(2022, 5, 15),
                        Description = "Very good event",
                        Participants = new List<EventParticipant>
                        {
                            new EventParticipant
                            {
                                Participant = companies[2],
                            },
                            new EventParticipant
                            {
                                Participant = persons[2],
                            },
                        },
                        Address = addresses[2],
                    },
                   new Event
                    {
                        Name = "Joe Cocker songs",
                        Occurrence = new DateTime(2021, 6, 15),
                        Description = "Very good event",
                        Participants = new List<EventParticipant>
                        {
                            new EventParticipant
                            {
                                Participant = companies[0],
                            },
                            new EventParticipant
                            {
                                Participant = persons[1],
                            },
                        },
                        Address = addresses[2],
                    },
                   new Event
                    {
                        Name = "B.B. King songs",
                        Occurrence = new DateTime(2022, 10, 15),
                        Description = "Very good event",
                        Participants = new List<EventParticipant>
                        {
                            new EventParticipant
                            {
                                Participant = companies[2],
                            },
                            new EventParticipant
                            {
                                Participant = persons[1],
                            },
                        },
                        Address = addresses[0],
                    },
                   new Event
                    {
                        Name = "Patti LaBelle songs",
                        Occurrence = new DateTime(2022, 12, 15),
                        Description = "Very good event",
                        Participants = new List<EventParticipant>
                        {
                            new EventParticipant
                            {
                                Participant = companies[1],
                            },
                            new EventParticipant
                            {
                                Participant = persons[2],
                            },
                        },
                        Address = addresses[1],
                    },
                };

                await context.Addresses.AddRangeAsync(addresses);
                await context.Participants.AddRangeAsync(companies);
                await context.Participants.AddRangeAsync(persons);
                await context.Events.AddRangeAsync(events);
                await context.SaveChangesAsync();
            }
        }
    }
}
