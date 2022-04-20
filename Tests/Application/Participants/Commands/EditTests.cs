using Application.Participants.Commands;
using AutoMapper;
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


namespace Tests.Application.Participants
{
    [TestFixture]
    public class EditTests
    {
        private Edit.Handler _subject;
        private Mock<IDataContext> _dataContext;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void SetUp()
        {
            _dataContext = new Mock<IDataContext>();
            _mapper = new Mock<IMapper>();
            _subject = new Edit.Handler(_dataContext.Object, _mapper.Object);
        }

        [Test]
        public async Task Handle_ShouldTryFind()
        {
            //Arrange
            var participants = CreateParticipants();
            Mock<DbSet<Participant>> participantSet = SetUpMocks(participants, null, -1);
            var command = CreateCommand("1");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            participantSet.Verify(e => e.FindAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task Handle_EventNotFound_ShouldReturnNull()
        {
            //Arrange
            var participants = CreateParticipants();
            SetUpMocks(participants, null, -1);
            var command = CreateCommand("1");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.Null(actual);
        }

        [Test]
        public async Task Handle_EventFound_ShouldMap()
        {
            //Arrange
            var participants = CreateParticipants();
            SetUpMocks(participants, participants[1], 1);
            var command = CreateCommand("2");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            _mapper.Verify(x => x.Map(It.IsAny<Participant>(), It.IsAny<Participant>()), Times.Once);
        }

        [Test]
        public async Task Handle_EventFound_ShouldSaveChanges()
        {
            //Arrange
            var participants = CreateParticipants();
            SetUpMocks(participants, participants[1], 1);
            var command = CreateCommand("2");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            _dataContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task Handle_ChangesSaved_ShouldReturnSuccess()
        {
            //Arrange
            var participants = CreateParticipants();
            SetUpMocks(participants, participants[1], 1);
            var command = CreateCommand("2");

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
            var participants = CreateParticipants();
            SetUpMocks(participants, participants[1], -1);
            var command = CreateCommand("2");

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(actual.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(actual.Error));
        }

        private Edit.Command CreateCommand(string code)
        {
            return new Edit.Command
            {
                Participant = new Company
                {
                    Code = code,
                }
            };
        }

        private Mock<DbSet<Participant>> SetUpMocks(
            IList<Participant> participants,
            Participant found,
            int saveResult)
        {
            var participantSet = participants.AsQueryable().BuildMockDbSet();
            _ = participantSet.Setup(e => e.FindAsync(It.IsAny<string>()))
                .Returns(new ValueTask<Participant>(found));
            _dataContext.SetupGet(e => e.Participants).Returns(participantSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(saveResult));

            return participantSet;
        }

        private IList<Participant> CreateParticipants()
        {
            return new List<Participant>
            {
                new Company
                {
                    Code = "1",
                },
                new Company
                {
                    Code = "2",
                },
            };
        }

    }
}

