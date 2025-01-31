using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MindSpace.Application.Commons.Utilities.Seeding;

namespace MindSpace.Infrastructure.Persistence
{
    internal class DatabaseCleaner : IDataCleaner
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<DatabaseCleaner> _logger;
        private readonly IEntityOrderProvider _entityOrderProvider;

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
                DropForeignKeyConstraints();
                TruncateTables();
                RecreateConstraints();
            }
            catch (Exception ex)
            {
                _logger.LogCritical("Error during clearing data {ExceptionMessage}", ex.Message);
                throw;
            }
        }

        private void DropForeignKeyConstraints()
        {
            _dbContext.Database.ExecuteSqlRaw(@"
                DECLARE @sql NVARCHAR(MAX) = N'';
                SELECT @sql += N'
                    ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id))
                    + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) +
                    ' DROP CONSTRAINT ' + QUOTENAME(name) + ';'
                FROM sys.foreign_keys;
                EXEC sp_executesql @sql;
            ");
        }

        private void TruncateTables()
        {
            foreach (var entityName in _entityOrderProvider.GetOrderedEntities())
            {
                var entity = _dbContext.Model.FindEntityType(entityName);
                var tableName = entity!.GetTableName();
                var schemaName = entity!.GetSchema();

                _dbContext.Database.ExecuteSqlRaw($"TRUNCATE TABLE {schemaName}.{tableName}");
            }
        }

        private void RecreateConstraints()
        {
            _dbContext.Database.EnsureCreated();
        }
    }
}