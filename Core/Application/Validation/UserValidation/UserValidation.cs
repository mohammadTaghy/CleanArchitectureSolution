using Common;
using Domain;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public sealed class UserValidation : ValidationRuleBase<User>, IUserValidation
    {
        public IUserRepoRead UserRepo { get; set; }
        public UserValidation(IUserRepoRead repo)
        {
            this.UserRepo = repo;
            AddCheckValidation();
        }
        public override void AddCheckValidation()
        {
            base.AddCheckValidation();
            RuleFor(p => p.UserName).Must(
                (rootObject, list, context) => 
                UserRepo.CheckUniqUserName(rootObject.UserName, rootObject.Id))
                .WithMessage(string.Format(CommonMessage.IsDuplicateUserName, nameof(User.UserName)));
        }

    }
}
