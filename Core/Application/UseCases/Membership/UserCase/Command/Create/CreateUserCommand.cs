﻿using Application.Common.Model;
using Application.Mappings;
using Common;
using Domain;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.UserCase.Command.Create
{
    public class CreateUserCommand: UserCommandBase,IRequest<CommandResponse<int>>,IMapFrom<Membership_User>
    {
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateUserCommand, Membership_User>()
                .ForMember(t=>t.PasswordHash,s=>s.MapFrom(source=>UtilizeFunction.CreateMd5(source.Password)));
        }
    }
}
