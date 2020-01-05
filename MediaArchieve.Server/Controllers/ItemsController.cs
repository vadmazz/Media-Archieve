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
        [HttpPost("{folderId}")]
        public ActionResult<Item> PostItem(int folderId, Item item)
        {
            var folder = _context.Folders
                .Include(x => x.Items)
                .FirstOrDefault(f => f.Id == folderId);
            if (item == null || folder == null)
                return BadRequest();
            folder.Items.Add(item);
            
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Item>> GetAll(int id)
        {
            var folder = _context.Folders
                .Include(x => x.Items)
                .FirstOrDefault(f => f.Id == id);
            if (folder == null)
                return NotFound();
            return folder.Items.ToList();
        }

        [HttpGet("{folderId}/{itemId}")]
        public ActionResult<Item> GetById(int folderId, int itemId)
        {
            var folder = _context.Folders
                .Include(x => x.Items)
                .FirstOrDefault(f => f.Id == folderId);
            var item = folder.Items
                .FirstOrDefault(x => x.Id == itemId);
            if (folder == null || item == null)
                return NotFound();
            return item;
        }
    }
}