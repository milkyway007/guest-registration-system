using Application.Events.Dtos;
using Application.Events.Queries;
using Application.Interfaces.Core;
using AutoMapper;
using Domain.Entities;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;


namespace Tests.Application.Events
{
    [TestFixture]
    public class ListPersonsTests
    {
        private ListPersons.Handler _subject;
        private Mock<IDataContext> _dataContext;
        private Mock<IMapper> _mapper;
        private Mock<IEntityFrameworkQueryableExtensionsAbstraction> _eFextensionsAbstraction;
        private Mock<IQueryableExtensionsAbstraction> _extensionsAbstraction;

        [SetUp]
        public void SetUp()
        {
            _dataContext = new Mock<IDataContext>();
            _mapper = new Mock<IMapper>();
            _eFextensionsAbstraction = new Mock<IEntityFrameworkQueryableExtensionsAbstraction>();
            _extensionsAbstraction = new Mock<IQueryableExtensionsAbstraction>();
            _subject = new ListPersons.Handler(
                _dataContext.Object,
                _mapper.Object,
                _eFextensionsAbstraction.Object,
                _extensionsAbstraction.Object);
        }

        [Test]
        public async Task Handle_ShouldTryList()
        {
            //Arrange
            var personDtoList = CreatePersonDtoList();
            var eventParticipantList = new List<EventParticipant>();
            SetUpMocks(
                personDtoList,
                eventParticipantList,
                personDtoList,
                e => { });
            var query = CreateQuery();

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            _eFextensionsAbstraction.Verify(x => x.ToListAsync(
                It.IsAny<IQueryable<PersonDto>>(), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task Handle_PersonsNotFound_ShouldReturnSuccess()
        {
            //Arrange
            var personDtoList = CreatePersonDtoList();
            var eventParticipantList = new List<EventParticipant>();
            SetUpMocks(
                personDtoList,
                eventParticipantList,
                new List<PersonDto>(),
                e => { });
            var query = CreateQuery();

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsEmpty(actual.Value);
        }

        [Test]
        public async Task Handle_PersonsFound_ShouldReturnSuccess()
        {
            //Arrange
            var personDtoList = CreatePersonDtoList();
            var eventParticipantList = new List<EventParticipant>();
            SetUpMocks(
                personDtoList,
                eventParticipantList,
                personDtoList,
                e => { });
            var query = CreateQuery();

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsInstanceOf<List<PersonDto>>(actual.Value);
        }

        [Test]
        public async Task Handle_PersonsFound_ShouldReturnSorted()
        {
            //Arrange
            var personDtoList = CreatePersonDtoList();
            var eventParticipantList = new List<EventParticipant>();
            SetUpMocks(
                personDtoList,
                eventParticipantList,
                personDtoList,
                e => VerifyAreSorted(e, x => x.FirstName));
            var query = CreateQuery();

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());
        }

        private void VerifyAreSorted<T>(
            IEnumerable<PersonDto> list,
            Func<PersonDto, T> filterFunc)
        {
            var arraySorted = list.Select(filterFunc).ToArray();
            Array.Sort(arraySorted);
            CollectionAssert.AreEqual(arraySorted, list.Select(filterFunc), "Verify dates are sorted");
        }

        private ListPersons.Query CreateQuery()
        {
            return new ListPersons.Query
            {
                EventId = 1,
            };
        }

        private void SetUpMocks(
            List<PersonDto> personDtoList,
            List<EventParticipant> eventParticipantList,
            List<PersonDto> listed,
            Action<IEnumerable<PersonDto>> callback)
        {
            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<PersonDto, object>>[]>()))
                .Returns(personDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<PersonDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(listed))
                .Callback<IQueryable<PersonDto>, CancellationToken>(
                //Assert
                (e, _) => callback(e)
                );
        }

        private List<PersonDto> CreatePersonDtoList()
        {
            return new List<PersonDto>
            {
                new PersonDto
                {
                    FirstName = "Anna",
                },
                new PersonDto
                {
                    FirstName = "Maria",
                },
                new PersonDto
                {
                    FirstName = "Kalju",
                },
                new PersonDto
                {
                    FirstName = "ABC",
                },
                new PersonDto
                {
                    FirstName = "XYZ",
                },
                new PersonDto
                {
                    FirstName = "QWERTY",
                },
            };
        }
    }
}
