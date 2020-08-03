using Payment.Gateway.Api.DTO;
using Payment.Gateway.Api.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.Service
{
    public class PaymentMd5UniqueId : IPaymentUniqueIDBuilder<PaymentData>
    {
        /// <summary>
        /// Populates the paymentId of a given paymentData object.
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public PaymentData HydratePaymentId(PaymentData payment)            
        {
            payment.PaymentId = generateMd5(payment.TransactionDate.ToString("MM/dd/yyyy hh:mm") + payment.SortCode + payment.BankAccountNumber); //Removing seconds
            return payment;
        }

        
        /// <summary>
        /// Generates an MD5 Hash from a given string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string generateMd5(string value)
        {
            var md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(value);
            byte[] hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();


        }
    }
}
