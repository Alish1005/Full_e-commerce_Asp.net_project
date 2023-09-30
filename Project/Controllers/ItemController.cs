using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Data.Repository;
using Project.Models;
using System.Data;


namespace Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ItemController : Controller
    {
        private readonly IEntityRepository<Item> _ItemRepo;

        public ItemController(IEntityRepository<Item> ItemRepo) {
            _ItemRepo = ItemRepo;
        }

        public async Task<IActionResult> Index()
        {
            var all=await _ItemRepo.GetAllAsync();
            return View(all);
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add([Bind("Name,Description,Price,IconUrl,quantity,isForMan")] Item item)
        {
            await _ItemRepo.AddAsync(item);
            return RedirectToAction(nameof(Index));

            if (ModelState.IsValid) {
                await _ItemRepo.AddAsync(item);
                return RedirectToAction(nameof(Index));
            } else {
                return View(item);
            }
            
        }
        public async Task<IActionResult> Update(int id)
        {
            var item= await _ItemRepo.GetByIdAsync(id);
            return View(item);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id,[Bind("Name,Description,Price,IconUrl,quantity,isForMan")] Item item)
        {
            item.Id= id;
            await _ItemRepo.UpdateAsync(id,item);
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _ItemRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
