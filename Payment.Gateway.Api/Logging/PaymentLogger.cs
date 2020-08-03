using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.Logging
{
    public class PaymentLogger<PaymentController> : ILogger<PaymentController>
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
           
            var message = logLevel switch
            {
                LogLevel.Error => $"Log in {logLevel} {exception} {state}",
                _ =>  $"Log in {logLevel} {exception} {state}"
            };

            Debug.WriteLine(message);
           

        }
    }
}
