using Bussiness.Accounts.Utils.Token;
using Core.CQRS.Decorators.Caching;
using Core.CQRS.Decorators.Transaction;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Bussiness
{
    public static class BusinessServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.Decorate(typeof(IRequestHandler<,>), typeof(TransactionDecorator<,>));
            services.Decorate(typeof(IRequestHandler<,>), typeof(CachingDecorator<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<TokenHelper>();

            services.AddMemoryCache();
        }
    }
}
