using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Payment.Gateway.Api.DTO;
using Payment.Gateway.Api.Interface;

namespace Payment.Gateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class PaymentController : ControllerBase
    {

        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService<PaymentData> _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService<PaymentData> paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }


        [HttpGet]
        public MaskedPaymentData Get(String paymentId)
        {

            try
            {
                var val = _paymentService.GetPaymentDetails(paymentId);
                if (val == null) return null;
                return new MaskedPaymentData(val);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting paymentId", ex);
            }
            return null;
        }


        /// <summary>
        /// Returns if the payment is successful or not.
        /// </summary>
        /// <param name="payment"></param>
        /// <returns></returns>
        [HttpPost]

        public ActionResult<MaskedPaymentData> Post(PaymentData payment)
        {

            try
            {
                var requestedPayment = _paymentService.RequestPayment(payment);
                return new MaskedPaymentData(requestedPayment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error posting new payment", ex);
             }
            return null;
        }
    }
}
