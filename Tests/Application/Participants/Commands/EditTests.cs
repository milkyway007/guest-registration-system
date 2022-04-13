﻿using Application.Participants.Commands;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
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
            var eventList = new List<Participant>
            {
                new Company
                {
                    Code = "1",
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<string>()))
                .Returns(null);
            _dataContext.SetupGet(e => e.Participants).Returns(eventSet.Object);

            var command = new Edit.Command
            {
                Participant = new Company
                {
                    Code = "1",
                }
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            eventSet.Verify(e => e.FindAsync(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task Handle_EventNotFound_ShouldReturnNull()
        {
            //Arrange
            var eventList = new List<Participant>
            {
                new Company
                {
                    Code = "1",
                },
            };

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<int>()))
                .Returns(null);
            _dataContext.SetupGet(e => e.Participants).Returns(eventSet.Object);

            var command = new Edit.Command
            {
                Participant = new Company
                {
                    Code = "1",
                }
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.Null(actual);
        }

        [Test]
        public async Task Handle_EventFound_ShouldMap()
        {
            //Arrange
            var eventList = new List<Participant>
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

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<string>()))
                .Returns(new ValueTask<Participant>(eventList[1]));
            _dataContext.SetupGet(e => e.Participants).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            var command = new Edit.Command
            {
                Participant = new Company
                {
                    Code = "2",
                }
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            _mapper.Verify(x => x.Map(It.IsAny<Participant>(), It.IsAny<Participant>()), Times.Once);
        }

        [Test]
        public async Task Handle_EventFound_ShouldSaveChanges()
        {
            //Arrange
            var eventList = new List<Participant>
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

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<string>()))
                .Returns(new ValueTask<Participant>(eventList[1]));
            _dataContext.SetupGet(e => e.Participants).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            var command = new Edit.Command
            {
                Participant = new Company
                {
                    Code = "2",
                }
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
            var eventList = new List<Participant>
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

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<string>()))
                .Returns(new ValueTask<Participant>(eventList[1]));
            _dataContext.SetupGet(e => e.Participants).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(1));

            var command = new Edit.Command
            {
                Participant = new Company
                {
                    Code = "2",
                }
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
            var eventList = new List<Participant>
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

            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _ = eventSet.Setup(e => e.FindAsync(It.IsAny<string>()))
                .Returns(new ValueTask<Participant>(eventList[1]));
            _dataContext.SetupGet(e => e.Participants).Returns(eventSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(-1));

            var command = new Edit.Command
            {
                Participant = new Company
                {
                    Code = "2",
                }
            };

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.False(actual.IsSuccess);
            Assert.False(string.IsNullOrWhiteSpace(actual.Error));
        }
    }
}

