using SinglePageSample.Db.DbStore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SinglePageSample.UnitTest.InMemoryStore
{
    public class InMemoryDbStore : IDbStore
    {
        private readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, object>> _data = new();

        public T Load<T>(string id)
        {
            var key = NormalizeId<T>(id);
            if (_data.TryGetValue(typeof(T), out var collection) && collection.TryGetValue(key, out var entity))
            {
                return (T)entity;
            }

            return default;
        }

        public T Load<T>(Guid id)
        {
            return Load<T>(id.ToString());
        }

        public IQueryable<T> Query<T>(string indexName)
        {
            return GetAll<T>().AsQueryable();
        }

        public IQueryable<T> Search<T>(string indexName, Expression<Func<T, object>> fieldSelector, string searchTerms)
        {
            var selector = fieldSelector.Compile();
            var pattern = WildcardToRegex(searchTerms);
            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

            return GetAll<T>()
                .Where(entity =>
                {
                    var value = selector(entity)?.ToString() ?? string.Empty;
                    return regex.IsMatch(value);
                })
                .AsQueryable();
        }

        public void Save<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }

            if (entity is IEntity entityWithId)
            {
                Save(entity, entityWithId.Id.ToString(CultureInfo.InvariantCulture));
                return;
            }

            Save(entity, Guid.NewGuid().ToString());
        }

        public void Save<T>(T entity, string id)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }

            var collection = GetOrCreateCollection<T>();
            collection[NormalizeId<T>(id)] = entity;
        }

        public void Save<T>(T entity, Guid id)
        {
            Save(entity, id.ToString());
        }

        public void Delete<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentException("entity");
            }

            if (entity is IEntity entityWithId)
            {
                var collection = GetOrCreateCollection<T>();
                collection.TryRemove(entityWithId.Id.ToString(CultureInfo.InvariantCulture), out _);
                return;
            }

            throw new ArgumentException("entity");
        }

        public int QuickCount<T>()
        {
            return GetAll<T>().Count();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private IEnumerable<T> GetAll<T>()
        {
            if (_data.TryGetValue(typeof(T), out var collection))
            {
                return collection.Values.Cast<T>();
            }

            return Enumerable.Empty<T>();
        }

        private ConcurrentDictionary<string, object> GetOrCreateCollection<T>()
        {
            return _data.GetOrAdd(typeof(T), _ => new ConcurrentDictionary<string, object>());
        }

        private static string NormalizeId<T>(string id)
        {
            var prefix = GetEntityCollectionPrefix<T>() + "/";
            if (id.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                return id[prefix.Length..];
            }

            return id;
        }

        private static string GetEntityCollectionPrefix<T>()
        {
            var entityName = typeof(T).Name;
            if (entityName.EndsWith("y", StringComparison.OrdinalIgnoreCase))
            {
                entityName = entityName[..^1] + "ies";
            }
            else if (!entityName.EndsWith("s", StringComparison.OrdinalIgnoreCase))
            {
                entityName += "s";
            }

            return entityName.ToLowerInvariant();
        }

        private static string WildcardToRegex(string searchTerms)
        {
            var escaped = Regex.Escape(searchTerms).Replace("\\*", ".*");
            return "^" + escaped + "$";
        }
    }
}
