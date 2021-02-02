using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Domain.Identity;

namespace WebStore.Clients.Identity
{
    public static class IdentityExstensios
    {
        public static IServiceCollection AddIdentityWebStoreClients(this IServiceCollection services)
        {
            services.AddTransient<IUserStore<User>, UserClient>()
                .AddTransient<IUserRoleStore<User>, UserClient>()
                .AddTransient<IUserPasswordStore<User>, UserClient>()
                .AddTransient<IUserEmailStore<User>, UserClient>()
                .AddTransient<IUserPhoneNumberStore<User>, UserClient>()
                .AddTransient<IUserTwoFactorStore<User>, UserClient>()
                .AddTransient<IUserLoginStore<User>, UserClient>();

            services.AddTransient<IRoleStore<Role>, RolesClient>();

            return services;
        }

        public static IdentityBuilder AddIdentityWebStoreWebAPIClients(this IdentityBuilder builder)
        {
            builder.Services.AddIdentityWebStoreClients();

            return builder;
        }
    }
}
