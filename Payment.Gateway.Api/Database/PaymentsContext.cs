using Microsoft.EntityFrameworkCore;
using Payment.Gateway.Api.DTO;
using Payment.Gateway.Api.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Payment.Gateway.Api.Database
{
    public class PaymentsContext : DbContext, IPaymentsContextInterface
    {
        public DbSet<PaymentData> PaymentData { get; set; }


        bool _useInMemory = false;

        public PaymentsContext()
        {       }


        public PaymentsContext(bool useInMemory)
        {
            _useInMemory = useInMemory;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!_useInMemory)
                options.UseSqlite("Data Source=payments.db");
            else
                options.UseInMemoryDatabase(databaseName: "Payments");


        }

       
    }
}
