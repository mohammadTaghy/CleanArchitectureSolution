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
    public sealed class UserValidation : ValidationRuleBase<Membership_User>, IUserValidation
    {
        public IUserRepo UserRepo { get; set; }
        public UserValidation(IUserRepo repo)
        {
            this.UserRepo = repo;
            AddCheckValidation();
        }
        public override void AddCheckValidation()
        {
            base.AddCheckValidation();
            //RuleFor(p => p.UserName).Must(
            //    (rootObject, list, context) =>
            //    UserRepo.AnyEntity(p => p.UserName == rootObject.UserName || p.Id == rootObject.Id).Result)
            //    .WithMessage(string.Format(CommonMessage.IsDuplicateUserName, nameof(User.UserName)));
        }

    }
}
