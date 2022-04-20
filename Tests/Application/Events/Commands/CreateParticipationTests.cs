using Application.Events.Commands;
using Application.Interfaces.Core;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private Mock<IEntityFrameworkQueryableExtensionsAbstraction> _eFExtensionsAbstraction;

        [SetUp]
        public void SetUp()
        {
            _eFExtensionsAbstraction = new Mock<IEntityFrameworkQueryableExtensionsAbstraction>();
            _dataContext = new Mock<IDataContext>();
            _subject = new CreateParticipation.Handler(
                _dataContext.Object,
                _eFExtensionsAbstraction.Object);
        }

        [Test]
        public async Task Handle_EventNotFound_ShouldReturnNull()
        {
            //Arrange
            (IList<Event> events, IList<Participant> participants) = CreateTestData();
            SetUpMocks(events, participants, null, -1);
            var command = CreateCommand(2, "A");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.Null(actual);
        }

        [Test]
        public async Task Handle_ParticipantFound_ShouldReturnFailure()
        {
            //Arrange
            (IList<Event> events, IList<Participant> participants) = CreateTestData();
            SetUpMocks(events, participants, participants[0], 1);
            var command = CreateCommand(1, "B");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(actual.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(actual.Error));
        }

        [Test]
        public async Task Handle_ParticipantNotFound_ShouldAddParticipant()
        {
            //Arrange
            (IList<Event> events, IList<Participant> participants) = CreateTestData();
            SetUpMocks(events, participants, null, -1);
            var command = CreateCommand(1, "A");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            _eFExtensionsAbstraction.Verify(x => x.AddAsync(
                It.IsAny<Participant>(),
                It.IsAny<DbSet<Participant>>()), Times.Once);
        }

        [Test]
        public async Task Handle_EventParticipantNotFound_ShouldAddEventParticipant()
        {
            //Arrange
            (IList<Event> events, IList<Participant> participants) = CreateTestData();
            SetUpMocks(events, participants, participants[0], 1);
            var command = CreateCommand(1, "A");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.True(events
                .FirstOrDefault(x => x.Id == 1)
                .Participants.Any(x => x.ParticipantCode == "A"));
        }

        [Test]
        public async Task Handle_EventParticipantNotFound_ShouldSaveChanges()
        {
            //Arrange
            (IList<Event> events, IList<Participant> participants) = CreateTestData();
            SetUpMocks(events, participants, participants[0], 1);
            var command = CreateCommand(1, "A");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            _dataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Handle_ChangesSaved_ShouldReturnSuccess()
        {
            //Arrange
            (IList<Event> events, IList<Participant> participants) = CreateTestData();
            SetUpMocks(events, participants, participants[0], 1);
            var command = CreateCommand(1, "A");

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
            (IList<Event> events, IList<Participant> participants) = CreateTestData();
            SetUpMocks(events, participants, participants[0], -1);
            var command = CreateCommand(1, "B");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(actual.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(actual.Error));
        }

        private (IList<Event> eventList, IList<Participant> participantList) CreateTestData()
        {
            var person = new Person
            {
                Code = "B",
            };
            var company = new Company
            {
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
            var events = new List<Event>
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

            return (events, participants);
        }

        private void SetUpMocks(
            IList<Event> eventList,
            IList<Participant> participantsList,
            Participant added,
            int saveResult)
        {
            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            var participantSet = participantsList.AsQueryable().BuildMockDbSet();

            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _eFExtensionsAbstraction.Setup(x => x.AddAsync(
                It.IsAny<Participant>(),
                It.IsAny<DbSet<Participant>>()))
                .Returns(Task.FromResult(added));

            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.SetupGet(e => e.Participants).Returns(participantSet.Object);

            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(saveResult));
        }

        private CreateParticipation.Command CreateCommand(int eventId, string participantCode)
        {
            return new CreateParticipation.Command
            {
                EventId = eventId,
                Participant = new Person
                {
                    Code = participantCode,
                },
            };
        }
    }
}
