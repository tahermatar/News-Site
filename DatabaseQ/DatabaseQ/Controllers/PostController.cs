using DatabaseQ.Data;
using DatabaseQ.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseQ.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext _db;
        public PostController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int page, string searchKey)
        {
            var numberOfPages = Math.Ceiling(_db.Posts.Count(x => !x.IsDelete && (x.Title.Contains(searchKey)
                                                                  || string.IsNullOrWhiteSpace(searchKey))) / 10.0);

            if (page > numberOfPages || page < 1)
            {
                page = 1;
            }

            var skipValue = (page - 1) * 10;

            var posts = _db.Posts.Include(x => x.Category).Where(x => !x.IsDelete && (x.Title.Contains(searchKey) 
                                                                      || string.IsNullOrWhiteSpace(searchKey)))
                                                                         .Skip(skipValue).Take(10).ToList();

            var postsVm = new List<PostViewModel>();
            foreach (var post in posts)
            {
                var postVm = new PostViewModel();
                postVm.Id = post.Id;
                postVm.Title = post.Title;
                postVm.TimeToRead = post.TimeToRead;
                postVm.ImageUrl = post.ImageUrl;
                postVm.CreatedAt = post.CreatedAt;
                postVm.Body = post.Body;
                postVm.CategoryName = post.Category.Name;

                postsVm.Add(postVm);
            }

            var result = new PagingResultViewModel();
            result.NumberOfPages = (int)numberOfPages;
            result.CurrentPage = page;
            result.Data = postsVm;

            ViewBag.searchKey = searchKey;

            return View(result);
        }
        public IActionResult Delete(int id)
        {
            var post = _db.Posts.Find(id);
            post.IsDelete = true;
            _db.Posts.Update(post);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
