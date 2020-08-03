using Payment.Gateway.Api.ExtensionMethods;
using Payment.Gateway.Api.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.DTO
{
    public class MaskedPaymentData
    {

        public MaskedPaymentData(PaymentData payment)
        {
            this.PaymentId = payment.PaymentId;
            this.BankAccountNumber = payment.BankAccountNumber.ToString().MaskString();
            this.SortCode = payment.SortCode.ToString().MaskString();
            this.CcvNumber = payment.CcvNumber.ToString().MaskString();
            this.Status = payment.Status;

        }

        public string PaymentId { get; private set; }

        [Required]
        public string BankAccountNumber { get; private set; }

        [Required]
        public string SortCode { get; private set; }

        [Required]
        public string CcvNumber { get; private  set; }

        public PaymentStatus Status { get; private set; }

    }
}
