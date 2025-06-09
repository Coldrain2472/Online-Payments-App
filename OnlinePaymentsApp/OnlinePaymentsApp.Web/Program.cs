using OnlinePaymentsApp.Repository;
using OnlinePaymentsApp.Repository.Implementations.Account;
using OnlinePaymentsApp.Repository.Implementations.Payment;
using OnlinePaymentsApp.Repository.Implementations.User;
using OnlinePaymentsApp.Repository.Implementations.UserAccount;
using OnlinePaymentsApp.Repository.Interfaces.Account;
using OnlinePaymentsApp.Repository.Interfaces.Payment;
using OnlinePaymentsApp.Repository.Interfaces.User;
using OnlinePaymentsApp.Repository.Interfaces.UserAccount;
using OnlinePaymentsApp.Services.Implementations.Account;
using OnlinePaymentsApp.Services.Implementations.Authentication;
using OnlinePaymentsApp.Services.Implementations.Payment;
using OnlinePaymentsApp.Services.Implementations.User;
using OnlinePaymentsApp.Services.Implementations.UserAccount;
using OnlinePaymentsApp.Services.Interfaces.Account;
using OnlinePaymentsApp.Services.Interfaces.Authentication;
using OnlinePaymentsApp.Services.Interfaces.Payment;
using OnlinePaymentsApp.Services.Interfaces.User;
using OnlinePaymentsApp.Services.Interfaces.UserAccount;

namespace OnlinePaymentsApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // services
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IUserAccountService, UserAccountService>();

            // repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();

            // connecti9on to the db
            ConnectionFactory.Initialize(builder.Configuration.GetConnectionString("DefaultConnection"));

            // session properties
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
