using AzureB2C.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using Azure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.Authentication.Methods.Item.ResetPassword;

namespace AzureB2C.Client.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult ResetPassword()
        {
            return Redirect("/");
        }
        public async Task<IActionResult> CreateUser()
        {
            try
            {
                ClientSecretCredential credential =
                    new ClientSecretCredential("c56e7a1c-e809-48a7-8b6b-d252245ba152",
                        "cb86c1d0-996d-4800-81f7-7b9ecae78677", "yk_8Q~bYopWsUoZFK3g.jt5cvtOHSY5WIflg1be2");

                var graphClient =
                    new GraphServiceClient(credential, new List<string>() { "https://graph.microsoft.com/.default" });
                //var requestBody = new User
                //{
                //    AccountEnabled = true,
                //    DisplayName = "Ashis Barai",
                //    MailNickname = "AshisB",
                //    GivenName = "Ashis",
                //    Surname = "Barai",
                //    UserPrincipalName = "ashisbarai@sideriantest.onmicrosoft.com",
                //    PasswordProfile = new PasswordProfile
                //    {
                //        //ForceChangePasswordNextSignIn = true,
                //        Password = "AB@123456",
                //    },
                //};


                var requestBody = new User
                {
                    DisplayName = "Ashis Barai1",
                    Identities = new List<ObjectIdentity>
                    {
                        new ObjectIdentity
                        {
                            SignInType = "emailAddress",
                            Issuer = "sideriantest.onmicrosoft.com",
                            IssuerAssignedId = "ashis084002+1@gmail.com",
                        },
                    },
                    PasswordProfile = new PasswordProfile
                    {
                        Password = "AB@123456",
                        ForceChangePasswordNextSignIn = false,
                    },
                    PasswordPolicies = "DisablePasswordExpiration",
                    AdditionalData = new Dictionary<string, object>{ { "extension_a535844ed4db4c07b061b614b47fa20a_AppScope", "siderian-test" } }
                };

                var result = await graphClient.Users.PostAsync(requestBody);



                //var requestBody2 = new ResetPasswordPostRequestBody
                //{
                //    NewPassword = "AB@123456",
                //};

                //await graphClient.Users["e54db6c1-32b3-4993-865f-2a859f90af4c"].PatchAsync(new User
                //{
                //    PasswordProfile = new PasswordProfile
                //    {
                //        Password = "AB@123456",
                //        ForceChangePasswordNextSignIn = true
                //    }
                //});

                //var result = await graphClient.Users["e54db6c1-32b3-4993-865f-2a859f90af4c"].Authentication
                //.Methods.GetAsync();
                //.Methods["28c10230-6103-485e-b985-444c60001490"].ResetPassword.PostAsync(requestBody2);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Redirect("Index");
        }
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7197/WeatherForecast");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await httpClient.SendAsync(request);

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
