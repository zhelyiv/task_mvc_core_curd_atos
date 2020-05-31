using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using ViewModels;
using DAL.Contracts;
using DAL.DataServices.Contracts;

namespace AtsTask.Controllers
{
    public class BlogsController : Controller
    {
        private readonly ICrudOperation<BlogViewModel> _operation;
        private readonly ICrudOperation<UserViewModel> _operationUser;
        IBlogOperations BlogOperations => (IBlogOperations)_operation;

        public BlogsController(ICrudOperation<BlogViewModel> operation,
            ICrudOperation<UserViewModel> operationUser
            )
        {
            _operation = operation;
            _operationUser = operationUser;
        }

        public void Dispose()
        {
            _operation.Dispose();
            _operationUser.Dispose();
        }

        // GET: Blogs
        public async Task<IActionResult> Index(int userId = 0, int blogId = 0)
        {
            if(userId > 0)
                return View(BlogOperations.GetByUserId(userId));
            else if(blogId > 0)
                return View(_operation.Get(blogId));

            return View(_operation.Get());
        }
         
        [NonAction]
        SelectList GetUsers(int? OwnerUserId = null)
        {
            var users = _operationUser.Get();
            if(OwnerUserId.HasValue)
                return new SelectList(users, "Id", "Login", OwnerUserId);

            return new SelectList(users, "Id", "Login"); 
        }

        // GET: Blogs/Create
        public IActionResult Create()
        { 
            ViewData["OwnerUserId"] = GetUsers();
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OwnerUserId")] BlogViewModel blogs)
        {
            if (ModelState.IsValid)
            { 
                _operation.Insert(blogs); 
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerUserId"] = GetUsers(blogs.OwnerUserId); 
            return View(blogs);
        }
         
        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _operation.Delete(id ?? 0);

            return RedirectToAction(nameof(Index));
        }
         
    }
}
