using Application.Events;
using Domain;
using Domain.Interfaces;
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
    public class DetailsTests
    {
        private Details.Handler _subject;
        private Mock<IDataContext> _dataContext;

        [SetUp]
        public void SetUp()
        {
            _dataContext = new Mock<IDataContext>();
            _subject = new Details.Handler(_dataContext.Object);
        }

        [Test]
        public async Task Handle_ShouldTryFind()
        {
            //Arrange
            var eventList = new List<Event>
            {
                new Event
                {
                    Id = 1,
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<int>()))
                .Returns(null);
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);

            var query = new Details.Query
            {
                Id = 2,
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            eventSet.Verify(e => e.FindAsync(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task Handle_EventNotFound_ShouldReturnSuccess()
        {
            //Arrange
            var eventList = new List<Event>
            {
                new Event
                {
                    Id = 1,
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<int>()))
                .Returns(null);
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);

            var query = new Details.Query
            {
                Id = 2,
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.Null(actual.Value);
        }

        [Test]
        public async Task Handle_EventFound_ShouldReturnSuccess()
        {
            //Arrange
            var eventList = new List<Event>
            {
                new Event
                {
                    Id = 1,
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<int>()))
                .Returns(new ValueTask<Event>(eventList[0]));
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);

            var query = new Details.Query
            {
                Id = 2,
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsInstanceOf<Event>(actual.Value);
        }
    }
}
