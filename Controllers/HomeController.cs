using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Runtime;
using TxtBlog.Models;
using TxtBlog.DAL;
using TxtBlog.ViewModels.Home;
using TxtBlog.ViewModels.Items;
using CommonMark;

namespace TxtBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationEnvironment _appEnvironment;
        
        private PostRepository _postRepo; // initialised in consutructor

        public HomeController(IApplicationEnvironment appEnv)
        {
            _appEnvironment = appEnv;
            _postRepo = new PostRepository(_appEnvironment);
        }

        public IActionResult Index()
        {
            List<PostViewModel> pvmList = new List<PostViewModel>();
            foreach(Post p in _postRepo.GetLastTenPosts()){
                PostViewModel pvm = new PostViewModel(){
                    Title = p.Title,
                    Content = CommonMarkConverter.Convert(p.Content),
                    Excerpt = p.Excerpt,
                    PostDate = p.Date,
                    IsDraft = p.Draft,
                    Tags = p.Tags
                };
                
                pvmList.Add(pvm);
            }
            
            return View( new IndexViewModel(){
                Posts = pvmList
            });
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }

        
    }
}