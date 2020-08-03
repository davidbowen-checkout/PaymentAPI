using Payment.Gateway.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.Interface
{
    public interface IPaymentData
    {


        public string PaymentId { get; set; }

        public long? BankAccountNumber { get; set; }

        public int? SortCode { get; set; }

        public int? CcvNumber { get; set; }

        public DateTime TransactionDate { get; set; }
        public PaymentStatus Status { get; set; }

        public double? PaymentValue { get; set; }

        public string CardHolderName { get; set; }

    }
}
