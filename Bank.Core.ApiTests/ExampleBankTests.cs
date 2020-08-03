using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Bank.Core.Api.Interface;

namespace Bank.Core.Api.Tests
{
    [TestClass()]
    public class ExampleBankTests
    {

        private readonly IBank _exampleBank;
        public ExampleBankTests()
        {

            List<SetupData> data = new List<SetupData>();
            data.Add(new SetupData() { BankAccount = 1234567890123456, SortCode = 123456, Value = 1000 });
            data.Add(new SetupData() { BankAccount = 1234567890123457, SortCode = 999999, Value = 1000 });
            data.Add(new SetupData() { BankAccount = 1111111111111111, SortCode = 888888, Value = 1000 });
            data.Add(new SetupData() { BankAccount = 1111111111111111, SortCode = 888888, Value = 500.12 });
            _exampleBank = new ExampleBank(data);
        }

      

        [TestMethod()]
        public void GetBalanceTest()
        {
            var val = _exampleBank.GetBalance(1234567890123456, 123456);

            Assert.AreEqual(val, 1000);

            var val2 = _exampleBank.GetBalance(1111111111111111, 888888);

            Assert.AreEqual(val2, 1500.12);

            
        }

        [TestMethod()]
        public void TransferFundsTest()
        {
           var success= _exampleBank.TransferFunds(1111111111111111, 888888, 1234567890123457, 999999 , 1000); // Transfer  £1000 to 999999
            var val = _exampleBank.GetBalance(1234567890123457, 999999);
            var val3 = _exampleBank.GetBalance(1111111111111111, 888888);

            Assert.AreEqual(val3, 500.11999999999989);

            Assert.AreEqual(BankPaymentStatus.Successfull, success.PaymentStatus);
            Assert.AreEqual(val, 2000);

            //?Try the same thing again, there shouldn't be enough now.
            var success2 = _exampleBank.TransferFunds(1111111111111111, 888888, 1234567890123457, 999999, 1000); // Transfer  £1000 to 999999
            var val2 = _exampleBank.GetBalance(1234567890123457, 999999);

            Assert.AreEqual(BankPaymentStatus.Failed, success2.PaymentStatus);
            Assert.AreEqual(val2, 2000);
        }
    }
}