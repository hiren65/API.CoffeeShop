using System.Text.Json.Serialization;
using CoffeeShop.API.Interfaces;
using CoffeeShop.API.Models;
using CoffeeShop.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


namespace CoffeeShop.API
{
    public class Program
    {
        
        public static void Main(string[] args)
        {
           
            var builder = WebApplication.CreateBuilder(args);
            var conStr = builder.Configuration.GetSection("ConnectionStrings").GetSection("DBConnect");
            builder.Services.AddDbContext<CoffeeContext>(options => options.UseSqlServer(conStr.Value));
            // Add services to the container.
            builder.Services.AddScoped<IMakeCoffee, CoffeeMachine>();                


            builder.Services.AddControllers();
           

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

           builder.Services.AddSwaggerGen(c =>
            {

               // c.SwaggerDoc("v1", new OpenApiInfo { Title = "Coffee Shop", Version = "v1" });
                c.UseInlineDefinitionsForEnums();

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Coffee Shop",
                        Version = "v1",
                        Description = "A Coffee Shop API to demo",
                        TermsOfService = new Uri("http://tempuri.org/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Hirenkumar Developer",
                            Email = "hirenkumar@com.au"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Apache 2.0",
                            Url = new Uri("http://www.apache.org/licenses/LICENSE-2.0.html")
                        }
                    }
                );

            });
            /*
             * To display the enums as strings in swagger, you configure the JsonStringEnumConverter, adding the following line in ConfigureServices :
             */
            builder.Services.AddControllers().AddJsonOptions(options =>
               options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.Configure<ApiBehaviorOptions>(options =>
           {
               options.SuppressModelStateInvalidFilter = true;
           });
            /*var conStr = builder.Configuration.GetSection("ConnectionStrings").GetSection("DBConnect");
            builder.Services.AddDbContext<CoffeeContext>(options => options.UseSqlServer(conStr.Value));*/

            // builder.Services.AddSwaggerGen();

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

            app.Run();
        }
    }
}