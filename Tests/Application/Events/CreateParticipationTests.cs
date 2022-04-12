using Application.Events;
using AutoMapper;
using Domain;
using MediatR;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Application.Events
{
    [TestFixture]
    public class CreateParticipationTests
    {
        private CreateParticipation.Handler _subject;
        private Mock<IDataContext> _dataContext;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void SetUp()
        {
            _dataContext = new Mock<IDataContext>();
            _mapper = new Mock<IMapper>();
            _subject = new CreateParticipation.Handler(_dataContext.Object, _mapper.Object);
        }

        [Test]
        public async Task Handle_EventNotFound_ShouldReturnNull()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
                Code = "B",
            };
            var company = new Company
            {
                Id = 2,
                Code = "C",
            };
            var participantList = new List<EventParticipant>
            {
                new EventParticipant
                {
                    Participant = person,
                },
                new EventParticipant
                {
                    Participant = company,
                },
            };
            var eventList = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);

            var command = new CreateParticipation.Command
            {
                EventId = 2,
                Participant = new Person
                {
                    Id = 3,
                    Code = "A",
                },
            };

            //Act
            var actual = await _subject.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.Null(actual);
        }

        [Test]
        public async Task Handle_ParticipantFound_ShouldReturnFailure()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
                Code = "B",
            };
            var company = new Company
            {
                Id = 2,
                Code = "C",
            };
            var participantList = new List<EventParticipant>
            {
                new EventParticipant
                {
                    Participant = person,
                },
                new EventParticipant
                {
                    Participant = company,
                },
            };
            var eventList = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };
            var participants = new List<Participant>
            {
                person,
                company
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            var participantSet = participants.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.SetupGet(e => e.Participants).Returns(participantSet.Object);

            var command = new CreateParticipation.Command
            {
                EventId = 1,
                Participant = new Person
                {
                    Id = 1,
                    Code = "A",
                }
            };

            //Act
            var actual = await _subject.Handle(command, new System.Threading.CancellationToken());

            //Assert
            Assert.False(actual.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(actual.Error));
        }

        [Test]
        public async Task Handle_ParticipantNotFound_ShouldAddParticipant()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
                Code = "B",
            };
            var company = new Company
            {
                Id = 2,
                Code = "C",
            };
            var eventParticipants = new List<EventParticipant>
            {
                new EventParticipant
                {
                    Participant = person,
                },
                new EventParticipant
                {
                    Participant = company,
                },
            };
            var eventList = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Participants = eventParticipants,
                },
            };
            var participants = new List<Participant>
            {
                person,
                company
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            var participantSet = participants.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.SetupGet(e => e.Participants).Returns(participantSet.Object);

            var command = new CreateParticipation.Command
            {
                EventId = 1,
                Participant = new Company
                {
                    Id = 3,
                    Code = "A",
                }
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            participantSet.Verify(x => x.Add(It.IsAny<Participant>()), Times.Once);
        }

        [Test]
        public async Task Handle_EventParticipantNotFound_ShouldAddEventParticipant()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
                Code = "B",
            };
            var company = new Company
            {
                Id = 2,
                Code = "C",
            };
            var participantList = new List<EventParticipant>
            {
                new EventParticipant
                {
                    Participant = person,
                },
                new EventParticipant
                {
                    Participant = company,
                },
            };
            var eventList = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };
            var participants = new List<Participant>
            {
                person,
                company
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            var participantSet = participants.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.SetupGet(e => e.Participants).Returns(participantSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            var command = new CreateParticipation.Command
            {
                EventId = 1,
                Participant = new Company
                {
                    Id = 3,
                    Code = "A",
                },
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.True(eventList
                .FirstOrDefault(x => x.Id == 1)
                .Participants.Any(x => x.Participant.Id == 3));
        }

        [Test]
        public async Task Handle_EventParticipantNotFound_ShouldSaveChanges()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
                Code = "B",
            };
            var company = new Company
            {
                Id = 2,
                Code = "C",
            };
            var participantList = new List<EventParticipant>
            {
                new EventParticipant
                {
                    Participant = person,
                },
                new EventParticipant
                {
                    Participant = company,
                },
            };
            var eventList = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };
            var participants = new List<Participant>
            {
                person,
                company
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            var participantSet = participants.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.SetupGet(e => e.Participants).Returns(participantSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            var command = new CreateParticipation.Command
            {
                EventId = 1,
                Participant = new Company
                {
                    Id = 3,
                    Code = "A",
                },
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            _dataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Handle_ChangesSaved_ShouldReturnSuccess()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
                Code = "B",
            };
            var company = new Company
            {
                Id = 2,
                Code = "C",
            };
            var participantList = new List<EventParticipant>
            {
                new EventParticipant
                {
                    Participant = person,
                },
                new EventParticipant
                {
                    Participant = company,
                },
            };
            var eventList = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };
            var participants = new List<Participant>
            {
                person,
                company
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            var participantSet = participants.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.SetupGet(e => e.Participants).Returns(participantSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            var command = new CreateParticipation.Command
            {
                EventId = 1,
                Participant = new Company
                {
                    Id = 3,
                    Code = "A",
                },
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsInstanceOf<Unit>(actual.Value);
        }

        [Test]
        public async Task Handle_ChangesNotSaved_ShouldReturnFailure()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
                Code = "B",
            };
            var company = new Company
            {
                Id = 2,
                Code = "C",
            };
            var participantList = new List<EventParticipant>
            {
                new EventParticipant
                {
                    Participant = person,
                },
                new EventParticipant
                {
                    Participant = company,
                },
            };
            var eventList = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };
            var participants = new List<Participant>
            {
                person,
                company
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            var participantSet = participants.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.SetupGet(e => e.Participants).Returns(participantSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(-1));

            var command = new CreateParticipation.Command
            {
                EventId = 1,
                Participant = new Company
                {
                    Id = 3,
                    Code = "A",
                },
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(actual.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(actual.Error));
        }
    }
}
