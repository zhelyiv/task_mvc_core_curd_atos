using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using ViewModels;
using DAL.Contracts;

namespace AtsTask.Controllers
{
    public class UsersController : Controller
    {
        private readonly ICrudOperation<UserViewModel> _operation;

        public UsersController(ICrudOperation<UserViewModel> operation)
        {
            _operation = operation;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(_operation.Get());
        }
         

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Login")] UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                _operation.Insert(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _operation.Get(id ?? 0);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Login")] UserViewModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _operation.Update(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _operation.Delete(id??0);

            return RedirectToAction(nameof(Index));
        }
         
    }
}
