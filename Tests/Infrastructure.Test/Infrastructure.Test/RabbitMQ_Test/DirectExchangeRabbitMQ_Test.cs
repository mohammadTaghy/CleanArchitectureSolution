using Common;
using Infrastructure.RabbitMQ;
using Microsoft.Extensions.Options;
using Moq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.Test.RabbitMQ_Test
{
    public class DirectExchangeRabbitMQ_Test
    {
        private readonly IOptions<AppSettings> option;
        private readonly DirectExchangeRabbitMQ directQueue;

        public DirectExchangeRabbitMQ_Test()
        {
            option=Options.Create<AppSettings>(new AppSettings()
            {
                RabbitMQHostName = "localhost",
                SecretKey= "F?Q2y+8bl5DQZJtw>fCY>}Z|Q=Ir#U?Y@o3(B)}[i~CK{<6yYUQn?!P6hYvhx><"
            });
            directQueue = new DirectExchangeRabbitMQ(option);
        }
        [Fact]
        public async Task PublishSub_CannotRecieveMessageInDifferentRouting_ResultTest()
        {
            string result = "test message";
            string recieveMessage = "";
            directQueue.RecieveMessage(new Application.Common.Model.RabbitMQRecieveRequest
            {
                RoutingKey = "",
                QueueName = "Test",
                EventHandler = (model, args) =>
                {
                    var body = args.Body.ToArray();
                    recieveMessage = Encoding.UTF8.GetString(body);

                }
            });
            directQueue.SendMessage(new Application.Common.Model.RabbitMQSendRequest
            {
                Message = result,
                QueueName = "Test",
                RoutingKey = "Test_Routing"
            });
            await Task.Delay(5000);

            Assert.Equal("", recieveMessage);
        }
        [Fact]
        public async Task PublishSub_EmptyQueueTest_ResultTest()
        {
            string result = "test message";
            string recieveMessage = "";
            directQueue.RecieveMessage(new Application.Common.Model.RabbitMQRecieveRequest
            {
                RoutingKey = "Test_Message",
                EventHandler = (model, args) =>
                {
                    var body = args.Body.ToArray();
                    recieveMessage = Encoding.UTF8.GetString(body);

                }
            });
            directQueue.SendMessage(new Application.Common.Model.RabbitMQSendRequest
            {
                Message = result,
                RoutingKey = "Test_Message"
            });
            await Task.Delay(5000);

            Assert.Equal(result, recieveMessage);
        }
        [Fact]
        public void PublishSub_SuccessfulTest_ResultTest()
        {
            string result = "test message";
            string recieveMessage = "";
            directQueue.RecieveMessage(new Application.Common.Model.RabbitMQRecieveRequest
            {
                RoutingKey = "Test_Message",
                QueueName = "Test",
                EventHandler = (model, args) =>
                {
                    var body = args.Body.ToArray();
                    recieveMessage = Encoding.UTF8.GetString(body);
                    
                }
            });
            directQueue.SendMessage(new Application.Common.Model.RabbitMQSendRequest
            {
                Message = result,
                QueueName = "Test",
                RoutingKey = "Test_Message"
            });
            Thread.Sleep(5000);

            Assert.Equal(result, recieveMessage);
        }
    }
}
