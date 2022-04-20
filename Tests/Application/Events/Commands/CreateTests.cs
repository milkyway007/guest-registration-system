using Application.Events.Commands;
using Application.Events.Dtos;
using Application.Interfaces.Core;
using AutoMapper;
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

namespace Tests.Application.Events
{
    [TestFixture]
    public class CreateTests
    {
        private Create.Handler _subject;
        private Mock<IDataContext> _dataContext;
        private Mock<IMapper> _mapper;
        private Mock<IEntityFrameworkQueryableExtensionsAbstraction> _eFExtensionsAbstraction;

        [SetUp]
        public void SetUp()
        {
            _dataContext = new Mock<IDataContext>();
            _mapper = new Mock<IMapper>();
            _eFExtensionsAbstraction = new Mock<IEntityFrameworkQueryableExtensionsAbstraction>();
            _subject = new Create.Handler(
                _dataContext.Object,
                _mapper.Object,
                _eFExtensionsAbstraction.Object
                );
        }

        [Test]
        public async Task Handle_ShouldAdd()
        {
            //Arrange
            var eventList = CreateEventList();
            var addressesList = CreateAddressList();
            var eventSet = SetUpMocks(eventList, addressesList, 1);
            var command = CreateCommand();

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            _eFExtensionsAbstraction.Verify(x => x.AddAsync(It.IsAny<Event>(), It.IsAny<DbSet<Event>>()), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldSaveChanges()
        {
            //Arrange
            var eventList = CreateEventList();
            var addressesList = CreateAddressList();
            var eventSet = SetUpMocks(eventList, addressesList, 1);
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
            var addressesList = CreateAddressList();
            SetUpMocks(eventList, addressesList, 1);
            var command = CreateCommand();

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsInstanceOf<Event>(actual.Value);
        }

        [Test]
        public async Task Handle_ChangesNotSaved_ShouldReturnFailure()
        {
            //Arrange
            var eventList = CreateEventList();
            var addressesList = CreateAddressList();
            var eventSet = SetUpMocks(eventList, addressesList, -1);
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
            };
        }

        private IList<Address> CreateAddressList()
        {
            return new List<Address>
            {
                new Address
                {
                    Zip = "1",
                },
            };
        }

        private Mock<DbSet<Event>> SetUpMocks(
            IList<Event> eventList,
            IList<Address> addressList,
            int saveResult)
        {
            var eventSet = eventList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Events).Returns(eventSet.Object);

            var addressSet = addressList.AsQueryable().BuildMockDbSet();
            addressSet.Setup(x => x.FindAsync(It.IsAny<string>()))
                .Returns(new ValueTask<Address>(addressList[0]));
            _dataContext.SetupGet(e => e.Addresses).Returns(addressSet.Object);

            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(saveResult));

            return eventSet;
        }

        private Create.Command CreateCommand()
        {
            return new Create.Command
            {
                Event = new EventDto
                {
                    Id = 2,
                }
            };
        }
    }
}
