using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using DumAPI.Persistence.Services;
using DumAPI.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.OAuth;



var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddIdentityServer()
        .AddInMemoryClients(Config.GetClients())
        .AddInMemoryApiScopes(Config.GetApiScopes())
        .AddInMemoryApiResources(Config.GetApiResources())
        .AddTestUsers(Config.GetTestUsers())
        .AddDeveloperSigningCredential();

builder.Services.AddDbContext<DummyDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<RightService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpLogging(opts =>
{
    opts.LoggingFields = HttpLoggingFields.ResponseBody;
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OAuthDefaults.DisplayName;
}).AddOAuth("GitHub", options =>
{
    options.ClientId = builder.Configuration["GitHub:ClientId"];
    options.ClientSecret = builder.Configuration["GitHub:ClientSecret"];
    options.CallbackPath = new Microsoft.AspNetCore.Http.PathString("/signin-github");

    options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
    options.TokenEndpoint = "https://github.com/login/oauth/access_token";
    options.UserInformationEndpoint = "https://api.github.com/user";

    options.Scope.Add("read:user");

    options.Events = new OAuthEvents
    {
        OnCreatingTicket = async context =>
        { 
            // Retrieve user information from GitHub
            var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", context.AccessToken);
            var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
            response.EnsureSuccessStatusCode();

            var user = System.Text.Json.JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var userId = user.RootElement.GetProperty("id").GetString();
            context.Identity.AddClaim(new System.Security.Claims.Claim("github_id", userId));

            // Add more claims if needed (e.g., username, email)
            var username = user.RootElement.GetProperty("login").GetString();
            context.Identity.AddClaim(new System.Security.Claims.Claim("github_username", username));
        }
    };
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseHttpLogging();
app.UseAuthentication(); 
app.UseAuthorization();

app.Run();