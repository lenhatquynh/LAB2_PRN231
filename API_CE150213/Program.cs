using API_CE150213.Repository.CustomerRepository;
using Data_CE150213.Models;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);

// Add db context
builder.Services.AddDbContext<AppDatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
);

// Add services to the container.

static IEdmModel GetEdmModel()
{
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Customer>("Customers");
    return builder.GetEdmModel();
}

builder.Services.AddControllers().AddOData(option =>
{
    option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100)
        .AddRouteComponents("odata", GetEdmModel());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<AppDatabaseContext>();
        var created = context.Database.EnsureCreated();
        if (created) DatabaseInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseODataBatching();

app.UseHttpsRedirection();

// Test middleware  
app.Use((context, next) =>
{
    var endpoint = context.GetEndpoint();
    if (endpoint == null)
    {
        return next(context);
    }

    IEnumerable<string> templates;
    IODataRoutingMetadata metadata = endpoint.Metadata.GetMetadata<IODataRoutingMetadata>();
    if (metadata != null)
    {
        templates = metadata.Template.GetTemplates();
    }

    return next(context);
});

app.UseAuthorization();

app.MapControllers();

app.Run();
