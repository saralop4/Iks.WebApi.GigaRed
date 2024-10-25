﻿using Microsoft.AspNetCore.Mvc;

namespace Iks.WebApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class IksController : ControllerBase
    {
        // GET: api/<IksController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<IksController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<IksController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<IksController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IksController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
