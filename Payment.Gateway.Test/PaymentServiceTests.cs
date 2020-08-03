using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Payment.Gateway.Api.Database;
using Payment.Gateway.Api.DTO;
using Payment.Gateway.Api.Interface;
using Payment.Gateway.Api.Service;
using RestSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore.InMemory;
using Bank.Core.Api;

namespace Payment.Gateway.Test
{
    [TestClass]
    public class PaymentServiceTests
    {
        private readonly PaymentService _paymentService;




        public PaymentServiceTests()
        {
            var context = new PaymentsContext(true);
            IPaymentUniqueIDBuilder<PaymentData> builder = new PaymentMd5UniqueId();
            PaymentsDatabaseRepository repo = new PaymentsDatabaseRepository(context, builder);
            _paymentService = new PaymentService(repo);
        }



        [TestMethod]
        public void TestRequestPayment()
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

            var value = _paymentService.RequestPayment(payment1);
            Assert.AreEqual(value.PaymentId, payment1.PaymentId); // If a unique ID is passed in is it overwritten?
            Assert.IsNotNull(value.TransactionDate);// Is the transaction date successfully set.
        }



        [TestMethod]
        public void TestGetPayment()
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

            var value = _paymentService.RequestPayment(payment1);


            var val = _paymentService.GetPaymentDetails(value.PaymentId);
            Assert.AreEqual(val.BankAccountNumber, value.BankAccountNumber);
            Assert.AreEqual(val.SortCode, value.SortCode);
            Assert.AreEqual(val.PaymentValue, value.PaymentValue);


        }


        [TestMethod]
        public void TestUpdatingARecord()
        {
            var payment1 = new PaymentData()
            {
                PaymentId = "Unique12346",
                BankAccountNumber = 1234567890123457, //Different account number
                SortCode = 121212,
                CardHolderName = "Tester",
                CcvNumber = 123,
                PaymentValue = 1000.00,
                Status = PaymentStatus.Submitted
            };

            _paymentService.RequestPayment(payment1); // Add the  payment
            payment1.Status = PaymentStatus.Successful; // Change the status            
            _paymentService.UpdatePayment(payment1); //Update the status.
            var val = _paymentService.UpdatePayment(payment1);


            Assert.AreEqual(val.CardHolderName, payment1.CardHolderName);
            Assert.AreEqual(val.Status, PaymentStatus.Successful);
        }


    }
}
