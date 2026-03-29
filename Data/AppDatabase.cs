using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FreshFarmApp.Models;

namespace FreshFarmApp.Data
{
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public AppDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<FarmProduct>().Wait();}
        

        // Users
        public Task<User> GetUserAsync(Guid userId)
         => _database.Table<User>().Where(u => u.UserID == userId).FirstOrDefaultAsync();

         // Users
        // AppDatabase.cs
        public Task<User> GetUserByEmailAsync(string email)
        {
            return _database.Table<User>()
                            .Where(u => u.Email == email)
                            .FirstOrDefaultAsync();
        }
           //var query = "SELECT * FROM Users WHERE Email = '" + email + "'";
        //var user = database.Query<User>(query).FirstOrDefault();
        
       public Task<int> SaveUserAsync(User user)
         => _database.InsertOrReplaceAsync(user);


        public Task<int> DeleteUserAsync(User user)
            => _database.DeleteAsync(user);

        // INSERT
        public Task<int> AddProductAsync(FarmProduct product)
        {
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;
            return _database.InsertAsync(product);
        }

        // UPDATE
        public Task<int> UpdateProductAsync(FarmProduct product)
        {
            product.UpdatedAt = DateTime.UtcNow;
            return _database.UpdateAsync(product);
        }

        // DELETE
        public Task<int> DeleteProductAsync(FarmProduct product)
        {
            return _database.DeleteAsync(product);
        }

        // GET ALL
       public Task<List<FarmProduct>> GetAllProductsAsync()
        {
            return _database.Table<FarmProduct>()
                            .Where(p => p.IsAvailable)
                            .ToListAsync();
        }
        /*
        public async Task<List<Product>> GetAllProductsAsync()
        {
            if (UserSession.CurrentUser == null)
            {
                throw new UnauthorizedAccessException("User must be authenticated.");
            }

             return _database.Table<FarmProduct>()
                            .Where(p => p.IsAvailable)
                            .ToListAsync();
        }
        */
        // GET BY ID
        public Task<FarmProduct?> GetProductByIdAsync(int id)
        {
            return _database.Table<FarmProduct>()
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        // SEARCH
       public Task<List<FarmProduct>> SearchProductsAsync(string keyword)
        {
            var lower = keyword.ToLower();
            return _database.Table<FarmProduct>()
                            .Where(p => p.Name.ToLower().Contains(lower) ||
                                        p.Category.ToLower().Contains(lower))
                            .ToListAsync();
        }


        // FILTER BY CATEGORY
        public Task<List<FarmProduct>> GetByCategoryAsync(string category)
        {
            return _database.Table<FarmProduct>()
                            .Where(p => p.Category == category && p.IsAvailable)
                            .ToListAsync();
        } 

    }

}