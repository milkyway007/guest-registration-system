using Application.Events;
using Application.Interfaces.Core;
using AutoMapper;
using Domain;
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
            var personDtoList = new List<PersonDto>
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
            var eventParticipantList = new List<EventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<PersonDto, object>>[]>()))
                .Returns(personDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<PersonDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((List<PersonDto>)null));

            var query = new ListPersons.Query
            {
                EventId = 1,
            };

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
            var personDtoList = new List<PersonDto>
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
            var eventParticipantList = new List<EventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<PersonDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<PersonDto, object>>[]>()))
                .Returns(personDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<PersonDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((List<PersonDto>)null));

            var query = new ListPersons.Query
            {
                EventId = 1,
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.Null(actual.Value);
        }

        [Test]
        public async Task Handle_PersonsFound_ShouldReturnSuccess()
        {
            //Arrange
            var personDtoList = new List<PersonDto>
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
            var eventParticipantList = new List<EventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<PersonDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<PersonDto, object>>[]>()))
                .Returns(personDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<PersonDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(personDtoList));

            var query = new ListPersons.Query
            {
                EventId = 1,
            };

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
            var personDtoList = new List<PersonDto>
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
            var eventParticipantList = new List<EventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<PersonDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<PersonDto, object>>[]>()))
                .Returns(personDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<PersonDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(personDtoList))
                .Callback<IQueryable<PersonDto>, CancellationToken>(
                //Assert
                (q, _) => VerifyAreSorted(q, x => x.FirstName)
                );

            var query = new ListPersons.Query
            {
                EventId = 1,
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());
        }

        public void VerifyAreSorted<T>(
            IEnumerable<PersonDto> list,
            Func<PersonDto, T> filterFunc)
        {
            var arraySorted = list.Select(filterFunc).ToArray();
            Array.Sort(arraySorted);
            CollectionAssert.AreEqual(arraySorted, list.Select(filterFunc), "Verify dates are sorted");
        }
    }
}
