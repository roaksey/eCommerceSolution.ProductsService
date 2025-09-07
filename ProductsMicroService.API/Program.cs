using BusinessLogicLayer;
using FluentValidation.AspNetCore;
using ProductsMicroService.API.APIEndpoints;
using ProductsMicroService.API.Middleware;
using DataAccessLayer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ADD DAL AND BLL services
builder.Services.AddDataAccessLayer(builder.Configuration); // Ensure AddDataAccessLayer is implemented
builder.Services.AddBusinessLogicLayer(); // Ensure AddBusinessLogicLayer is implemented

builder.Services.AddControllers();

// FluentValidations
builder.Services.AddFluentValidationAutoValidation();

//Add model binder to read values from JSON to Enum
builder.Services.ConfigureHttpJsonOptions(options => { 
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


var app = builder.Build();
app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapProductAPIEndpoints(); 
app.Run();
