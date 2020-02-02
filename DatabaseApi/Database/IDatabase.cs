using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DatabaseApi.Database
{
    public interface IDatabase
    {
        bool Add(string key, JsonElement json);
        List<JsonElement> Get(string key);
    }
}
