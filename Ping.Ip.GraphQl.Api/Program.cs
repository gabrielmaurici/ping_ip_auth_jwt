using GraphQL.MicrosoftDI;
using GraphQL.Types;
using GraphQL;
using Ping.Ip.Infra.GraphQl.Schema;
using Ping.Ip.Domain.GraphQl.Repositorio;
using Ping.Ip.Infra.GraphQl.Repositorios;
using Ping.Ip.Domain.Domain;
using Ping.Ip.Infra.GraphQl.Queries;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddScoped<IDispositivoGraphQlRepositorio, DispositivoGraphQlRepositorio>();

builder.Services.AddScoped<ISchema, DispositivoSchema>(services => new DispositivoSchema(new SelfActivatingServiceProvider(services)));
builder.Services.AddGraphQL(options =>
                    options.ConfigureExecution((opt, next) =>
                    {
                        opt.EnableMetrics = true;
                        return next(opt);
                    }).AddSystemTextJson());

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Ping Ip GraphQl", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ping.Ip.GraphQl.Api v1"));
    app.UseGraphQLAltair();
}


//app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseGraphQL();

app.Run();
