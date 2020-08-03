using Payment.Gateway.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.Interface
{
    public interface IPaymentService<T>
    {
        T RequestPayment(T payment);

        T GetPaymentDetails(string uniqueId);

        T GetFirstOf(Func<T, bool> query);


        T UpdatePayment(T data);


    }
}
