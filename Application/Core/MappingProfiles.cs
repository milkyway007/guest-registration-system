using Application.Events.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Entity, Entity>()
            .ForMember(dest => dest.Created, opt => opt.Ignore());

            CreateMap<Event,Event>()
                .IncludeBase<Entity, Entity>();

            CreateMap<Participant, ParticipantDto>()
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));

            CreateMap<Person, PersonDto>()
            .IncludeBase<Participant, ParticipantDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

            CreateMap<Company, CompanyDto>()
            .IncludeBase<Participant, ParticipantDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Participant, Participant>()
            .IncludeBase<Entity, Entity>();
            CreateMap<Person, Person>()
              .IncludeBase<Participant, Participant>();
            CreateMap<Company, Company>()
              .IncludeBase<Participant, Participant>();

        }
    }
}
