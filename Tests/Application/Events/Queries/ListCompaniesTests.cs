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
    public class ListCompaniesTests
    {
        private ListCompanies.Handler _subject;
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
            _subject = new ListCompanies.Handler(
                _dataContext.Object,
                _mapper.Object,
                _eFextensionsAbstraction.Object,
                _extensionsAbstraction.Object);
        }

        [Test]
        public async Task Handle_ShouldTryList()
        {
            //Arrange
            var companyDtoList = CreateCompanyDtoList();
            var eventParticipantList = new List<EventParticipant>();
            SetUpMocks(companyDtoList, eventParticipantList, new List<CompanyDto>(), _ => { });
            var query = CreateQuery();

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            _eFextensionsAbstraction.Verify(x => x.ToListAsync(
                It.IsAny<IQueryable<CompanyDto>>(), It.IsAny<CancellationToken>()));
        }

        [Test]
        public async Task Handle_CompaniesNotFound_ShouldReturnSuccess()
        {
            //Arrange
            var companyDtoList = CreateCompanyDtoList();
            var eventParticipantList = new List<EventParticipant>();
            SetUpMocks(companyDtoList, eventParticipantList, new List<CompanyDto>(), _ => { });
            var query = CreateQuery();
            
            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsEmpty(actual.Value);
        }

        [Test]
        public async Task Handle_CompaniesFound_ShouldReturnSuccess()
        {
            //Arrange
            var companyDtoList = CreateCompanyDtoList();
            var eventParticipantList = new List<EventParticipant>();
            SetUpMocks(companyDtoList, eventParticipantList, companyDtoList, _ => { });
            var query = CreateQuery();

            //Act
            var actual = await _subject.Handle(query, new CancellationToken());

            //Assert
            Assert.True(actual.IsSuccess);
            Assert.IsInstanceOf<List<CompanyDto>>(actual.Value);
        }

        [Test]
        public async Task Handle_CompaniesFound_ShouldReturnSorted()
        {
            //Arrange
            var companyDtoList = CreateCompanyDtoList();
            var eventParticipantList = new List<EventParticipant>();
            SetUpMocks(
                companyDtoList,
                eventParticipantList,
                companyDtoList,
                e => VerifyAreSorted(e, x => x.Name));
            var query = CreateQuery();
            
            //Act
            var actual = await _subject.Handle(query, new CancellationToken());
        }

        private void VerifyAreSorted<T>(
            IEnumerable<CompanyDto> list,
            Func<CompanyDto, T> filterFunc)
        {
            var arraySorted = list.Select(filterFunc).ToArray();
            Array.Sort(arraySorted);
            CollectionAssert.AreEqual(arraySorted, list.Select(filterFunc), "Verify dates are sorted");
        }


        private ListCompanies.Query CreateQuery()
        {
            return new ListCompanies.Query
            {
                EventId = 1,
            };
        }

        private void SetUpMocks(
            List<CompanyDto> companyDtoList,
            List<EventParticipant> eventParticipantList,
            List<CompanyDto> listed,
            Action<IEnumerable<CompanyDto>> callback)
        {
            var eventParticipantSet = eventParticipantList.AsQueryable().BuildMockDbSet();
            _dataContext.SetupGet(e => e.EventParticipants).Returns(eventParticipantSet.Object);
            _extensionsAbstraction.Setup(x => x.ProjectTo(
                It.IsAny<IQueryable>(),
                It.IsAny<IConfigurationProvider>(),
                It.IsAny<Expression<Func<CompanyDto, object>>[]>()))
                .Returns(companyDtoList.AsQueryable());
            _eFextensionsAbstraction.Setup(x => x.ToListAsync(
                It.IsAny<IQueryable<CompanyDto>>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(listed))
                .Callback<IQueryable<CompanyDto>, CancellationToken>(
                //Assert
                (e, _) => callback(e)
                );
        }

        private List<CompanyDto> CreateCompanyDtoList()
        {
            return new List<CompanyDto>
            {
                new CompanyDto
                {
                    Name = "Anna",
                },
                new CompanyDto
                {
                    Name = "Maria",
                },
                new CompanyDto
                {
                    Name = "Kalju",
                },
                new CompanyDto
                {
                    Name = "ABC",
                },
                new CompanyDto
                {
                    Name = "XYZ",
                },
                new CompanyDto
                {
                    Name = "QWERTY",
                },
            };
        }
    }
}
