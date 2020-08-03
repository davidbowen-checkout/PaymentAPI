using Bank.Core.Api;
using Bank.Core.Api.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using Payment.Gateway.Agent;
using Payment.Gateway.Api.DTO;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Payment.Gateway.Agent.Test
{
    [TestClass]
    public class PaymentProcessingTaskTest
    {
        Mock<HttpMessageHandler> _handler;

        private readonly PaymentProcessingTask _processingTask;

        private readonly PaymentData _testPayment;

        public PaymentProcessingTaskTest()
        {

            var payment1 = new PaymentData()
            {
                PaymentId = "Unique12347",
                BankAccountNumber = 1234567890123456,
                SortCode = 121212,
                CardHolderName = "David Bowen",
                CcvNumber = 123,
                PaymentValue = 1000.00,
                Status = PaymentStatus.Submitted
            };
            _testPayment = payment1;
            var paymentToProcess = JsonConvert.SerializeObject(payment1);

            _handler = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _handler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(  "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(paymentToProcess),
                })
                .Verifiable();

            var httpClient = new HttpClient(_handler.Object)
            {
                BaseAddress = new Uri("https://localhost:58447/Tasks/"),
            };



             _processingTask = new PaymentProcessingTask(httpClient);


        }

        [TestMethod]        
        public void TestCheckForJobs()
        {

            var val = _processingTask.checkForJobs().Result;

            Assert.IsNotNull(val);
        }



        [TestMethod]
        public void TestMakingPayment()
        {

            BankResponse response = new BankResponse() { PaymentStatus = BankPaymentStatus.Successfull, UniqueIdentifier = "12321312312312312" };

            Mock<IBank> bank = new Mock<IBank>();
            bank.Setup(p => p.TransferFunds(It.IsAny<long>(), It.IsAny<int>(), It.IsAny<long>(), It.IsAny<int>(), It.IsAny<double>())).Returns(response);
            var val = _processingTask.enactPayment(bank.Object, 111111, 1234567890123456, _testPayment).Result;

            Assert.IsNotNull(val);
        }


        [TestMethod]
        public void TestUpdatePayment()
        {
            var payment1 = new PaymentData()
            {
                PaymentId = "Unique12347",
                BankAccountNumber = 1234567890123456,
                SortCode = 121212,
                CardHolderName = "David Bowen",
                CcvNumber = 123,
                PaymentValue = 1000.00,
                Status = PaymentStatus.Successful
            };

            var val = _processingTask.UpdatePayment(payment1).Result;

            Assert.AreEqual(val, true);
            Assert.IsNotNull(val);
        }
    }
}
