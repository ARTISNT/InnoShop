
using UsersManagement.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddCustomEmailServices(builder.Configuration);
builder.Services.AddCustomApplicationServices();
builder.Services.AddApiConfiguration();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
