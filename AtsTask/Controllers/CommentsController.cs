using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Db.Context.Models;
using DAL.Contracts;
using ViewModels;

namespace AtsTask.Controllers
{
    public class CommentsController : Controller
    { 
        private readonly ICrudOperation<CommentViewModel> _operation;
        private readonly ICrudOperation<PostViewModel> _operationPosts;
        private readonly ICrudOperation<UserViewModel> _operationUser;

        public CommentsController(ICrudOperation<CommentViewModel> operation,
            ICrudOperation<PostViewModel> operationPosts,
            ICrudOperation<UserViewModel> operationUser
            )
        {
            _operation = operation;
            _operationPosts = operationPosts;
            _operationUser = operationUser;
        }
          
        // GET: Comments
        public async Task<IActionResult> Index()
        {
            return View(_operation.Get());
        }

        [NonAction]
        SelectList GetPosts(int? postId = null)
        {
            if(postId.HasValue)
                return new SelectList(_operationPosts.Get(), "Id", "Id", postId);

            return new SelectList(_operationPosts.Get(), "Id", "Id");
        }

        [NonAction]
        SelectList GetUsers(int? userId = null)
        {
            if (userId.HasValue)
                return new SelectList(_operationUser.Get(), "Id", "Login", userId);

            return new SelectList(_operationUser.Get(), "Id", "Id");
        }

        // GET: Comments/Create
        public IActionResult Create()
        {
            ViewData["PostId"] = GetPosts();
            ViewData["UserId"] = GetUsers();
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Contents,UserId,PostId")]
            CommentViewModel comments)
        {
            if (ModelState.IsValid)
            {
                _operation.Insert(comments);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = GetPosts(comments.PostId);
            ViewData["UserId"] = GetUsers(comments.UserId);
            return View(comments);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment  = _operation.GetSingle(id ?? 0);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["PostId"] = GetPosts(comment.PostId);
            ViewData["UserId"] = GetUsers(comment.UserId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Contents,UserId,PostId")]
            CommentViewModel comments)
        {
            if (id != comments.Id)  
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _operation.Update(comments);
                return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = GetPosts(comments.PostId);
            ViewData["UserId"] = GetUsers(comments.UserId);
            return View(comments);
        }

        // GET: Comments/Delete/5
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
