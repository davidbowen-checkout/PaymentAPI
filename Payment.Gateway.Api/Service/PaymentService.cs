using Bank.Core.Api;
using Payment.Gateway.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RabbitMQ.Client;
using System.Text;
using Payment.Gateway.Api.Interface;

namespace Payment.Gateway.Api.Service
{
    public class PaymentService : IPaymentService<PaymentData>
    {
        private readonly IRepository<PaymentData> _paymentRepo;           


    

        /// <summary>
        /// In this class we look to validate and create payments. These can then be shipped to the bank.
        /// </summary>
        /// <param name="bank"></param>
        /// <param name="paymentRepo"></param>
        public PaymentService( IRepository<PaymentData> paymentRepo) 
        {
            _paymentRepo = paymentRepo;
        }

        /// <summary>
        /// Gets the first occurrence of an item that meets the passed in parameter.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PaymentData GetFirstOf(Func<PaymentData, bool> query)
        {
            return _paymentRepo.GetFirstOf(query);
        }

        /// <summary>
        /// Gets the details of a submitted payment.
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public PaymentData GetPaymentDetails(string payment)
        {    
            return _paymentRepo.Get(payment);
        }        

        /// <summary>
        /// Creates a payment request.
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public PaymentData RequestPayment(PaymentData payment)
        {            

            _paymentRepo.Add(payment);
            return _paymentRepo.Get(payment.PaymentId);
        }


        /// <summary>
        /// Updates a payment. This is typically used to update statuses, but could be used for credit card updates etc in the future.
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        public PaymentData UpdatePayment(PaymentData payment)
        {
            return _paymentRepo.Update(payment);
        }
               

    }
}
