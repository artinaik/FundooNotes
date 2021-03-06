using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NotesBL:INotesBL
    {
        INotesRL notesRL;
        public NotesBL(INotesRL notesRL)
        {
            this.notesRL = notesRL;
        }
        public Notes CreateNotes(NotesModel notesModel,long userId)
        {
            try
            {
                return notesRL.CreateNotes(notesModel, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Notes> GetAllNotes()
        {
            try
            {
                return notesRL.GetAllNotes();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Notes> GetAllNotesByUserID(int id)
        {
            try
            {
                return notesRL.GetAllNotesByUserID(id);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool DeleteNote(int notesID)
        {
            try
            {
                if (notesRL.DeleteNote(notesID))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Notes UpdateNotes(int noteID,UpdateNotesModel notesModel)
        {
            try
            {
                return notesRL.UpdateNotes(noteID, notesModel);
                  
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Notes Colorchange(long userId, long noteID, string color)
        {
            try
            {
                return notesRL.Colorchange(userId, noteID, color);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Notes ArchieveChange(long userId, long noteID)
        {
            try
            {
                return notesRL.ArchieveChange(userId, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Notes PinChange(long userId, long noteID)
        {
            try
            {
                return notesRL.PinChange(userId, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Notes TrashChange(long userId, long noteID)
        {
            try
            {
                return notesRL.TrashChange(userId, noteID);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public Notes UploadImage(long userId, long noteID, IFormFile file)
        {
            try
            {
                return notesRL.UploadImage(userId,noteID,file);
            }
            catch (Exception)
            {

                throw;
            }
        }
      
    }
}
