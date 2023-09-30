using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Project.Data;
using Project.Models;
using System.Reflection.Metadata;

namespace Project.Data.Repository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        AppDbContext _db;
        DbSet<T> _dbSet;
        public EntityRepository(AppDbContext db)
        {
            _db = db;
            this._dbSet = db.Set<T>();
        }
        public async Task AddAsync(T obj)
        {
             _db.Set<T>().AddAsync(obj);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var t = await _db.Set<T>().FindAsync(id);
            _db.Set<T>().Remove(t);
            await _db.SaveChangesAsync();
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }


        public async Task<T> UpdateAsync(object id, T obj)
        {
            if (id is int)
            {
                int intValue = (int)id;
                // Process the integer value
                Console.WriteLine("Integer value: " + intValue);
            }
            else if (id is string)
            {
                string stringValue = (string)id;
                // Process the string value
                Console.WriteLine("String value: " + stringValue);
            }
            else
            {
                // Handle other types or invalid inputs
                throw new Exception("invalid parameter");
            }
            _db.Update(obj);
            await _db.SaveChangesAsync();
            return obj;
            
        }
    }
}
