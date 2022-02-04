using CommonLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.AppContext;
using RepositoryLayer.Entites;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NotesRL : INotesRL
    {
        Context context;

        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;
        public NotesRL(Context context, IConfiguration config, IHostingEnvironment hostingEnvironment)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
            this.hostingEnvironment = hostingEnvironment;
        }
        public Notes CreateNotes(NotesModel notesModel,long userId)
        {
            try
            {
                Notes notes = new Notes();
                notes.Id = userId;
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
                    return notes;
                else
                    return null;
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
            return context.Notes.Where(e => e.Id == id).ToList();
        }
        public bool DeleteNote(int notesID)
        {
            Notes notes = context.Notes.Where(e => e.NoteId == notesID).FirstOrDefault();
            if (notes != null)
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
        public Notes UpdateNotes(int noteID, UpdateNotesModel notesModel)
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
                return notes;
            else
                return null;
        }
        public Notes Colorchange(long userId, long noteID, string color)
        {
            try
            {
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    note.Color = color;
                    context.SaveChanges();
                    return note;
                }
                else
                {
                    return null;
                }
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
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    bool checkarchieve = note.IsArchive;
                    if (checkarchieve == true)
                    {
                        note.IsArchive = false;
                    }
                    if (checkarchieve == false)
                    {
                        note.IsArchive = true;
                    }
                    context.SaveChanges();
                    return note;
                }
                else
                {
                    return null;
                }
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
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    bool checkpin = note.IsPin;
                    if (checkpin == true)
                    {
                        note.IsPin = false;
                    }
                    if (checkpin == false)
                    {
                        note.IsPin = true;
                    }
                    context.SaveChanges();
                    return note;
                }
                else
                {
                    return null;
                }
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
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);
                if (note != null)
                {
                    bool checktrash = note.IsTrash;
                    if (checktrash == true)
                    {
                        note.IsTrash = false;
                    }
                    if (checktrash == false)
                    {
                        note.IsTrash = true;
                    }
                    context.SaveChanges();
                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Notes UploadImage(long userId,long noteID,IFormFile file)
        {
            try
            {
                var target = Path.Combine(hostingEnvironment.ContentRootPath, "Images");
                Directory.CreateDirectory(target);
                var filePath = Path.Combine(target, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                Notes note = context.Notes.FirstOrDefault(e => e.Id == userId && e.NoteId == noteID);            
                if(note!=null)
                {
                    note.Image = file.FileName;
                    var result = context.SaveChanges();
                    return note;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
            

        }
    }
}
