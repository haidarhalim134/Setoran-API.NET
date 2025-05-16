using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Setoran_API.NET;
using Setoran_API.NET.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddSingleton<SupabaseService>();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllersWithViews()
        .AddNewtonsoftJson(options =>
        {
            // Configure Newtonsoft.Json options here
            options.SerializerSettings.Converters.Add(new StringEnumConverter()); 
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

builder.Services.AddControllers()
    .AddJsonOptions(options => { 
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); 
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        // options.JsonSerializerOptions.refe
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

builder.Services.AddDbContext<Database>();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.MapIdentityApi<Pengguna>();

app.UseCors(policy => policy.AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithOrigins(["http://localhost:3000", "https://setoran.vercel.app/"]));//.SetIsOriginAllowed(hostName => {Console.WriteLine(hostName);return true;}));

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();


app.Run();
