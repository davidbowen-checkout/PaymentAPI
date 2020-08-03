using Payment.Gateway.Api.Database;
using Payment.Gateway.Api.DTO;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using Bank.Core.Api;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Bank.Core.Api.Interface;

namespace Payment.Gateway.Agent
{
    class Program
    {


        private const long BANK_ACCOUNT = 9999999999999999; // We're transferring the money to this account. In this example this would be the payment acceptor. This should be held in a secret store.
        private const int SORT_CODE = 999999;


        static async Task Main(string[] args)
        {
            HttpClient client = new HttpClient();
          
            IBank bank = new ExampleBank();
            var paymentProcessing = new PaymentProcessingTask(client);


            while (true)
            {
               var payment = await   paymentProcessing.checkForJobs();
                if (payment != null)
                {
                    var paidPayment = await paymentProcessing.enactPayment(bank, SORT_CODE, BANK_ACCOUNT, payment);
                    await paymentProcessing.UpdatePayment(paidPayment); // Update the payment status

                }

                await Task.Delay(10000);//Check every 10 seconds. Ideally we'd use SignalR or a message bus to handle this in a better way than continuous polling.

            }
        }
     
    }
}
