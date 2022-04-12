using Application.Events.Commands;
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
    public class CreateTests
    {
        private Create.Handler _subject;
        private Mock<IDataContext> _dataContext;

        [SetUp]
        public void SetUp()
        {
            _dataContext = new Mock<IDataContext>();
            _subject = new Create.Handler(_dataContext.Object);
        }

        [Test]
        public async Task Handle_ShouldAdd()
        {
            //Arrange
            var eventList = CreateEventList();
            var eventSet = SetUpMocks(eventList, 1);
            var command = CreateCommand();

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            eventSet.Verify(x => x.Add(It.IsAny<Event>()), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldSaveChanges()
        {
            //Arrange
            var eventList = CreateEventList();
            var eventSet = SetUpMocks(eventList, 1);
            var command = CreateCommand();

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
            var command = CreateCommand();

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
            var command = CreateCommand();

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(actual.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(actual.Error));
        }


        private Create.Command CreateCommand()
        {
            return new Create.Command
            {
                Event = new Event
                {
                    Id = 2,
                }
            };
        }

        private Mock<DbSet<Event>> SetUpMocks(List<Event> eventList, int saveResult)
        {
            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(saveResult));

            return eventSet;
        }

        private List<Event> CreateEventList()
        {
            return new List<Event>
            {
                new Event
                {
                    Id = 1,
                },
            };
        }
    }
}
