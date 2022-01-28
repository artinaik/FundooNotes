using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
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
                if(notesBL.CreateNotes(notesModel))
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
    }
}
