using System;
using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

namespace TxtBlog.Models
{
    public class Post
    {
        // This is the number of charachters to have in the excerpt
        private const int TRIM = 149;

        public string Title { get; set; }

        public string Excerpt
        {
            get
            {
                int trimSize = this.Content.Length < TRIM ? this.Content.Length : TRIM;
                return this.Content.Substring(0, trimSize);
            }
        }

        public IEnumerable<string> Tags { get; set; }
        
        public string Slug {get;set;}

        public DateTime Date { get; set; }

        public bool Draft { get; set; }

        public string Content { get; set; }
    }
}