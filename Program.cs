using EntityFrameworkCore.SqlServer.JsonExtention;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using web_store_server.Domain.Contracts;
using web_store_server.Domain.Services;
using web_store_server.Persistence.Database;
using web_store_server.Shared.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts =>
{
    opts.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    opts.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    var fileName = Assembly.GetExecutingAssembly().GetName().Name + ".xml";
    opts.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));
});

/*
 *  Services
 */
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(typeof(Program).Assembly));
builder.Services.AddScoped<RequestExceptionMiddleware>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IAccountService, AccountService>();

/*
 * Database
 */
builder.Services.AddDbContext<StoreContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseJsonFunctions());

/*
 * JWT
 */
var key = builder.Configuration.GetValue<string>("JWTSettings:Key");
var keyBytes = Encoding.ASCII.GetBytes(key);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidIssuer = builder.Configuration.GetValue<string>("JWTSettings:Issuer"),
        ValidAudience = builder.Configuration.GetValue<string>("JWTSettings:Audience"),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

/*
 * Routing
 */
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
 * Middlewares
 */
app.UseMiddleware<RequestExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
