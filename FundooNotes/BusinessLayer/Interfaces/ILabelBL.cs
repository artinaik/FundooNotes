﻿using RepositoryLayer.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        public bool CreateLabel(long userID, long noteID, string labelName);
        public bool RenameLabel(long userID, string oldLabelName,string labelName);
        public bool RemoveLabel(long userID, string labelName);
        public IEnumerable<Labels> GetLabelsByNoteID(long userID, long noteID);
    }
}
