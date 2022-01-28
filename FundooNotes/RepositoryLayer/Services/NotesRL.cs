using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NotesRL:INotesRL
    {
        Context context;

        private readonly IConfiguration configuration;

        public NotesRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }
        public bool CreateNotes(NotesModel notesModel)
        {
            try
            {
                Notes notes = new Notes();
                notes.Id = notesModel.Id;
                notes.Title = notesModel.Title;
                notes.Message = notesModel.Message;
                notes.Remainder = notesModel.Remainder;
                notes.Color = notesModel.Color;
                notes.Image = notesModel.Image;
                notes.IsArchive = notesModel.IsArchive;
                notes.IsPin = notesModel.IsPin;
                notes.IsTrash = notesModel.IsTrash;
                context.Notes.Add(notes);
                var result = context.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<Notes> GetAllNotes()
        {
            return context.Notes.ToList();
        }
        public IEnumerable<Notes> GetAllNotesByUserID(int id)
        {
            return context.Notes.Where(e=>e.Id==id).ToList();
        }
        public bool DeleteNote(int notesID)
        {
            Notes notes = context.Notes.Where(e => e.NoteId == notesID).FirstOrDefault();
            if (notes!=null)
            {
                context.Notes.Remove(notes);
                context.SaveChanges();
                return true;
            }               
            else
            {
                return false;
            }
              
            
        }
        public bool UpdateNotes(int noteID, UpdateNotesModel notesModel)
        {
            Notes notes = context.Notes.Where(e => e.NoteId == noteID).FirstOrDefault();
            notes.Title = notesModel.Title;
            notes.Message = notesModel.Message;
            notes.Remainder = notesModel.Remainder;
            notes.Color = notesModel.Color;
            notes.Image = notesModel.Image;
            notes.IsArchive = notesModel.IsArchive;
            notes.IsPin = notesModel.IsPin;
            notes.IsTrash = notesModel.IsTrash;
            context.Notes.Update(notes);
            var result = context.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }
    }
}
