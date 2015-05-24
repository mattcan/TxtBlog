using System;
using System.Collections.Generic;

namespace TxtBlog.ViewModels.Items
{
    public class PostViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Excerpt { get; set; }

        public DateTime PostDate { get; set; }

        public bool IsDraft { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public string Slug { get; set; }
    }
}