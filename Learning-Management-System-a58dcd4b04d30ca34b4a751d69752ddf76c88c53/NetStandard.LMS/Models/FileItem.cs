﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetStandard.LMS.Models
{
    public class FileItem : ContentItem
    {
        public string Path { get; set; }

        public override string ToString()
        {
            return $"{base.ToString()}" +
                   $"\n\t{Path}";
        }
    }
}