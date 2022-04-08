﻿using Domain;
using FluentValidation;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Participants
{
    public class PersonValidator : ParticipantValidator<Person>
    {
        public PersonValidator(DataContext dataContext) : base(dataContext)
        {
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Description).MaximumLength(1500);
        }
    }
}