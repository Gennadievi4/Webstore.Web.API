using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services.InMemory;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private WebStoreDB _db;
        private ILogger<WebStoreDbInitializer> _logger;

        private DbInMemory _inMemoryDb;
        public WebStoreDbInitializer(WebStoreDB db, ILogger<WebStoreDbInitializer> logger, IEmployeesData employeesData)
        {
            _db = db;
            _logger = logger;

            _inMemoryDb = (DbInMemory)employeesData;
        }

        public void Initialize()
        {
            _logger.LogInformation("Инициализация Базы Данных");

            var db = _db.Database;

            if (db.GetPendingMigrations().Any())
            {
                _logger.LogInformation("Миграции не применены..");
                db.Migrate();
                _logger.LogInformation("Миграции выполнены успешно");
            }
            else
                _logger.LogInformation("Структура бд в ауктуальном состоянии");

            try
            {
                InitializeProducts();
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex, "Ошибка при инициализации БД данными товаров");
                throw;
            }
        }

        public void InitializeProducts()
        {
            if (_db.Products.Any())
                return;

            using (_db.Database.BeginTransaction())
            {
                _db.Sections.AddRange(_inMemoryDb.Sections);

                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                _db.Database.CommitTransaction();
            }

            using (_db.Database.BeginTransaction())
            {
                _db.Brands.AddRange(_inMemoryDb.Brands);

                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                _db.Database.CommitTransaction();
            }

            using (_db.Database.BeginTransaction())
            {
                _db.Products.AddRange(_inMemoryDb.Products);

                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _db.SaveChanges();
                _db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                _db.Database.CommitTransaction();
            }
        }
    }
}
