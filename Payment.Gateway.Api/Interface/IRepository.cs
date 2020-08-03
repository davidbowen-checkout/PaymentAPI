using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.Gateway.Api.Interface
{
    public interface IRepository<T>
    {
        void Add(T value);

        void Remove(T value);


        T Get(string PaymentId);


        public T Update(T value);


        public T GetFirstOf(Func<T, bool> query);


    }
}
