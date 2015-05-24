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

        [HttpGet]
        public IActionResult Index()
        {
            return View("AllPosts", this.getPosts());
        }

        [HttpGet, Route("posts/{slug}")]
        public IActionResult Get(string slug)
        {
            return View("SinglePost", this.getPost(slug));
        }

        private PostViewModel getPost(string slug)
        {
            Post p = _postRepo.GetPostBySlug(slug);

            return this.postViewModelFactory(p);
        }

        private IEnumerable<PostViewModel> getPosts()
        {
            List<PostViewModel> posts = new List<PostViewModel>();
            foreach (Post p in _postRepo.GetLastTenPosts())
            {
                posts.Add(this.postViewModelFactory(p));
            }

            return posts;
        }

        private PostViewModel postViewModelFactory(Post p)
        {
            return new PostViewModel()
            {
                Title = p.Title,
                Content = CommonMarkConverter.Convert(p.Content),
                Excerpt = CommonMarkConverter.Convert(p.Excerpt),
                PostDate = p.Date,
                IsDraft = p.Draft,
                Slug = p.Slug,
                Tags = p.Tags
            };
        }
    }
}