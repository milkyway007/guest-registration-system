using Application.Addresses.Commands;
using Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/*
namespace Tests.Application.Addresses
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
            var addressList = CreateAddressList();
            var addressSet = SetUpMocks(addressList, 1);
            var command = CreateCommand();

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            addressSet.Verify(x => x.Add(It.IsAny<Address>()), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldSaveChanges()
        {
            //Arrange
            var addressList = CreateAddressList();
            var addressSet = SetUpMocks(addressList, 1);
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
            var addressList = CreateAddressList();
            SetUpMocks(addressList, 1);
            var command = CreateCommand();

            //Act
            var actual = await _subject.Handle(command, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsInstanceOf<int>(actual.Value);
        }

        [Test]
        public async Task Handle_ChangesNotSaved_ShouldReturnFailure()
        {
            //Arrange
            var addressList = CreateAddressList();
            SetUpMocks(addressList, -1);
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
                Address = new Address
                {
                    Id = 2,
                }
            };
        }

        private Mock<DbSet<Address>> SetUpMocks(List<Address> addressList, int saveResult)
        {
            var addressSet = addressList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Addresses).Returns(addressSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(saveResult));

            return addressSet;
        }

        private List<Address> CreateAddressList()
        {
            return new List<Address>
            {
                new Address
                {
                    Id = 1,
                },
            };
        }
    }
}*/
