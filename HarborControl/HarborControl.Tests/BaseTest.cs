using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace HarborControl.Tests
{
    public abstract class BaseTest
    {
        protected readonly ServiceCollection services;

        public BaseTest()
        {
            services = new ServiceCollection();
        }

        protected T1 Resolve<T1, T2>()
                where T1 : class 
                where T2 : class, T1 
        {
            services.AddTransient<T1, T2>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<T1>();
        }

        protected static bool StringEquals(string a, string b)
            => string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
    }
}