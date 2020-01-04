using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using MediaArchieve.Server.Models;
using MediaArchieve.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        ///GET:
        /// Возвращает все элементы Folder
        /// url: .../api/folders/
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Folder>> Get()
        {
            return _context.Folders;
        }

        /// <summary>
        /// GET:
        /// Возвращает Folder из базы данных с указанным id
        /// url: .../api/folders/id
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Folder> GetById(int id)
        {
            var folder = _context.Folders.Find(id);
            if (folder == null)
                return NotFound();
            return folder;
        }

        [HttpPost]
        public HttpResponseMessage Post(Folder fo)
        {
            var length = _context.Folders.ToArray().Length;
            _context.Folders.Add(fo);
            _context.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}