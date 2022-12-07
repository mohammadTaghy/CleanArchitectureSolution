using Application.Common.Model;
using Application.Mappings;
using Application.UseCases.UserCase.Command;
using Application.UseCases.UserCase.Command.Create;
using Application.UseCases.UserProfileCase.Command.Create;
using Application.UseCases.UserProfileCase.Query.GetUserList;
using Common;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases
{
    public class DeleteUserProfileCommand: IRequest<CommandResponse<bool>>
    {
        public int Id { get; set; }
    }
}
