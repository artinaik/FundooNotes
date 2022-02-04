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
    public class LabelRL:ILabelRL
    {
        Context context;
        private readonly IConfiguration configuration;
        public LabelRL(Context context, IConfiguration config)
        {
            this.context = context;//appcontext to for api
            this.configuration = config;//for startup file instance
        }
        public Labels CreateLabel(long userID,long noteID,string labelName)
        {
            try
            {
                Labels labels = new Labels();
                var findlabel = context.Labels.Where(e => e.LabelName == labelName).FirstOrDefault();
                if(findlabel==null)
                {
                    labels.LabelName = labelName;
                    labels.NoteID = noteID;
                    labels.Id = userID;
                    context.Labels.Add(labels);
                    context.SaveChanges();
                    return labels;
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
        public IEnumerable<Labels> RenameLabel(long userID,string oldLabelName,string labelName)
        {
            IEnumerable<Labels> labels;
            labels = context.Labels.Where(e =>e.Id==userID&&e.LabelName==oldLabelName).ToList();
            if(labels != null)
            {
                foreach(var label in labels)
                {
                    label.LabelName = labelName;
                }             
                context.SaveChanges();
                return labels;
            }
            else
            {
                return null;
            }
           
        }
        public bool RemoveLabel(long userID, string labelName)
        {
            IEnumerable<Labels> labels;
            labels = context.Labels.Where(e => e.Id == userID && e.LabelName == labelName).ToList();
            if (labels != null)
            {
                foreach (var label in labels)
                {
                    context.Labels.Remove(label);
                }
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool RemoveLabelByNoteID(long userID,long noteID, string labelName)
        {
            var label = context.Labels.Where(e => e.Id == userID && e.LabelName == labelName && e.NoteID == noteID).FirstOrDefault();
            if (label != null)
            {
                context.Labels.Remove(label);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public IEnumerable<Labels> GetLabelsByNoteID(long userID,long noteID)
        {
            try
            {
                var result = context.Labels.Where(e => e.NoteID == noteID && e.Id == userID).ToList();
                if(result!=null)
                {
                    return result;
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
