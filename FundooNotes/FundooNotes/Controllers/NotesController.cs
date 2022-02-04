using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotesBL notesBL;
        Context context;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public NotesController(INotesBL notesBL, IMemoryCache memoryCache, IDistributedCache distributedCache,Context context)
        {
            this.notesBL = notesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.context = context;
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.CreateNotes(notesModel, userId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Notes creation done successfully",Response= result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes creation unsuccessfull" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult UpdateNotes(int noteID, UpdateNotesModel notesModel)
        {
            try
            {
                var result = notesBL.UpdateNotes(noteID, notesModel);
                if (result!=null)
                {
                    return this.Ok(new { Success = true, message = "Notes updated successfully" ,Response= result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Note with given ID not found" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpDelete]
        public IActionResult DeleteNote(int noteID)
        {
            try
            {
                if (notesBL.DeleteNote(noteID))
                {
                    return this.Ok(new { Success = true, message = "Notes deleted successfully" });

                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes with given ID not found" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
         
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var notesList = new List<Notes>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                notesList = JsonConvert.DeserializeObject<List<Notes>>(serializedNotesList);
            }
            else
            {
                notesList = await context.Notes.ToListAsync();
                serializedNotesList = JsonConvert.SerializeObject(notesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
            }
            return Ok(notesList);
        }
        [Authorize]
        [HttpGet]
        public IEnumerable<Notes> GetAllNotes()
        {
            try
            {
                return notesBL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpGet ("{id}")]
        public IEnumerable<Notes> GetNotesByUserID(int id)
        {
            try
            {
                return notesBL.GetAllNotesByUserID(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult ColorNotes(long noteID,string color)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.Colorchange(userId, noteID, color);
                if (result!=null)
                {
                    return this.Ok(new { Success = true, message = "Color changed successfully",Response= result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult ArchieveNotes(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.ArchieveChange(userId, noteID);
                if (result!=null)
                {
                    return this.Ok(new { Success = true, message = "Archieve changed successfully",Response=result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult PinNotes(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.PinChange(userId, noteID);
                if (result!=null)
                {
                    return this.Ok(new { Success = true, message = "Pin changed successfully",Response= result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPut]
        public IActionResult TrashNotes(long noteID)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.TrashChange(userId, noteID);
                if (result!=null)
                {
                    return this.Ok(new { Success = true, message = "Trash changed successfully",Response=result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access denied" });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPut]
        public IActionResult UploadImage(long noteID,IFormFile image)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = notesBL.UploadImage(userId, noteID, image);
                if (result!=null)
                {
                    return this.Ok(new { Success = true, message = "Image uploaded successfully" ,Response= result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "User access is denied" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
