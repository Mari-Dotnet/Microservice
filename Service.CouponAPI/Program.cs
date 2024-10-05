
using Microsoft.EntityFrameworkCore;
using Service.CouponAPI.Data;
using Service.CouponAPI.Middleware;
using Service.CouponAPI.Mapper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    //Data Source=MARI-LAPTOP\\SQLEXPRESS2019;Initial Catalog=Coupon;User ID=mariapi;Password=apimari"
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));   
});

builder.Services.AddAutoMapper(typeof(MappingConfig).Assembly);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
applymigration();
app.Run();

void applymigration()
{
    using(var scope = app.Services.CreateScope())
    {
        var _db=scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if(_db.Database.GetPendingMigrations().Count() > 0)
        {
            _db.Database.Migrate();
        }

    }
}