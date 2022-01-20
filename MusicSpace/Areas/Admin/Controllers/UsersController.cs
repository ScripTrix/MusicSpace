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
    public class UsersController : Controller
    {
        private readonly DataManager dataManager;
        public UsersController(DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public async Task <IActionResult> Index()
        {
            return View(await dataManager.ApplicationUsers.GetApplicationUsers().ToListAsync());
        }
    }
}