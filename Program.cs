using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Newtonsoft.Json.Converters;
using Setoran_API.NET.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllersWithViews()
        .AddNewtonsoftJson(options =>
        {
            // Configure Newtonsoft.Json options here
            options.SerializerSettings.Converters.Add(new StringEnumConverter()); 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

builder.Services.AddAuthentication()
    // .AddCookie(IdentityConstants.ApplicationScheme)
    .AddBearerToken(IdentityConstants.BearerScheme, option => {
        option.BearerTokenExpiration = TimeSpan.FromDays(600);
    });

builder.Services.AddIdentityCore<Pengguna>(options => 
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<Database>()
    .AddApiEndpoints();

builder.Services.ConfigureApplicationCookie(options =>
{
    // options.Cookie.Name = "auth_cookie";
    options.Cookie.SameSite = SameSiteMode.None;
    // options.LoginPath = new PathString("/api/contests");
    // options.AccessDeniedPath = new PathString("/api/contests");

    // Not creating a new object since ASP.NET Identity has created
    // one already and hooked to the OnValidatePrincipal event.
    // See https://github.com/aspnet/AspNetCore/blob/5a64688d8e192cacffda9440e8725c1ed41a30cf/src/Identity/src/Identity/IdentityServiceCollectionExtensions.cs#L56
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    };
});

builder.Services.AddDbContext<Database>(options => options.UseSqlite($"Data Source=./database.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.MapIdentityApi<Pengguna>();

app.Run();
