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

    public class TasksController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService<PaymentData> _paymentService;

        public TasksController(ILogger<PaymentController> logger, IPaymentService<PaymentData> paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }    



        [HttpGet]
        public PaymentData Get()
        {
            try
            {
                return _paymentService.GetFirstOf(p => p.Status == PaymentStatus.InProgresss);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error getting payment to process", ex);

            }
            return null;
        }



        [HttpPost]
        public ActionResult<PaymentData> Post(PaymentData payment)
        {
            try
            {
                return _paymentService.UpdatePayment(payment);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error posting task update to task controller", ex);
            }
            return null;

        }
    }
}
