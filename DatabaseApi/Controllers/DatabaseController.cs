using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DatabaseApi.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseApi.Controllers
{
    /// <summary>
    /// Cache database for storing json data as a list.
    /// Easier than making a new database & api for each mini project.
    /// Will be up to the calling api to cast returned json etc.
    /// </summary>
    [Route("")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        IDatabase _database;

        public DatabaseController(IDatabase database)
        {
            _database = database;
        }

        [HttpGet]
        public IActionResult Ping()
        {
            return Ok("pong");
        }

        /// <summary>
        /// Will return all data in a cached object e.g. all users, all notes by every user.
        /// For mini projects this will be fine.
        /// </summary>
        [HttpGet]
        [Route("Get")]
        public IActionResult Get(string key)
        {
            return Ok(_database.Get(key));
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromQuery]string key, [FromBody] JsonElement jsonString)
        {
            if (_database.Add(key, jsonString))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromQuery]string key, [FromQuery]int index, [FromBody] JsonElement jsonString)
        {
            if (_database.Update(key, index, jsonString))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromQuery]string key, [FromQuery]int index)
        {
            if (_database.Delete(key, index))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteAll")]
        public IActionResult DeleteAll([FromQuery]string key)
        {
            if (_database.DeleteAll(key))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}