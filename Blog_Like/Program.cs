using Blog_Like.Repository;
using Microsoft.EntityFrameworkCore;
using MyBlog.Db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("BlogProject");
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBlogRepository, BlogSQLRepositoryImpl>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddDbContext<BlogDbContext>(options => {
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
