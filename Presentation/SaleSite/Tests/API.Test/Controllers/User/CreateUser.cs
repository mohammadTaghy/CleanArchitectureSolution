using API.Test.Common;
using Application.UseCases.UserCase.Command.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Test.Controllers.User
{
    public class CreateUser : IDisposable
    {
        private readonly HttpClient _client;

        public CreateUser()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            };
        }

        [Fact]
        public async Task GivenCreateCustomerCommand_ReturnsSuccessStatusCode()
        {
            

            var command = new CreateUserCommand
            {
                Email="mohammad@gmail.com",
                MobileNumber="0230",
                Password="123456",
                UserName="MTY"
            };

            var content = Utilities.GetRequestContent(command);

            var response = await _client.PostAsync($"/api/User/CreateUser", content);

            response.EnsureSuccessStatusCode();
        }
        public void Dispose()
        {
            
        }
    }
}
