﻿namespace Application.Events
{
    public class EventParticipantDto
    {
        public int EventId { get; set; }
        public ParticipantDto Participant { get; set; }
        public bool IsCompany => Participant is CompanyDto;
    }
}
