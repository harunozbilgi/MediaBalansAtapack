﻿using MediaBalans.Domain.Common;

namespace MediaBalans.Domain.Entities.Languages
{
    public class PageLanguage : BaseEntity
    {
        public Guid PageId { get; set; }
        public string Lang_Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public virtual Page Page { get; set; }

    }
}