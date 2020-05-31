using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using DAL.Contracts;
using ViewModels;
using DAL.DataServices.Contracts;

namespace AtsTask.Controllers
{
    public class PostsController : Controller
    {
        private readonly ICrudOperation<PostViewModel> _operation;
        private readonly ICrudOperation<BlogViewModel> _operationBlogs;
        private readonly ICrudOperation<TagViewModel> _operationTags;
         
        IPostOperations PostOperations => (IPostOperations)_operation;

        public PostsController(ICrudOperation<PostViewModel> operation,
            ICrudOperation<BlogViewModel> operationBlogs,
            ICrudOperation<TagViewModel> operationTag)
        {
            _operation = operation;
            _operationBlogs = operationBlogs;
            _operationTags = operationTag;
        }

        // GET: Posts
        public async Task<IActionResult> Index(int? blogId)
        {
            if (blogId.HasValue)
                return View(PostOperations.GetByBlogId(blogId));
            else
                return View(_operation.Get());
        }
          
        public async Task<IActionResult> RedirectToBlog(int id)
        {
            return RedirectToAction("Index", "Blogs", new { blogId = id });
        }

        [NonAction]
        SelectList GetPosts(int? postId = null)
        {
            var posts = _operation.Get();
            if (postId.HasValue)
                return new SelectList(posts, "Id", "DisplayText", postId);

            return new SelectList(posts, "Id", "DisplayText");
        }

        [NonAction]
        SelectList GetTags()
        { 
            return new SelectList(_operationTags.Get(), "Id", "DisplayText");
        }

        [NonAction]
        SelectList GetBlogs(int? blogId = null)
        {
           var blogs = _operationBlogs.Get();
           if(blogId.HasValue)
                return new SelectList(blogs, "Id", "Id", blogId);

           return new SelectList(blogs, "Id", "Id");
        }

        public IActionResult AddTag(int? id)
        {
            ViewData["PostId"] = GetPosts(id);
            ViewData["TagId"] = GetTags();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTag([Bind("PostId, TagId")]
            PostTagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                PostOperations.AddTag(tag);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = GetPosts();
            return View(tag);
        }

        public IActionResult Create(int? postId)
        {
            ViewData["PostId"] = GetBlogs();
            ViewData["BlogId"] = GetBlogs();
            return View();
        }

        // POST: Posts1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Contents,BlogId")] PostViewModel posts)
        {
            if (ModelState.IsValid)
            {
                _operation.Insert(posts);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = GetBlogs(posts.BlogId); 
            return View(posts);
        }

        // GET: Posts1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _operation.GetSingle(id ?? 0);
            if (post == null)
            {
                return NotFound();
            }

            ViewData["BlogId"] = GetBlogs(post.BlogId);
            ViewData["PostTags"] = PostOperations.GetTags(id ?? 0);
            
            return View(post);
        }

        // POST: Posts1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Contents,BlogId")] PostViewModel posts)
        {
            if (id != posts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _operation.Update(posts);
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = GetBlogs(posts.BlogId);
            return View(posts);
        }


        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _operation.Delete(id ?? 0);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteTag(int PostId, int TagId)
        { 
            PostOperations.RemoveTag(PostId, TagId);

            return RedirectToAction(nameof(Edit), new { id = PostId });
        }

        
    }
}
