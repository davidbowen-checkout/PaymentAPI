using Payment.Gateway.Api.Database;
using Payment.Gateway.Api.DTO;
using Payment.Gateway.Api.Interface;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.Service
{
    public class PaymentsDatabaseRepository : IRepository<PaymentData>
    {
        private readonly PaymentsContext _context;
        private readonly IPaymentUniqueIDBuilder<PaymentData> _uniqueIdBuilder;

        public PaymentsDatabaseRepository(PaymentsContext context, IPaymentUniqueIDBuilder<PaymentData> uniqueIdBuilder)
        {
            _context = context;
            _uniqueIdBuilder = uniqueIdBuilder;

        }

        /// <summary>
        /// Adds a payment to the database in a "InProgress" state.
        /// </summary>
        /// <param name="payment"></param>
        public void Add(PaymentData payment)
        {
            payment.TransactionDate = DateTime.Now; // Set it with the current date / time.
            var paymentToAdd = _uniqueIdBuilder.HydratePaymentId(payment); //Create a uniqueID

            paymentToAdd.Status = PaymentStatus.InProgresss;

            if (_context.PaymentData.Where(p => p.PaymentId == paymentToAdd.PaymentId).Count() == 0) // Prevent duplicate payments being added to the repository. This is dependent on the hashing mechanism making uniqueId's.            
            {
                _context.Add(paymentToAdd);
                _context.SaveChanges();
            }            

        }

        /// <summary>
        /// Returns the payment with the given payment ID provided.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public PaymentData Get(string paymentId)
        {
            return _context.PaymentData.Where(p => p.PaymentId == paymentId).FirstOrDefault();
        }


      
        public PaymentData GetFirstOf( Func<PaymentData, bool> query)
        {
            return _context.PaymentData.Where(query).FirstOrDefault();
        }


        /// <summary>
        /// Removes the given record from the database.
        /// </summary>
        /// <param name="value"></param>
        public void Remove(PaymentData value)
        {            
            _context.Remove(value);
            _context.SaveChanges();
        }


        /// <summary>
        /// Updates the given record.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public PaymentData Update(PaymentData value)
        {
                _context.Update(value);
                _context.SaveChanges();
                return Get(value.PaymentId);

        }
    }
}
