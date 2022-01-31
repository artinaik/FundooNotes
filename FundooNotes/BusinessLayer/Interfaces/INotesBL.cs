﻿using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface INotesBL
    {
        public bool CreateNotes(NotesModel notesModel);
        public IEnumerable<Notes> GetAllNotes();
        public IEnumerable<Notes> GetAllNotesByUserID(int id);
        public bool DeleteNote(int notesID);
        public bool UpdateNotes(int noteID,UpdateNotesModel notesModel);
        public bool Colorchange(long userId,long noteID, string color);
        public bool ArchieveChange(long userId, long noteID);
        public bool PinChange(long userId, long noteID);
        public bool TrashChange(long userId, long noteID);
        public bool UploadImage(long userId, long noteID, IFormFile file);

    }
}
