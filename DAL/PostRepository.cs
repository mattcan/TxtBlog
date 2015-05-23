using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using TxtBlog.Models;
using System.IO;
using System.Collections.Generic;
using Microsoft.Framework.Runtime;
using System.Linq;
using System;

namespace TxtBlog.DAL
{
    public class PostRepository
    {
        // Where the YAML configuration ends
        // This is used to split the content so that the deserializer
        // will not choke
        private const string YAMLEND = "...\n";

        private readonly IApplicationEnvironment _appEnvironment;

        public PostRepository(IApplicationEnvironment appEnv)
        {
            _appEnvironment = appEnv;
        }

#region public
        public IEnumerable<Post> GetAllPosts()
        {
            return this.getPostsToLimit(0);
        }

        public IEnumerable<Post> GetLastTenPosts()
        {
            return this.getPostsToLimit(10);
        }

        public Post GetPostBySlug(string slug)
        {
            string fileName = string.Format("{0}.md", slug);
            return this.getPost(fileName);
        }
#endregion

#region private
        private Post getPost(string filename)
        {
            Post post = null;
            using (FileStream fs = File.OpenRead("blog/" + filename))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    var deserializer = new Deserializer(
                        namingConvention: new CamelCaseNamingConvention()
                    );

                    string fileContents = sr.ReadToEnd();
                    string[] yamlEnd = new string[] { YAMLEND };
                    string[] parts = fileContents.Split(yamlEnd, StringSplitOptions.None);

                    string metaData = string.Format("{0}\n{1}", parts[0], YAMLEND);
                    string content = parts[1];

                    // create post object
                    post = deserializer.Deserialize<Post>(new StringReader(metaData));
                    if (post != null)
                    {
                        post.Content = content;
                    }
                }
            }

            return post;
        }

        private IEnumerable<Post> getPostsToLimit(int limit = 0)
        {
            DirectoryInfo di = new DirectoryInfo(_appEnvironment.ApplicationBasePath);
            FileInfo[] fileEntries = di.GetFiles("blog/*.md");

            var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());

            List<Post> posts = new List<Post>();
            foreach (FileInfo f in fileEntries)
            {
                //parse file yaml
                Post post = null;
                using (StreamReader sr = new StreamReader(f.OpenRead()))
                {
                    string fileContents = sr.ReadToEnd();
                    string[] separators = new string[] { YAMLEND };
                    string[] parts = fileContents.Split(separators, StringSplitOptions.None);

                    string metaData = string.Format("{0}\n{1}", parts[0], YAMLEND);
                    string content = parts[1];

                    post = deserializer.Deserialize<Post>(new StringReader(metaData));
                    if (post != null)
                    {
                        post.Content = content;
                    }
                }

                if (post != null)
                {
                    posts.Add(post);
                }
            }

            if (limit > 0)
            {
                return posts.OrderByDescending(p => p.Date).Take(limit);
            }

            return posts.OrderByDescending(p => p.Date);
        }
    }
#endregion
}