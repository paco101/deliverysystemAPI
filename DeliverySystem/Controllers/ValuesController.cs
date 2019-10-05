﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeliverySystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeliverySystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private ApiDbContext _dbContext;

        public ValuesController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            if (DateTime.Now.TimeOfDay > Config.AppConfiguration.EndWorkTime ||
                DateTime.Now.TimeOfDay < Config.AppConfiguration.StartWorkTime)
                return new string[]{DateTime.Now.TimeOfDay.ToString(),"kek"};
            
            return new string[] {DateTime.Now.TimeOfDay.ToString(), Config.AppConfiguration.StartWorkTime.ToString()};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}