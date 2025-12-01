using ProductsManagement.Api.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithFiltersAndValidation();
builder.Services.AddDbContexts(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddAuthServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Middleware
app.UseSwaggerDocumentation();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
