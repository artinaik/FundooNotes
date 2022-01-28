using BusinessLayer.Interfaces;
using CommonLayer.Models;
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
        public bool CreateNotes(NotesModel notesModel)
        {
            try
            {
                return notesRL.CreateNotes(notesModel);
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
        public bool UpdateNotes(int noteID,UpdateNotesModel notesModel)
        {
            try
            {
                if (notesRL.UpdateNotes(noteID,notesModel))
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
