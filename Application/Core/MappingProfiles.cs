using Application.Events.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Event,Event>();

            CreateMap<Participant, ParticipantDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.IsPerson, opt => opt.MapFrom(_ => true));

            CreateMap<Person, PersonDto>()
            .IncludeBase<Participant, ParticipantDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

            CreateMap<Company, CompanyDto>()
            .IncludeBase<Participant, ParticipantDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Participant, Participant>();
            CreateMap<Person, Person>()
              .IncludeBase<Participant, Participant>();
            CreateMap<Company, Company>()
              .IncludeBase<Participant, Participant>();

        }
    }
}
