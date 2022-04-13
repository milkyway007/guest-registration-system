using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using List = Application.Events.Queries.List;
using Moq;
using Persistence.Interfaces;
using System.Threading;
using MockQueryable.Moq;
using Application.Interfaces.Core;
using Domain.Entities;

namespace Tests.Application.Events
{
    [TestFixture]
    public class ListTests
    {
        private List.Handler _subject;
        private Mock<IDataContext> _dataContext;
        private Mock<IEntityFrameworkQueryableExtensionsAbstraction> _extensionsAbstraction;

        [SetUp]
        public void SetUp()
        {
            _dataContext = new Mock<IDataContext>();
            _extensionsAbstraction = new Mock<IEntityFrameworkQueryableExtensionsAbstraction>();
            _subject = new List.Handler(_dataContext.Object, _extensionsAbstraction.Object);
        }

        [Test]
        public async Task Handle_ShouldTryList()
        {
            //Arrange
            var eventList = CreateEventList();
            SetUpMocks(eventList, null);
            var query = new List.Query();

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            _extensionsAbstraction.Verify(x => x.ToListAsync(
                It.IsAny<IQueryable<Event>>(), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task Handle_EventsNotFound_ShouldReturnSuccess()
        {
            //Arrange
            var eventList = CreateEventList();
            SetUpMocks(eventList, null);
            var query = new List.Query();

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.Null(actual.Value);
        }

        [Test]
        public async Task Handle_EventsFound_ShouldReturnSuccess()
        {
            //Arrange
            var eventList = CreateEventList();
            SetUpMocks(eventList, eventList.ToList());
            var query = new List.Query();

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsInstanceOf<List<Event>>(actual.Value);
        }

        private static IList<Event> CreateEventList()
        {
            return new List<Event>
            {
                new Event
                {
                    Id = 1,
                },
            };
        }

        private void SetUpMocks(IList<Event> eventList, List<Event> listed)
        {
            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);
            _extensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<Event>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(listed));
        }
    }
}
