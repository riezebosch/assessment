using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.db;
using api.db.model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private WordListContext context;

        public ValuesController(WordListContext context)
        {
            this.context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await this.context.WordLists.AsNoTracking().ToListAsync());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var list = await context
                .WordLists
                .Include(l => l.Entries)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == id);

            if (list != null)
            {
                return Ok(list);
            }

            return NotFound(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
