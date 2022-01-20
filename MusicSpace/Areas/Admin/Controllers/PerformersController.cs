using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain;
using MusicSpace.Domain.Entities;
using MusicSpace.Service.FileService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PerformersController : Controller
    {
        private readonly DataManager dataManager;
        private readonly FileService fileService;
        public PerformersController(DataManager dataManager, IWebHostEnvironment env)
        {
            this.dataManager = dataManager;
            this.fileService = new FileService(env);
        }
        public async Task<IActionResult> Index()
        {
            return View(await dataManager.Performers.GetPerformers().OrderBy(e=> e.PerformerName).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("PerformerName,AvatarFile")] Performer performer)
        {
            if (ModelState.IsValid)
            {
                performer.AvatarPath = fileService.Upload(performer.AvatarFile, FileType.Performer);
                performer.Id = Guid.NewGuid().ToString();
                dataManager.Performers.SavePerformer(performer);
                return RedirectToAction(nameof(Index));
            }
            return View(performer);
        }

        public IActionResult Edit(string id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var performer = dataManager.Performers.GetPerformerById(id);
            if(performer == null)
            {
                return NotFound();
            }
            return View(performer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,PerformerName,AvatarFile")] Performer performer)
        {
            if (ModelState.IsValid)
            {
                fileService.DeleteUpload(performer.AvatarPath, FileType.Performer);
                performer.AvatarPath = fileService.Upload(performer.AvatarFile, FileType.Performer);
                dataManager.Performers.SavePerformer(performer);
                return RedirectToAction(nameof(Index));
            }
            return View(performer);
        }

        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performer = dataManager.Performers.GetPerformerById(id);
            if (performer == null)
            {
                return NotFound();
            }
            return View(performer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                dataManager.Performers.DeletePerformer(id);
                ///
                /// TODO
                /// 
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(DeleteConfirmed), id);
        }
    }
}
