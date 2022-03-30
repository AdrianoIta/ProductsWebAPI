using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using ProductsWebAPI.Business;
using ProductsWebAPI.Validators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(cors =>
{
    cors.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
builder.Services.AddScoped<IProductsBusiness, ProductsBusiness>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Products Web API",
        Description = "This API is responsible to present, create, delete and update the information related to the products.",
        Contact = new OpenApiContact
        {
            Name = "Adriano Santos",
            Url = new Uri("https://github.com/AdrianoIta")
        },
        License = new OpenApiLicense
        {
            Name = "Open Source",
            Url = new Uri("https://example.com/license")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
