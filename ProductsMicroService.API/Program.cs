using BusinessLogicLayer;
using FluentValidation.AspNetCore;
using ProductsMicroService.API.APIEndpoints;
using ProductsMicroService.API.Middleware;
using DataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// ADD DAL AND BLL services
builder.Services.AddDataAccessLayer(builder.Configuration); // Ensure AddDataAccessLayer is implemented
builder.Services.AddBusinessLogicLayer(); // Ensure AddBusinessLogicLayer is implemented

builder.Services.AddControllers();

// FluentValidations
builder.Services.AddFluentValidationAutoValidation();


var app = builder.Build();
app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapProductAPIEndpoints(); 
app.Run();
