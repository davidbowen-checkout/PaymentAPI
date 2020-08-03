using Microsoft.EntityFrameworkCore;
using Payment.Gateway.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.Interface
{
    public interface IPaymentsContextInterface 
    {
        public DbSet<PaymentData> PaymentData { get; set; }

    }
}
