using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace DatabaseApi.Database
{
    /// <summary>
    /// Cache database for storing json data as a list.
    /// Easier than making a new database & api for each mini project.
    /// Will be up to the calling api to cast returned json etc.
    /// </summary>
    public class CacheDatabase : IDatabase
    {
        private IMemoryCache _cache;

        public CacheDatabase(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public bool Add(string key, JsonElement json)
        {
            List<JsonElement> jsonList = new List<JsonElement>();

            if (!_cache.TryGetValue(key, out jsonList))
            {
                jsonList = new List<JsonElement>();
            }

            jsonList.Add(json);

            MemoryCacheEntryOptions x = new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(2) };
            _cache.Set(key, jsonList, x);

            return true;
        }

        public bool Delete(string key, int index)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            List<JsonElement> jsonList = new List<JsonElement>();
            _cache.TryGetValue(key, out jsonList);

            jsonList.RemoveAt(index);

            MemoryCacheEntryOptions x = new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(2) };
            _cache.Set(key, jsonList, x);

            return true;
        }

        public List<JsonElement> Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return null;
            }

            List<JsonElement> jsonList = new List<JsonElement>();
            _cache.TryGetValue(key, out jsonList);
            return jsonList;
        }

        public JsonElement Get(string key, int index)
        {
            if (string.IsNullOrEmpty(key))
            {
                return new JsonElement();
            }

            List<JsonElement> jsonList = new List<JsonElement>();
            _cache.TryGetValue(key, out jsonList);
            return jsonList[index];
        }

        public bool Update(string key, int index, JsonElement jsonString)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }

            List<JsonElement> jsonList = new List<JsonElement>();
            _cache.TryGetValue(key, out jsonList);

            jsonList[index] = jsonString;

            MemoryCacheEntryOptions x = new MemoryCacheEntryOptions() { AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(2) };
            _cache.Set(key, jsonList, x);

            return true;

        }
    }
}