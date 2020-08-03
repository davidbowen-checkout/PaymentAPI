using Payment.Gateway.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.Interface
{
    public interface IPaymentUniqueIDBuilder<T>
    {
        public T HydratePaymentId(T val);


    }
}
