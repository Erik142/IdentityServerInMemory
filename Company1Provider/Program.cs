using IdentityModel;
using IdentityServer4;
using IdentityServerCommon;
using IdentityServerCommon.Model;
using IdentityServerCommon.Model.AuthenticationProviders;
using System.Security.Claims;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var address = new
{
    street_address = "One Hacker Way",
    locality = "Heidelberg",
    postal_code = 69118,
    country = "Germany"
};

AuthProviderBase<UserPasswordAuthenticationProvider, UserAuthenticationModel, User> authProviderBase = new AuthProviderBase<UserPasswordAuthenticationProvider, UserAuthenticationModel, User>(new UserPasswordAuthenticationProvider(
                new AuthProviderModel()
                {
                    Name = "Company 1",
                    CallbackUrl = "https://localhost:7032",
                    AuthenticationMechanisms = new HashSet<AuthenticationMechanism>() { AuthenticationMechanism.UserPassword }
                }, new InMemoryUserStore(new HashSet<User>()
                {
                    new User
                    {
                        SubjectId = "12345",
                        Username = "alice",
                        Password = "alice",
                        Company = "Company 1",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    },
                    new User
                    {
                        SubjectId = "88421113",
                        Username = "bob",
                        Password = "bob",
                        Company = "Company 1",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    }
                }, System.Security.Authentication.HashAlgorithmType.None)), "https://localhost:5001");



builder.Services.AddSingleton<AuthProviderBase<UserPasswordAuthenticationProvider, UserAuthenticationModel, User>>(authProviderBase);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

Task.Run(async () =>
{
    while (!await authProviderBase.RegisterAsync())
    {
        Thread.Sleep(1000);
    }
});

app.Run();