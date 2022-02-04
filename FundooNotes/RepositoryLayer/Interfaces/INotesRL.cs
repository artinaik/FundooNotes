using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRL
    {
        public Notes CreateNotes(NotesModel notesModel,long userId);
        public IEnumerable<Notes> GetAllNotes();
        public IEnumerable<Notes> GetAllNotesByUserID(int id);
        public bool DeleteNote(int notesID);
        public Notes UpdateNotes(int noteID, UpdateNotesModel notesModel);
        public Notes Colorchange(long userId,long noteID, string color);
        public Notes ArchieveChange(long userId, long noteID);
        public Notes PinChange(long userId, long noteID);
        public Notes TrashChange(long userId, long noteID);
        public Notes UploadImage(long userId, long noteID, IFormFile file);
       

    }
}
