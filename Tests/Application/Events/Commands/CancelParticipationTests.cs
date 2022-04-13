using Application.Events.Commands;
using Domain.Entities;
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
            var eventList = CreateEventList();
            SetUpMocks(eventList, 1);
            var command = new CancelParticipation.Command
            {
                EventId = 2,
                ParticipantCode = "1",
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
            var eventList = CreateEventList();
            SetUpMocks(eventList, 1);
            var command = new CancelParticipation.Command
            {
                EventId = 1,
                ParticipantCode = "3",
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
            var eventList = CreateEventList();
            SetUpMocks(eventList, 1);
            var command = new CancelParticipation.Command
            {
                EventId = 1,
                ParticipantCode = "1",
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(eventList
                .FirstOrDefault(x => x.Id == 1)
                .Participants.Any(x => x.ParticipantCode == "1"));
        }

        [Test]
        public async Task Handle_EventParticipantFound_ShouldSaveChanges()
        {
            //Arrange
            var eventList = CreateEventList();
            SetUpMocks(eventList, 1);
            var command = new CancelParticipation.Command
            {
                EventId = 1,
                ParticipantCode = "1",
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
            var eventList = CreateEventList();
            SetUpMocks(eventList, 1);
            var command = new CancelParticipation.Command
            {
                EventId = 1,
                ParticipantCode = "1",
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
            var eventList = CreateEventList();
            SetUpMocks(eventList, -1);
            var command = new CancelParticipation.Command
            {
                EventId = 1,
                ParticipantCode = "1",
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(actual.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(actual.Error));
        }

        private IList<Event> CreateEventList()
        {
            var person = new Person
            {
                Code = "1",
            };
            var company = new Company
            {
                Code = "2",
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

            return new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Participants = participantList,
                },
            };
        }

        private void SetUpMocks(IList<Event> eventList, int saveResult)
        {
            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(saveResult));
        }
    }
}

