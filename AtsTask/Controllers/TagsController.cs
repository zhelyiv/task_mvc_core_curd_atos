using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; 
using DAL.Contracts;
using ViewModels;

namespace AtsTask.Controllers
{
    public class TagsController : Controller
    {
        private readonly ICrudOperation<TagViewModel> _operation;

        public TagsController(ICrudOperation<TagViewModel> operation)
        {
            _operation = operation;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(_operation.Get());
        }
         
        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TagViewModel tags)
        {
            if (ModelState.IsValid)
            {
                _operation.Insert(tags); 
                return RedirectToAction(nameof(Index));
            }
            return View(tags);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
             
            return View(_operation.Get(id??0));
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TagViewModel tags)
        {
            if (id != tags.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _operation.Update(tags);
                return RedirectToAction(nameof(Index));
            }
            return View(tags);
        }

        // GET: Tags/Delete/5
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
