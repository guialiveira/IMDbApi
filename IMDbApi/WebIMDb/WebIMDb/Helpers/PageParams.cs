﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebIMDb.Helpers
{
    public class PageParams
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 5;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }
        public string Nome { get; set; } = string.Empty;
        public string Diretor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
    }
}