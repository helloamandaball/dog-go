using DogGoMVC.Models;
using DogGoMVC.Models.ViewModels;
using DogGoMVC.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DogGoMVC.Controllers
{
    public class WalkersController : Controller
    {

        private readonly IWalkerRepository _walkerRepo;
        private readonly IOwnerRepository _ownerRepo;
        private readonly IWalkRepository _walkRepo;

        // ASP.NET will give us an instance of our Walker Repository. This is called "Dependency Injection"
        public WalkersController(IWalkerRepository walkerRepository, IOwnerRepository ownerRepository, IWalkRepository walkRepository)
        {
            _walkerRepo = walkerRepository;
            _ownerRepo = ownerRepository;
            _walkRepo = walkRepository;
        }

        // GET: WalkersController
        public ActionResult Index()
        {
            int userId = GetCurrentUserId();
            if (userId != 0)
            {
                //Update the Index method in the walkers controller so that owners only see walkers in their own neighborhood. Use the OwnerRepository to look up the owner by Id before getting the walkers.
                Owner currentUser = _ownerRepo.GetOwnerById(userId);
                List<Walker> walkersInUsersNeighborhood = _walkerRepo.GetWalkersInNeighborhood(currentUser.NeighborhoodId);

                return View(walkersInUsersNeighborhood);
            }
            else 
            {
                //If a user goes to /walkers and is not logged in, they should see the entire list of walkers.
                //This code will get all the walkers in the Walker table, convert it to a List and pass it off to the view.
                List<Walker> allWalkers = _walkerRepo.GetAllWalkers();

                return View(allWalkers);
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                id = "0";
            }
            return int.Parse(id);
        }

        // GET: WalkersController/Details/5
        //Notice that this method accepts an id parameter. When the ASP.NET framework invokes this method for us, it will take whatever value is in the url and pass it to the Details method.
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            WalkerViewModel walkerViewModel = new WalkerViewModel()
            {
                Walker = _walkerRepo.GetWalkerById(id),
                Walk = _walkRepo.GetWalksByWalkerId(id)
            };

            if (walker == null)
            {
                return NotFound();
            }

            return View(walkerViewModel);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
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

        // GET: WalkersController/Edit/5
        // Same as what we did for Details:
        public ActionResult Edit(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);

            if (walker == null)
            {
                return NotFound();
            }

            return View(walker);
        }

        // POST: WalkersController/Edit/5
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

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
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
