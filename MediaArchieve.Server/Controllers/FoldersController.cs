using MediaArchieve.Server.Models;
using MediaArchieve.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MediaArchieve.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoldersController : ControllerBase
    {
        private readonly AppDbContext _context;
        public FoldersController(AppDbContext db)
        {
            _context = db;
        }

        /// <summary>
        /// GET
        /// Возвращает все элементы Folder
        /// url: .../api/folders/
        /// </summary>
        [HttpGet]
        public ActionResult<IEnumerable<Folder>> Get()
        {
            return _context.Folders.Include(x => x.Items).ToList();
        }

        /// <summary>
        /// GET
        /// Возвращает Folder из базы данных с указанным id
        /// url: .../api/folders/id
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<Folder> GetById(int id)
        {

            var folder = _context.Folders
                .Include(x => x.Items)
                .FirstOrDefault(f => f.Id == id);                

            if (folder == null)
                return NotFound();
           
            return folder;
        }

        /// <summary>
        /// POST
        /// Добавляет в базу данных Folder 
        /// url: .../api/folders/
        /// </summary>
        [HttpPost]
        public ActionResult<Folder> Post(Folder fo)
        {
            if (fo == null)
                return BadRequest();
            _context.Folders.Add(fo);
            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// PUT
        /// Заменяет Folder
        /// url: .../api/folders/id
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult Put(int id, Folder folder)
        {
            if (id != folder.Id)
                return BadRequest();            
            var folderAim = _context.Folders
                .Include(x => x.Items)
                .FirstOrDefault(f => f.Id == id);
            folderAim.Update(folder);
            _context.SaveChanges();

            return NoContent();
        }

        /// <summary>
        /// DELETE
        /// Удаляет Folder
        /// url: .../api/folders/id
        /// </summary>
        [HttpDelete("{id}")]
        public ActionResult<Folder> Delete(int id)
        {
            var deletedFolder = _context.Folders
                .Include(x => x.Items)
                .FirstOrDefault(f => f.Id == id);

            if (deletedFolder == null)
                return NotFound();
            _context.Entry(deletedFolder).State = EntityState.Deleted;
            _context.Folders.Remove(deletedFolder);
            _context.SaveChanges();

            return deletedFolder;
        }
    }
}