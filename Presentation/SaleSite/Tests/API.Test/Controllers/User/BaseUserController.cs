using API.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.Controllers.User
{
    public class BaseUserController:BaseController, IDisposable
    {
        protected readonly UserController _userController;
        public BaseUserController()
        {
            _userController = new UserController(_mediator.Object);
        }

        public void Dispose()
        {
        }
    }
}
