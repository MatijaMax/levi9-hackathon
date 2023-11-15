using CsvHelper;
using FibaApi;
using FibaCore;
using FibaCore.Enums;
using FibaInfrastructure;
using FibaInfrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Configuration
builder.Configuration.AddJsonFile("appsettings.json");
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<FibaContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString("FibaConnection"));
});

// Configure MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

// Initialize repositories
builder.Services.AddTransient<IRepository<Player>, GenericRepository<Player>>();

//Schema name generation
builder.Services.AddOpenApiDocument(cfg =>
{
    cfg.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator();
});

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var dbContext = serviceProvider.GetRequiredService<FibaContext>();

    //ImportCsvData(dbContext, "C:\\Users\\Matija\\Desktop\\project-fiba");
    ImportCsvData(dbContext, Directory.GetCurrentDirectory());
}


// Configure OpenApi/Swagger
app.UseOpenApi(); // serve OpenAPI/Swagger documents
app.UseSwaggerUi3(); // serve Swagger  

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

static void ImportCsvData(FibaContext dbContext, string folderPath)
{
    dbContext.Database.ExecuteSqlRaw("DELETE FROM public.\"Players\"");
    var csvFiles = Directory.EnumerateFiles(folderPath, "*.csv");

    foreach (var csvFile in csvFiles)
    {
        var records = ReadCsv<PlayerCSV>(csvFile, skipHeader: true);

        var players = records.Select(csvModel => new Player
        {
            Id = Guid.NewGuid(),
            Name = csvModel.Name,
            Position = (Position)Enum.Parse(typeof(Position), csvModel.Position),
            FTM = csvModel.FTM,
            FTA = csvModel.FTA,
            TwoPM = csvModel.TwoPM,
            TwoPA = csvModel.TwoPA,
            ThreePM = csvModel.ThreePM,
            ThreePA = csvModel.ThreePA,
            REB = csvModel.REB,
            BLK = csvModel.BLK,
            AST = csvModel.AST,
            STL = csvModel.STL,
            TOV = csvModel.TOV,
        }).ToList();

        dbContext.Players.AddRange(players);
        dbContext.SaveChanges();

    }
}



static List<T> ReadCsv<T>(string filePath, bool skipHeader = false)
{
    using (var reader = new StreamReader(filePath, Encoding.Unicode))
    using (var csv = new CsvReader(reader, new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)))
    {
        // Skip the header if specified
        if (skipHeader)
        {
            csv.Read();
            csv.ReadHeader();
        }
        csv.Context.RegisterClassMap<PlayerCsvMap>();
        return csv.GetRecords<T>().ToList();
    }
}