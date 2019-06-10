using IdentityModel;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopOnContainers.Services.Identity.API.Models.UserViewModel;
using IdentityServer4.Stores;
using Microsoft.eShopOnContainers.Services.Identity.API.Services;
using Microsoft.eShopOnContainers.Services.Identity.API.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using IdentityServer4;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Microsoft.eShopOnContainers.Services.Identity.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoginService<ApplicationUser> _loginService;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IdentityServerTools _identityServerTools;

        public UserController(
            //InMemoryUserLoginService loginService,
            ILoginService<ApplicationUser> loginService,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            ILogger<AccountController> logger,
            UserManager<ApplicationUser> userManager,
            IdentityServerTools identityServerTools
        )
        {
            _loginService = loginService;
            _interaction = interaction;
            _clientStore = clientStore;
            _logger = logger;
            _userManager = userManager;
            _identityServerTools = identityServerTools;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<LoginRensponse>> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var user = await _loginService.FindByUsername(login.username);

                if (await _loginService.ValidateCredentials(user, login.password))
                {
                    
                    var claims = GetClaimsFromUser(user);
        
                    var token = await _identityServerTools.IssueJwtAsync(3600, claims);

                    return new LoginRensponse()
                    {
                        code = 200,
                        result = token,
                        meta = new Meta()
                        {
                            refreshTocken = ""
                        }
                    };
                }
            }

            return new LoginRensponse()
            {
                code = 500,
                result = "",
                meta = new Meta()
                {
                    refreshTocken = ""
                }
            };
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<NewUserResponse>> Create(NewUser model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.customer.email,
                    Email = model.customer.email,
                    LastName = model.customer.lastname,
                    Name = model.customer.firstname,
                    CardHolderName = string.Empty,
                    CardNumber = string.Empty,
                    CardType = 1,
                    City = string.Empty,
                    Country = string.Empty,
                    Expiration = "12/50",
                    Street = string.Empty,
                    State = string.Empty,
                    ZipCode = string.Empty,
                    PhoneNumber = string.Empty,
                    SecurityNumber = string.Empty
                };

                var result = await _userManager.CreateAsync(user, model.password);

                if (result.Succeeded)
                {
                    var response = new NewUserResponse()
                    {
                        code = 200,
                        result = new User()
                        {
                            Email = model.customer.email,
                            Lastname = model.customer.lastname,
                            Firstname = model.customer.firstname
                        }
                    };

                    return response;
                }
                else
                {
                    return new NewUserResponse()
                    {
                        code = 500
                    };
                }
            }

            return new NewUserResponse()
            {
                code = 500
            };
        }

        private IEnumerable<Claim> GetClaimsFromUser(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.ClientId, "js"),
                new Claim(JwtClaimTypes.Audience, "orders"),
                new Claim(JwtClaimTypes.Audience, "basket"),
                new Claim(JwtClaimTypes.IssuedAt, DateTime.UtcNow.ToEpochTime().ToString(), ClaimValueTypes.Integer),
                new Claim(JwtClaimTypes.JwtId, CryptoRandom.CreateUniqueId(16)),
                new Claim(JwtClaimTypes.Scope, IdentityServerConstants.StandardScopes.Profile),

                new Claim(JwtClaimTypes.Subject, user.Id),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            if (!string.IsNullOrWhiteSpace(user.Name))
                claims.Add(new Claim("name", user.Name));

            if (!string.IsNullOrWhiteSpace(user.LastName))
                claims.Add(new Claim("last_name", user.LastName));

            if (!string.IsNullOrWhiteSpace(user.CardNumber))
                claims.Add(new Claim("card_number", user.CardNumber));

            if (!string.IsNullOrWhiteSpace(user.CardHolderName))
                claims.Add(new Claim("card_holder", user.CardHolderName));

            if (!string.IsNullOrWhiteSpace(user.SecurityNumber))
                claims.Add(new Claim("card_security_number", user.SecurityNumber));

            if (!string.IsNullOrWhiteSpace(user.Expiration))
                claims.Add(new Claim("card_expiration", user.Expiration));

            if (!string.IsNullOrWhiteSpace(user.City))
                claims.Add(new Claim("address_city", user.City));

            if (!string.IsNullOrWhiteSpace(user.Country))
                claims.Add(new Claim("address_country", user.Country));

            if (!string.IsNullOrWhiteSpace(user.State))
                claims.Add(new Claim("address_state", user.State));

            if (!string.IsNullOrWhiteSpace(user.Street))
                claims.Add(new Claim("address_street", user.Street));

            if (!string.IsNullOrWhiteSpace(user.ZipCode))
                claims.Add(new Claim("address_zip_code", user.ZipCode));

            if (_userManager.SupportsUserEmail)
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }

            if (_userManager.SupportsUserPhoneNumber && !string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber),
                    new Claim(JwtClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }

            return claims;
        }
    }
}