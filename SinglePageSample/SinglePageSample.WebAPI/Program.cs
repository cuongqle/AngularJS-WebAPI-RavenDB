using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using SinglePageSample.Db.DbStore;
using SinglePageSample.Db.RavenStore;
using SinglePageSample.Repository;
using SinglePageSample.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var ravenUrl = builder.Configuration["RavenDB:Url"] ?? "http://localhost:8080";
var database = builder.Configuration["RavenDB:Database"] ?? "Sample";

var store = new DocumentStore
{
    Urls = new[] { ravenUrl },
    Database = database
}.Initialize();

IndexCreation.CreateIndexes(typeof(CompanyRepository).Assembly, store);

builder.Services.AddSingleton<IDocumentStore>(store);
builder.Services.AddScoped<IDbStore, RavenDbStore>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

var app = builder.Build();

app.UseCors();
app.MapControllers();

app.Run();
