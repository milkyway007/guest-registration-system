using Application.Events;
using Domain;
using Domain.Interfaces;
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
    public class CancelParticipationTests
    {
        private CancelParticipation.Handler _subject;
        private Mock<IDataContext> _dataContext;

        [SetUp]
        public void SetUp()
        {
            _dataContext = new Mock<IDataContext>();
            _subject = new CancelParticipation.Handler(_dataContext.Object);
        }

        [Test]
        public async Task Handle_EventNotFound_ShouldReturnNull()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
            };
            var company = new Company
            {
                Id = 2,
            };
            var participantList = new List<IEventParticipant>
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
            var eventList = new List<IEvent>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);

            var command = new CancelParticipation.Command
            {
                EventId = 2,
                ParticipantId = 1,
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.Null(actual);
        }

        [Test]
        public async Task Handle_ParticipantNotFound_ShouldReturnNull()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
            };
            var company = new Company
            {
                Id = 2,
            };
            var participantList = new List<IEventParticipant>
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
            var eventList = new List<IEvent>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);

            var command = new CancelParticipation.Command
            {
                EventId = 1,
                ParticipantId = 3,
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.Null(actual);
        }

        [Test]
        public async Task Handle_EventParticipantFound_ShouldRemove()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
            };
            var company = new Company
            {
                Id = 2,
            };
            var participantList = new List<IEventParticipant>
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
            var eventList = new List<IEvent>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));

            var command = new CancelParticipation.Command
            {
                EventId = 1,
                ParticipantId = 1,
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(eventList
                .FirstOrDefault(x => x.Id == 1)
                .Participants.Any(x => x.ParticipantId == 1));
        }

        [Test]
        public async Task Handle_EventParticipantFound_ShouldSaveChanges()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
            };
            var company = new Company
            {
                Id = 2,
            };
            var participantList = new List<IEventParticipant>
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
            var eventList = new List<IEvent>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));

            var command = new CancelParticipation.Command
            {
                EventId = 1,
                ParticipantId = 1,
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            _dataContext.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ChangesSaved_ShouldReturnSuccess()
        {
            //Arrange
            var person = new Person
            {
                Id = 1,
            };
            var company = new Company
            {
                Id = 2,
            };
            var participantList = new List<IEventParticipant>
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
            var eventList = new List<IEvent>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(1));

            var command = new CancelParticipation.Command
            {
                EventId = 1,
                ParticipantId = 1,
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
            };
            var company = new Company
            {
                Id = 2,
            };
            var participantList = new List<IEventParticipant>
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
            var eventList = new List<IEvent>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync()).Returns(Task.FromResult(-1));

            var command = new CancelParticipation.Command
            {
                EventId = 1,
                ParticipantId = 1,
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(actual.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(actual.Error));
        }
    }
}
