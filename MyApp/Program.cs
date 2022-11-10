using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using MyDB;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddCors(o => o.AddPolicy("LowCorsPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddControllers();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<MyDBContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
}
else
{
    builder.Services.AddDbContext<MyDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<MyDBContext>();
    dataContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("LowCorsPolicy");
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();
app.Run();
