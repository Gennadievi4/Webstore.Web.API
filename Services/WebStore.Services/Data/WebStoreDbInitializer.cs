using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL.Context;
using WebStore.Domain.Identity;
using WebStore.Interfaces.Services;
using WebStore.Services.Products.InMemory;

namespace WebStore.Services.Data
{
    public class WebStoreDbInitializer
    {
        private WebStoreDB _db;
        private UserManager<User> _UserManager;
        private RoleManager<Role> _RoleManager;
        private ILogger<WebStoreDbInitializer> _logger;

        private DbInMemory _inMemoryDb;
        public WebStoreDbInitializer(
            WebStoreDB db,
            UserManager<User> UserManager,
            RoleManager<Role> RoleManager,
            ILogger<WebStoreDbInitializer> logger,
            IEmployeesData employeesData)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
            _logger = logger;
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
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Ошибка при инициализации БД данными товаров");
                throw;
            }

            try
            {
                InitizializeIdentityAsync().Wait();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Ошибка при инициализации БД системы Identity");
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

        public async Task InitizializeIdentityAsync()
        {
            async Task CheckRole(string RoleName)
            {
                if (!await _RoleManager.RoleExistsAsync(RoleName))
                    await _RoleManager.CreateAsync(new Role { Name = RoleName });
            }

            await CheckRole(Role.Administrator);
            await CheckRole(Role.User);

            if (await _UserManager.FindByNameAsync(User.Administrator) is null)
            {
                var admin = new User
                {
                    UserName = User.Administrator
                };

                var creation_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);

                if (creation_result.Succeeded)
                    await _UserManager.AddToRoleAsync(admin, Role.Administrator);
                else
                {
                    var errors = creation_result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Ошибка при создании учётной записи администратора {string.Join(",", errors)}");
                }
            }
        }
    }
}
