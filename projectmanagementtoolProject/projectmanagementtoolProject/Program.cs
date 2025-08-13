using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using projectmanagementtoolProject.Context;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProjectDBContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("dBConstr") ));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




app.UseStaticFiles();
app.UseRouting();

app.MapControllers();

app.UseCors(c=>c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());    

app.Run();
