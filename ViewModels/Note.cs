﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRS.ViewModels
{
    public class Note
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
