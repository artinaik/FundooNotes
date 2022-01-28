using CommonLayer.Models;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRL
    {
        public bool CreateNotes(NotesModel notesModel);
        public IEnumerable<Notes> GetAllNotes();
        public IEnumerable<Notes> GetAllNotesByUserID(int id);
        public bool DeleteNote(int notesID);
        public bool UpdateNotes(int noteID, UpdateNotesModel notesModel);
    }
}
