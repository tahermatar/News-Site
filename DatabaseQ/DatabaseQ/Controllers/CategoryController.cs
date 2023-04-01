using DatabaseQ.Data;
using DatabaseQ.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseQ.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int page, string searchKey)
        {
            //Pagination
            var numberOfPages = Math.Ceiling(_db.Categories.Count(x => !x.IsDelete && (x.Name.Contains(searchKey)
                                                                       || string.IsNullOrWhiteSpace(searchKey))) / 10.0);
            if (page > numberOfPages || page < 1)
            {
                page = 1;
            }

            var skipValue = (page - 1) * 10;

            var categories = _db.Categories.Where(x => !x.IsDelete && (x.Name.Contains(searchKey)
                                           || string.IsNullOrWhiteSpace(searchKey))).Skip(skipValue).Take(10).ToList();

            // the first way to return list from CategoryModelView
            var categoriesVm = new List<CategoryViewModel>();
            foreach (var category in categories)
            {
                var categoryVm = new CategoryViewModel();
                categoryVm.Id = category.Id;
                categoryVm.Name = category.Name;
                categoryVm.PostCount = _db.Posts.Count(x => x.CategoryId == category.Id);
                // error under
                //categoryVm.LastPostAdded = _db.Posts.Where(x => x.CategoryId == category.Id).OrderByDescending(x => x.CreatedAt).FirstOrDefault().CreatedAt;
                var lastItem = _db.Posts.Where(x => x.CategoryId == category.Id).OrderByDescending(x => x.CreatedAt).FirstOrDefault();
                if (lastItem != null)
                {
                    categoryVm.LastPostAdded = lastItem.CreatedAt;
                }
                else
                {
                    categoryVm.LastPostAdded = null;
                }
                categoriesVm.Add(categoryVm);
            }
            //Pagination
            var result = new PagingResultViewModel();
            result.NumberOfPages = (int)numberOfPages;
            result.CurrentPage = page;
            result.Data = categoriesVm;

            ViewBag.searchKey = searchKey;
            return View(result);
            //return View(categoriesVm);
        }

        public IActionResult Delete(int id)
        {
            var category = _db.Categories.Find(id);
            //Soft Delete
            category.IsDelete = true;
            _db.Categories.Update(category);
            //Hard Delete
            //_db.Categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
