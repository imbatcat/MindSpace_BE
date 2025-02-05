namespace MindSpace.Infrastructure.Persistence;

using Application.Commons.Utilities.Seeding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

internal class DatabaseCleaner : IDataCleaner
{
    private readonly ApplicationDbContext _dbContext;

    private readonly IEntityOrderProvider _entityOrderProvider;

    private readonly ILogger<DatabaseCleaner> _logger;

    public DatabaseCleaner(
        ApplicationDbContext dbContext,
        ILogger<DatabaseCleaner> logger,
        IEntityOrderProvider entityOrderProvider)
    {
        _dbContext = dbContext;
        _logger = logger;
        _entityOrderProvider = entityOrderProvider;
    }

    public void ClearData()
    {
        try
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Error during clearing data {ExceptionMessage}", ex.Message);
            throw;
        }
    }
}

