using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ______________.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoginServiceFactory _loginServiceFactory;

        public HomeController(LoginServiceFactory loginServiceFactory)
        {
            _loginServiceFactory = loginServiceFactory;
        }

        public IActionResult Index(string loginMethod)
        {
            var loginService = _loginServiceFactory.GetLoginService(loginMethod);

            // Use the appropriate login service based on the key
            loginService.Login();

            return Ok();

        }
    }
    public interface ILoginService
    {
        void Login();
    }

    public class UsernamePasswordLoginService : ILoginService
    {
        public void Login()
        {
            Console.WriteLine("Username/password login");
        }
    }

    public class SocialMediaLoginService : ILoginService
    {
        public void Login()
        {
            Console.WriteLine("Social media login");
        }
    }

    public class LoginServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public LoginServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ILoginService GetLoginService(string key)
        {
            return key switch
            {
                "UsernamePassword" => _serviceProvider?.GetRequiredService<UsernamePasswordLoginService>(),
                "SocialMedia" => _serviceProvider?.GetRequiredService<SocialMediaLoginService>(),
                _ => throw new ArgumentException($"Invalid key: {key}")
            };
        }
    }

}
