using Microsoft.EntityFrameworkCore;
using UsersManagement.Application.Abstractions.Repositories;
using UsersManagement.Application.Dto.UserDto;
using UsersManagement.Application.Implementation.Queries;
using UsersManagement.Infrastructure.Db.Context;
using UsersManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UsersManagementDbContext>(dbContextOptions => 
    dbContextOptions.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAllUsersQuery).Assembly));
builder.Services.AddAutoMapper(cfg => { }, typeof(UserDtoResponse).Assembly);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();