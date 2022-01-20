using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSpace.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicSpace.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartsController : Controller
    {
        private readonly DataManager dataManager;
        public ChartsController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        // GET: ChartsController
        public async Task<ActionResult> Index()
        {
            return View(await dataManager.WeeklyCharts.GetWeeklyCharts().ToListAsync());
        }

        // GET: ChartsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WeeklyChartsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WeeklyChartsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WeeklyChartsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WeeklyChartsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WeeklyChartsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WeeklyChartsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
