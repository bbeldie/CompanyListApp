using Company.Dal;
using Company.Logic.CompanyLogic;
using Company.RestApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace Company.RestApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetCompanyByIdHandler>());

            builder.Services.AddDbContext<CompanyDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalHost", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            AddAuthentication(builder);


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.MapControllers();

            app.UseCors("AllowLocalHost");

            app.Run();
        }

        private static void AddAuthentication(WebApplicationBuilder builder)
        {
            var key = builder.Configuration["Authentication:Key"];
            var issuer = builder.Configuration["Authentication:Issuer"];
            var audience = builder.Configuration["Authentication:Audience"];

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
            {
                throw new InvalidOperationException("The authentication key is not configured properly.");
            }
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                    };
                });
        }
    }
}
