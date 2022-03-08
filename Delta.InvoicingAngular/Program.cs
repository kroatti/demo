using System.Text.Json;
using Delta.Invoicing.Core.Extensions;
using Delta.InvoicingAngular;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews(c => c.Filters.Add(typeof(ExceptionFilter)))
    .AddJsonOptions(o => o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase);

builder.Services
    .AddApplication()
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(c =>
    {
        c.CustomSchemaIds(type => type.GetNameConcat());
        c.SwaggerDoc("v1", new OpenApiInfo {Title = "Delta.Invoicing", Version = "v1"});
    });


var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseSwagger();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors(e => e.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseRouting();
app.UseSwaggerUI();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");
;

app.Run();