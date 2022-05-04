using Common;
using Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validation
{
    public sealed class UserValidation : ValidationRuleBase<IUser>, IUserValidation
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
                .WithMessage(string.Format(CommonMessage.IsDuplicateUserName, nameof(IUser.UserName)));
        }

    }
}
