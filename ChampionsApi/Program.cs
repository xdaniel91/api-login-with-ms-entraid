using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ChampionsApi",
        Version = "v1"
    });
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Name = "oauth2.0",
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri(builder.Configuration["AzureAdClient:AuthorizationUrl"]),
                TokenUrl = new Uri(builder.Configuration["AzureAdClient:TokenUrl"]),
                Scopes = new Dictionary<string, string>
                {
                    { builder.Configuration["AzureAdClient:ScopeAccess"], "Scope Access"}
                }
            }
        }
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
          new OpenApiSecurityScheme
          {
             Reference = new OpenApiReference{ Type = ReferenceType.SecurityScheme, Id = "oauth2" },
          },
          new[] { builder.Configuration["AzureAdClient:Scope"] }
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.OAuthUsePkce(); // com esse item não precisamos informar o client_secret
        options.OAuthClientId(builder.Configuration["AzureAdClient:ClientId"]); // esse já deixa preenchido o client_id
        options.OAuth2RedirectUrl("https://localhost:7149/swagger/oauth2-redirect.html");
        options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        options.EnablePersistAuthorization();
    });
}
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
