using Application.Common.Exceptions;
using Application.Common.Model;
using Application.Mappings;
using Application.Validation;
using AutoMapper;
using Common;
using Domain;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.UserCase.Command.Update
{
    public class UpdateUserCommand :UserCommandBase, IRequest<CommandResponse<bool>>, IMapFrom<User>
    {
        public int Id { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateUserCommand, User>();
        }
    }
}
