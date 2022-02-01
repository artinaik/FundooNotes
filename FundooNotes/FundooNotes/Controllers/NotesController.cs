using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotesBL notesBL;
        public NotesController(INotesBL notesBL)
        {
            this.notesBL = notesBL;
        }
        [Authorize]
        [HttpPost]
        public IActionResult CreateNotes(NotesModel notesModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (notesBL.CreateNotes(notesModel, userId))
                {
                    return this.Ok(new { Success = true, message = "Notes creation done successfully" });
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
                if (notesBL.UpdateNotes(noteID, notesModel))
                {
                    return this.Ok(new { Success = true, message = "Notes updated successfully" });
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
        [HttpGet]
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
                if (notesBL.Colorchange(userId,noteID, color))
                {
                    return this.Ok(new { Success = true, message = "Color changed successfully" });
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
                if (notesBL.ArchieveChange(userId, noteID))
                {
                    return this.Ok(new { Success = true, message = "Archieve changed successfully" });
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
                if (notesBL.PinChange(userId, noteID))
                {
                    return this.Ok(new { Success = true, message = "Pin changed successfully" });
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
                if (notesBL.TrashChange(userId, noteID))
                {
                    return this.Ok(new { Success = true, message = "Trash changed successfully" });
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
                if(notesBL.UploadImage(userId, noteID, image))
                {
                    return this.Ok(new { Success = true, message = "Image uploaded successfully" });
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
