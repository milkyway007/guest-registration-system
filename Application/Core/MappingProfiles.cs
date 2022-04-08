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
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code));

            CreateMap<Person, PersonDto>()
            .IncludeBase<Participant, ParticipantDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName));

            CreateMap<Company, CompanyDto>()
            .IncludeBase<Participant, ParticipantDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<EventParticipant, EventParticipantDto>()
            .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Event.Id))
            .ForMember(dest => dest.Participant, opt => opt.MapFrom(src => src.Participant));
        }
    }
}
