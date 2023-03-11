using Velkhana.MES.PLCService.API.Helpers;
using Velkhana.Shared.WebServer.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.MapAllWebAPIEndpoints();

app.UseHttpsRedirection();

app.Run();
