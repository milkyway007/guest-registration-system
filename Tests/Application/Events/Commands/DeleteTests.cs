using Application.Events.Commands;
using Domain.Entities;
using FluentValidation;
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
    public class DeleteTests
    {
        private Delete.Handler _subject;
        private Mock<IDataContext> _dataContext;

        [SetUp]
        public void SetUp()
        {
            _dataContext = new Mock<IDataContext>();
            _subject = new Delete.Handler(_dataContext.Object, new Mock<IValidator<Delete.Command>>().Object);
        }

        [Test]
        public async Task Handle_ShouldTryFind()
        {
            //Arrange
            var eventList = CreateEventList();
            var eventSet = SetUpMocks(eventList, null, 1);
            var command = CreateCommand();

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            eventSet.Verify(e => e.FindAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task Handle_EventNotFound_ShouldReturnNull()
        {
            //Arrange
            var eventList = CreateEventList();
            SetUpMocks(eventList, null, 1);
            var command = CreateCommand();

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.Null(actual);
        }

        [Test]
        public async Task Handle_EventFound_ShouldRemove()
        {
            //Arrange
            var eventList = CreateEventList();
            var eventSet = SetUpMocks(eventList, eventList[1], 1);
            var command = CreateCommand();

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            _dataContext.Verify(x => x.Remove(It.IsAny<Event>()), Times.Once);
        }

        [Test]
        public async Task Handle_EventFound_ShouldSaveChanges()
        {
            //Arrange
            var eventList = CreateEventList();
            var eventSet = SetUpMocks(eventList, eventList[1], 1);
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
            var eventSet = SetUpMocks(eventList, eventList[1], 1);
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
            var eventSet = SetUpMocks(eventList, eventList[1], - 1);
            var command = CreateCommand();

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(actual.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(actual.Error));
        }

        private IList<Event> CreateEventList()
        {
            return new List<Event>
            {
                new Event
                {
                    Id = 1,
                },
                new Event
                {
                    Id = 2,
                },
            };
        }

        private Mock<DbSet<Event>> SetUpMocks(IList<Event> eventList, Event found, int saveResult)
        {
            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<int>()))
                .Returns(new ValueTask<Event>(found));
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(saveResult));

            return eventSet;
        }

        private Delete.Command CreateCommand()
        {
            return new Delete.Command
            {
                Id = 2,
            };
        }

    }
}
