using Auth.Jwt.App.Service;
using Auth.Jwt.Domain.Repositorio;
using Auth.Jwt.Domain.Service;
using Auth.Jwt.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepositorio, UserRepositorio>();
builder.Services.AddScoped<IGerarToken, GeraToken>();

builder.Services.AddCors(options => options.AddDefaultPolicy(
            builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
        );

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth.Jwt.Web", Version = "v1" });
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth.Jwt.Web v1"));
}

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();