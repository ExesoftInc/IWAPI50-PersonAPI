using Microsoft.Extensions.DependencyInjection;

namespace PersonAPI.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IAddressBuilder, AddressBuilder>();
            services.AddScoped<IAddressTypeBuilder, AddressTypeBuilder>();
            services.AddScoped<IBusinessEntityBuilder, BusinessEntityBuilder>();
            services.AddScoped<IBusinessEntityAddressBuilder, BusinessEntityAddressBuilder>();
            services.AddScoped<IBusinessEntityContactBuilder, BusinessEntityContactBuilder>();
            services.AddScoped<IContactTypeBuilder, ContactTypeBuilder>();
            services.AddScoped<ICountryRegionBuilder, CountryRegionBuilder>();
            services.AddScoped<IEmailAddressBuilder, EmailAddressBuilder>();
            services.AddScoped<IPasswordBuilder, PasswordBuilder>();
            services.AddScoped<IPersonBuilder, PersonBuilder>();
            services.AddScoped<IPersonPhoneBuilder, PersonPhoneBuilder>();
            services.AddScoped<IPhoneNumberTypeBuilder, PhoneNumberTypeBuilder>();
            services.AddScoped<IStateProvinceBuilder, StateProvinceBuilder>();
        }
    }
}
