using Application.Participants.Queries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Application.Participants
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
            var participantList = CreateParticipantList();
            var eventSet = SetUpMocks(participantList, null);
            var query = new Details.Query
            {
                Code = "1",
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            eventSet.Verify(e => e.FindAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task Handle_EventNotFound_ShouldReturnSuccess()
        {
            //Arrange
            var participantList = CreateParticipantList();
            var eventSet = SetUpMocks(participantList, null);
            _dataContext.SetupGet(e => e.Participants).Returns(eventSet.Object);

            var query = new Details.Query
            {
                Code = "1",
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
            var participantList = CreateParticipantList();
            SetUpMocks(participantList, participantList[0]);
            var query = new Details.Query
            {
                Code = "1",
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsInstanceOf<Participant>(actual.Value);
        }

        private IList<Participant> CreateParticipantList()
        {
            return new List<Participant>
            {
                new Company
                {
                    Code = "1",
                },
            };
        }

        private Mock<DbSet<Participant>> SetUpMocks(
            IList<Participant> participantList,
            Participant found)
        {
            var eventSet = participantList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<string>()))
                .Returns(new ValueTask<Participant>(found));
            _dataContext.SetupGet(e => e.Participants).Returns(eventSet.Object);

            return eventSet;
        }
    }
}
