using Application.Addresses.Commands;
using Application.Interfaces.Core;
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

namespace Tests.Application.Addresses
{
    [TestFixture]
    public class CreateTests
    {
        private Create.Handler _subject;
        private Mock<IDataContext> _dataContext;
        private Mock<IEntityFrameworkQueryableExtensionsAbstraction> _eFExtensionsAbstraction;

        [SetUp]
        public void SetUp()
        {
            _dataContext = new Mock<IDataContext>();
            _eFExtensionsAbstraction = new Mock<IEntityFrameworkQueryableExtensionsAbstraction>();
            _subject = new Create.Handler(
                _dataContext.Object,
                _eFExtensionsAbstraction.Object);
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
            _eFExtensionsAbstraction.Verify(x => x.AddAsync(It.IsAny<Address>(), It.IsAny<DbSet<Address>>()),
                Times.Once);
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
            Assert.IsInstanceOf<string>(actual.Value);
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

        private IList<Address> CreateAddressList()
        {
            return new List<Address>
            {
                new Address
                {
                    Zip = "2",
                },
            };
        }

        private Mock<DbSet<Address>> SetUpMocks(IList<Address> addressList, int saveResult)
        {
            var addressSet = addressList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.Addresses).Returns(addressSet.Object);
            _dataContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(saveResult));

            _eFExtensionsAbstraction.Setup(x => x.AddAsync(It.IsAny<Address>(), It.IsAny<DbSet<Address>>()))
            .Returns(Task.FromResult((Address) null));

            return addressSet;
        }

        private Create.Command CreateCommand()
        {
            return new Create.Command
            {
                Address = new Address
                {
                    Zip = "2",
                }
            };
        }

    }
}
