using Microsoft.AspNetCore.Authentication.JwtBearer;
using MoviesAPI.Filters;
using MoviesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options=>
{
    options.Filters.Add(typeof(MyExceptionFilter));
});
builder.Services.AddControllers();
builder.Services.AddResponseCaching();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddBearerToken();
builder.Services.AddSingleton<IRepository, InMemoryRepository>();
builder.Services.AddTransient<MyActionFilter>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var logger = app.Logger;








// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseResponseCaching();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{

    using (var swapStream = new MemoryStream())
    {
        var originalResponseBody = context.Response.Body;
        context.Response.Body = swapStream;
        await next.Invoke();

        swapStream.Seek(0, SeekOrigin.Begin);
        string responseBody = new StreamReader(swapStream).ReadToEnd();
        swapStream.Seek(0, SeekOrigin.Begin);

        await swapStream.CopyToAsync(originalResponseBody);
        context.Response.Body = originalResponseBody;

        logger.LogInformation(responseBody);
    }
});


app.Map("/map1", (app) =>
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("I m short-circuiting the pipeline");
    });
});

app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
