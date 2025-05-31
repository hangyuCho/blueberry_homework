using System.Linq.Expressions;
using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Domain.Exceptions;
using BlueberryHomeworkApp.Domain.Specification;
using IResult = BlueberryHomeworkApp.Application.IResult;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace BlueberryHomeworkApp.Infrastructure.Repositories
{
    public class MongoRepository<T>(MongoDbContext dbContext, string collectionName, ILogger<MongoRepository<T>> logger)
        : IRepository<T>
        where T : class
    {
        private readonly IMongoCollection<T> _collection = dbContext.GetCollection<T>(collectionName);

        public async Task<IResult> AddAsync(T entity)
        {
            try
            {
                await _collection.InsertOneAsync(entity);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity to {CollectionName}",
                    _collection.CollectionNamespace.CollectionName);
                return Result.Error(ex);
            }
        }

        public async Task<IResult> UpdateAsync(T entity)
        {
            try
            {
                var idProperty = typeof(T).GetProperty("Id");
                if (idProperty == null)
                {
                    throw new InvalidOperationException("Entity must have an Id property");
                }

                var id = idProperty.GetValue(entity)!;
                var filter = Builders<T>.Filter.Eq("_id", id);
                await _collection.ReplaceOneAsync(filter, entity);
                return Result.Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error updating entity in {CollectionName}",
                    _collection.CollectionNamespace.CollectionName);
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
                    ? Result<T>.Error(new DataNotFoundException(typeof(T), id!))
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
                var filter = Builders<T>.Filter.Eq("_id", entity.GetType().GetProperty("Id")?.GetValue(entity, null));
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

        public async Task<IResult<List<T>>> FindAsync(ISpecification<T> spec)
        {
            try
            {
                var filter = spec.ToExpression();
                var list = await _collection.Find(filter).ToListAsync();
                return Result<List<T>>.Ok(list);
            }
            catch (Exception ex)
            {
                return Result<List<T>>.Error(ex);
            }
        }
    }
}