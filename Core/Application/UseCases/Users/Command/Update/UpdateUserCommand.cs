using Application.Common;
using Application.Common.Exceptions;
using Application.Decorators;
using Application.Validation;
using Common;
using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Users.Command.Update
{
    public class UpdateUserCommand : ICommand
    {

        public UpdateUserCommand(string userName, string firstName, string lastName, int id)
        {
            UserName = userName;
            Id = id;
            FirstName = firstName;
            LastName = lastName;

        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [AuditLog]
        public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
        {
            private IUserValidation userValidation;
            private IUserRepo userRepo;
            public UpdateUserCommandHandler(IUserValidation userValidation, IUserRepo userRepo)
            {
                this.userRepo = userRepo;
                this.userValidation = userValidation;
            }
            public Result Handle(UpdateUserCommand command, CancellationToken cancellationToken)
            {
                userRepo.CancellationToken = cancellationToken;

                User user = userRepo.FindAsync(command.Id).Result;

                if (user == null)
                {
                    return Result.Fail(string.Format(CommonMessage.NotFound, command.UserName));
                }

               
                user.UserName = command.UserName;
                Result result;
                var validate = userValidation.ValidateAsync(user, cancellationToken);
                if (validate.IsFaulted)
                    result = Result.Fail(validate.ToString());
                else
                {
                    userRepo.Save();
                    result = Result.Ok(string.Format(CommonMessage.SucceedUpdate, command.UserName));
                }
                ///Call event for insert into read DB
                return result;
            }
        }
    }
}
