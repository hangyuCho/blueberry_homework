using MongoDB.Driver;
using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Domain.Exceptions;
using BlueberryHomeworkApp.Domain.Specification;
using IResult = BlueberryHomeworkApp.Application.IResult;

namespace BlueberryHomeworkApp.Infrastructure.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(MongoDbContext dbContext, string collectionName = "blueberry_db")
        {
            _collection = dbContext.GetCollection<T>(collectionName);
        }

        public async Task<IResult> AddAsync(T entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                return Result.Error(ex);
            }
        }

        public async Task<IResult> UpdateAsync(T entity)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", entity.GetType().GetProperty("Id").GetValue(entity, null));
                var result = await _collection.ReplaceOneAsync(filter, entity);
                return result.IsAcknowledged ? Result.Ok() : Result.Error(new Exception("Update failed"));
            }
            catch (Exception ex)
            {
                return Result.Error(ex);
            }
        }

        public async Task<IResult<T>> GetAsync<TKey>(TKey id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", id);
                var entity = await _collection.Find(filter).FirstOrDefaultAsync();
                return entity == null
                    ? Result<T>.Error(new DataNotFoundException(typeof(T), id))
                    : Result<T>.Ok(entity);
            }
            catch (Exception ex)
            {
                return Result<T>.Error(ex);
            }
        }

        public async Task<IResult> DeleteAsync(T entity)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("_id", entity.GetType().GetProperty("Id").GetValue(entity, null));
                var result = await _collection.DeleteOneAsync(filter);
                return result.IsAcknowledged ? Result.Ok() : Result.Error(new Exception("Delete failed"));
            }
            catch (Exception ex)
            {
                return Result.Error(ex);
            }
        }

        public async Task<IResult<List<T>>> FindAllAsync()
        {
            try
            {
                var list = await _collection.Find(FilterDefinition<T>.Empty).ToListAsync();
                return Result<List<T>>.Ok(list);
            }
            catch (Exception ex)
            {
                return Result<List<T>>.Error(ex);
            }
        }

        public async Task<IResult<T>> GetAsync(ISpecification<T> spec)
        {
            try
            {
                var filter = spec.ToExpression();

                var entity = await _collection.Find(filter).FirstOrDefaultAsync();

                return entity == null
                    ? Result<T>.Error(new DataNotFoundException(typeof(T), filter.Parameters))
                    : Result<T>.Ok(entity);
            }
            catch (Exception ex)
            {
                return Result<T>.Error(ex);
            }
        }
    }
}