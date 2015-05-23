using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Runtime;
using TxtBlog.Models;
using TxtBlog.DAL;
using System.Linq;
using TxtBlog.ViewModels.Home;
using TxtBlog.ViewModels.Items;
using CommonMark;

namespace TxtBlog.Controllers
{
    public class PostsController : Controller
    {
        private readonly IApplicationEnvironment _appEnvironment;

        private PostRepository _postRepo; // initialised in consutructor

        public PostsController(IApplicationEnvironment appEnv)
        {
            _appEnvironment = appEnv;
            _postRepo = new PostRepository(_appEnvironment);
        }

        [HttpGet, Route("posts/{slug?}")]
        public ActionResult Index(string slug)
        {
            if (string.IsNullOrEmpty(slug) == true)
            {
                return View("AllPosts", this.getPosts());
            }

            return View("SinglePost", this.getPost(slug));
        }

        private PostViewModel getPost(string slug)
        {
            Post p = _postRepo.GetPostBySlug(slug);

            return new PostViewModel()
            {
                Title = p.Title,
                Content = CommonMarkConverter.Convert(p.Content),
                Excerpt = CommonMarkConverter.Convert(p.Excerpt),
                PostDate = p.Date,
                IsDraft = p.Draft,
                Tags = p.Tags
            };
        }

        private IEnumerable<PostViewModel> getPosts()
        {
            List<PostViewModel> posts = new List<PostViewModel>();
            foreach (Post p in _postRepo.GetLastTenPosts())
            {
                posts.Add(new PostViewModel()
                {
                    Title = p.Title,
                    Content = CommonMarkConverter.Convert(p.Content),
                    Excerpt = CommonMarkConverter.Convert(p.Excerpt),
                    PostDate = p.Date,
                    IsDraft = p.Draft,
                    Tags = p.Tags
                });
            }

            return posts;
        }
    }
}