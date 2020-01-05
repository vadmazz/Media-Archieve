using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaArchieve.Server.Models;
using MediaArchieve.Shared;
using MediaArchieve.Shared.Items;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MediaArchieve.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public ItemsController(AppDbContext db)
        {
            _context = db;
        }
        /// <summary>
        /// POST
        /// Добавляет в базу данных Item 
        /// url: .../api/folderId/itemId
        /// </summary>
        [HttpPost("{id}")]
        public ActionResult<Item> PostDocument(int id, Document doc)
        {
            if (doc == null)
                return BadRequest();

            var folder = _context.Folders
                .Include(x => x.Items)
                .ThenInclude(x => x.Preview)
                .FirstOrDefault(f => f.Id == id);
            folder.Items.Add(doc);
            
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Item>> Get(int id)
        {
            var folder = _context.Folders
                .Include(x => x.Items)
                .ThenInclude(x => x.Preview)
                .FirstOrDefault(f => f.Id == id);
            return folder.Items.ToList();
        }

        [HttpGet("{folderId}/{itemId}")]
        public ActionResult<Item> GetById(int folderId, int itemId)
        {
            var folder = _context.Folders
                .Include(x => x.Items)
                .ThenInclude(x => x.Preview)
                .FirstOrDefault(f => f.Id == folderId);

            var item = folder.Items
                .FirstOrDefault(x => x.Id == itemId);

            if (folder == null || item == null)
                return NotFound();

            return item;
        }
    }
}