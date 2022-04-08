using Application.Events;
using Application.Interfaces;
using Application.Interfaces.Core;
using AutoMapper;
using AutoMapper.Internal;
using Domain;
using Domain.Interfaces;
using MockQueryable.Moq;
using Moq;
using NUnit.Framework;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tests.Application.Events
{
    [TestFixture]
    public class ListParticipantsTests
    {
        private ListParticipants.Handler _subject;
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
            _subject = new ListParticipants.Handler(
                _dataContext.Object,
                _mapper.Object,
                _eFextensionsAbstraction.Object,
                _extensionsAbstraction.Object);
        }

        [Test]
        public async Task Handle_ShouldTryList()
        {
            //Arrange
            var eventParticipantDtoList = new List<EventParticipantDto>
            {
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Anna",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Maria",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Kalju",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "ABC",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "XYZ",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "QWERTY",
                    },
                }
            };
            var eventParticipantList = new List<IEventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<EventParticipantDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<EventParticipantDto, object>>[]>()))
                .Returns(eventParticipantDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<EventParticipantDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((List<EventParticipantDto>)null));

            var query = new ListParticipants.Query
            {
                EventId = 1,
                Predicate = "person",
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            _eFextensionsAbstraction.Verify(x => x.ToListAsync(
                It.IsAny<IQueryable<EventParticipantDto>>(), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task Handle_EventParticipantsNotFound_ShouldReturnSuccess()
        {
            //Arrange
            var eventParticipantDtoList = new List<EventParticipantDto>
            {
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Anna",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Maria",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Kalju",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "ABC",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "XYZ",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "QWERTY",
                    },
                }
            };
            var eventParticipantList = new List<IEventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<EventParticipantDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<EventParticipantDto, object>>[]>()))
                .Returns(eventParticipantDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<EventParticipantDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((List<EventParticipantDto>)null));

            var query = new ListParticipants.Query
            {
                EventId = 1,
                Predicate = "person",
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
            var eventParticipantDtoList = new List<EventParticipantDto>
            {
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Anna",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Maria",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Kalju",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "ABC",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "XYZ",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "QWERTY",
                    },
                }
            };
            var eventParticipantList = new List<IEventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<EventParticipantDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<EventParticipantDto, object>>[]>()))
                .Returns(eventParticipantDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<EventParticipantDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((eventParticipantDtoList)));

            var query = new ListParticipants.Query
            {
                EventId = 1,
                Predicate = "person",
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsInstanceOf<List<EventParticipantDto>>(actual.Value);
        }

        [Test]
        public async Task Handle_EventFound_ShouldReturnPersons()
        {
            //Arrange
            var eventParticipantDtoList = new List<EventParticipantDto>
            {
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Anna",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Maria",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Kalju",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "ABC",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "XYZ",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "QWERTY",
                    },
                }
            };
            var eventParticipantList = new List<IEventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<EventParticipantDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<EventParticipantDto, object>>[]>()))
                .Returns(eventParticipantDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<EventParticipantDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((eventParticipantDtoList)))
                .Callback<IQueryable<EventParticipantDto>, CancellationToken>(
                //Assert
                (q, _) => Assert.True(q.All(x => x.Participant is PersonDto))
                );

            var query = new ListParticipants.Query
            {
                EventId = 1,
                Predicate = "person",
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());
        }

        [Test]
        public async Task Handle_EventFound_ShouldReturnSortedPersons()
        {
            //Arrange
            var eventParticipantDtoList = new List<EventParticipantDto>
            {
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Anna",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Maria",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Kalju",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "ABC",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "XYZ",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "QWERTY",
                    },
                }
            };
            var eventParticipantList = new List<IEventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<EventParticipantDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<EventParticipantDto, object>>[]>()))
                .Returns(eventParticipantDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<EventParticipantDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((eventParticipantDtoList)))
                .Callback<IQueryable<EventParticipantDto>, CancellationToken>(
                //Assert
                (q, _) => VerifyAreSorted<string>(q, x => ((PersonDto)x.Participant).FirstName)
                );

            var query = new ListParticipants.Query
            {
                EventId = 1,
                Predicate = "person",
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());
        }

        [Test]
        public async Task Handle_EventFound_ShouldReturnCompanies()
        {
            //Arrange
            var eventParticipantDtoList = new List<EventParticipantDto>
            {
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Anna",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Maria",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Kalju",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "ABC",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "XYZ",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "QWERTY",
                    },
                }
            };
            var eventParticipantList = new List<IEventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<EventParticipantDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<EventParticipantDto, object>>[]>()))
                .Returns(eventParticipantDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<EventParticipantDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((eventParticipantDtoList)))
                .Callback<IQueryable<EventParticipantDto>, CancellationToken>(
                //Assert
                (q, _) => Assert.True(q.All(x => x.Participant is CompanyDto))
                );

            var query = new ListParticipants.Query
            {
                EventId = 1,
                Predicate = "company",
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());
        }

        [Test]
        public async Task Handle_EventFound_ShouldReturnSortedCompanies()
        {
            //Arrange
            var eventParticipantDtoList = new List<EventParticipantDto>
            {
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Anna",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Maria",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Kalju",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "ABC",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "XYZ",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "QWERTY",
                    },
                }
            };
            var eventParticipantList = new List<IEventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<EventParticipantDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<EventParticipantDto, object>>[]>()))
                .Returns(eventParticipantDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<EventParticipantDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((eventParticipantDtoList)))
                .Callback<IQueryable<EventParticipantDto>, CancellationToken>(
                //Assert
                (q, _) => VerifyAreSorted<string>(q, x => ((CompanyDto)x.Participant).Name)
                );

            var query = new ListParticipants.Query
            {
                EventId = 1,
                Predicate = "company",
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());
        }

        [Test]
        public async Task Handle_EventFound_ShouldReturnSortedParticipants()
        {
            //Arrange
            var eventParticipantDtoList = new List<EventParticipantDto>
            {
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Anna",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Maria",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new PersonDto
                    {
                        FirstName = "Kalju",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "ABC",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "XYZ",
                    },
                },
                new EventParticipantDto
                {
                    EventId = 1,
                    Participant = new CompanyDto
                    {
                        Name = "QWERTY",
                    },
                }
            };
            var eventParticipantList = new List<IEventParticipant>();

            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo<EventParticipantDto>(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<EventParticipantDto, object>>[]>()))
                .Returns(eventParticipantDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<EventParticipantDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult((eventParticipantDtoList)))
                .Callback<IQueryable<EventParticipantDto>, CancellationToken>(
                //Assert
                (q, _) => VerifyAreSorted<int>(q, x => x.Participant.Id)
                );

            var query = new ListParticipants.Query
            {
                EventId = 1,
                Predicate = "person",
            };

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            VerifyAreSorted<int>(actual.Value, x => x.Participant.Id);
        }

        public void VerifyAreSorted<T>(
            IEnumerable<EventParticipantDto> list,
            Func<EventParticipantDto, T> filterFunc)
        {
            var arraySorted = list.Select(filterFunc).ToArray();
            Array.Sort(arraySorted);
            CollectionAssert.AreEqual(arraySorted, list.Select(filterFunc), "Verify dates are sorted");
        }
    }
}
