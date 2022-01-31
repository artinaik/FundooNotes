﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class NotesModel
    {
        public long Id { get; set; }
        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime Remainder { get; set; }


        public string Color { get; set; }

        public IFormFile Image { get; set; }

        public bool IsArchive { get; set; }

        public bool IsPin { get; set; }

        public bool IsTrash { get; set; }
        public DateTime? Createat { get; set; }
        public DateTime? Modifiedat { get; set; }

    }
}
