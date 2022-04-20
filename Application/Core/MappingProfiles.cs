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
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
            .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod));

            CreateMap<Person, PersonDto>()
            .IncludeBase<Participant, ParticipantDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Company, CompanyDto>()
            .IncludeBase<Participant, ParticipantDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ParticipantCount, opt => opt.MapFrom(src => src.ParticipantCount))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<Participant, Participant>()
            .IncludeBase<Entity, Entity>();
            CreateMap<Person, Person>()
              .IncludeBase<Participant, Participant>();
            CreateMap<Company, Company>()
              .IncludeBase<Participant, Participant>();

            CreateMap<EventDto, Event>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Occurrence,
            opt => opt.MapFrom(src =>
            DateTime.ParseExact(src.Occurrence, Constants.EVENT_DTO_DATE_FORMAT, null)))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Address, opt => opt.Ignore());

        }
    }
}
