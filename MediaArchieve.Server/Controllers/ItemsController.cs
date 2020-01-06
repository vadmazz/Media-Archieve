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
        /// url: .../api/items/folderId/
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
        /// <summary>
        /// GET
        /// Получает все Items из папки с folderId
        /// url: .../api/items/folderId/
        /// </summary>
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
        
        /// <summary>
        /// GET
        /// Получает Item по Id 
        /// url: .../api/items/folderId/itemId
        /// </summary>
        [HttpGet("{folderId}/{itemId}")]
        public ActionResult<Item> GetById(int folderId, int itemId)
        {
            var folder = _context.Folders
                .Include(x => x.Items)
                .FirstOrDefault(f => f.Id == folderId);
            if (folder == null)
                return NotFound();
            var item = folder.Items
                .FirstOrDefault(x => x.Id == itemId);
            if (item == null)
                return NotFound();
            
            return item;
        }
        
        /// <summary>
        /// PUT
        /// Заменяет Item
        /// url: .../api/items/folderId/ItemId
        /// </summary>
        [HttpPut("{folderId}/{itemId}")]
        public ActionResult Put(int folderId, int itemId, Item item)
        {
            var targetItem = _context.Folders
                .Include(x => x.Items)
                .FirstOrDefault(f => f.Id == folderId)
                ?.Items
                .FirstOrDefault(i => i.Id == itemId);
            if (targetItem == null)
                return NotFound();
            targetItem.Update(item);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// DELETE
        /// Удаляет Item
        /// url: .../api/items/folderId/itemId
        /// </summary>
        [HttpDelete("{folderId}/{itemId}")]
        public ActionResult<Item> Delete(int folderId, int itemId)
        {
            var deletedFolder = _context.Folders
                .Include(x => x.Items)
                .FirstOrDefault(f => f.Id == folderId);
            if (deletedFolder == null)
                return NotFound();
            var deletedItem = deletedFolder.Items
                .FirstOrDefault(i => i.Id == itemId);
            if (deletedItem == null)
                return NotFound();
            
            _context.Entry(deletedItem).State = EntityState.Deleted;
            deletedFolder.Items.Remove(deletedItem);
            _context.SaveChanges();

            return deletedItem;
        }
    }
}