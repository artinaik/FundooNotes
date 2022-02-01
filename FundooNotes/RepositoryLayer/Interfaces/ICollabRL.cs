﻿using CommonLayer.Models;
using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface ICollabRL
    {
        public bool AddCollaborator(CollabaoratorModel collabaoratorModel);
        public IEnumerable<Collaborator> GetCollaboratorsByID(long userID, long noteID);
        public bool RemoveCollaborator(long userID, long noteID, string collabEmail);
    }
}
