using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Core.Api
{
    public class BankResponse
    {

        public string UniqueIdentifier { get; set; }
        public BankPaymentStatus PaymentStatus { get; set; }
    }



    public enum BankPaymentStatus
    {
        Successfull,
        Failed
    }
}
