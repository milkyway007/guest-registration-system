using Application.Events;
using AutoMapper;
using Domain;

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
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

            CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<EventParticipant, EventParticipantDto>()
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dest => dest.ParticipantId, opt => opt.MapFrom(src => src.ParticipantId))
                .ForMember(dest => dest.Participant, opt => opt.MapFrom(src => src.Participant));

            CreateMap<Participant, Participant>();
            CreateMap<Person, Person>()
              .IncludeBase<Participant, Participant>();
            CreateMap<Company, Company>()
              .IncludeBase<Participant, Participant>();

        }
    }
}
