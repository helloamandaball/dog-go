using DogGoMVC.Models;
using DogGoMVC.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DogGoMVC.Controllers
{
    public class DogController : Controller
    {
        private readonly IDogRepository _dogRepo;

        // ASP.NET will give us an instance of our Dog Repository. This is called "Dependency Injection"
        public DogController(IDogRepository dogRepository)
        {
            _dogRepo = dogRepository;
        }

        // GET: DogController
        // put an [Authorize] annotation above any action for which a user would need to be logged in
        [Authorize]
        public ActionResult Index()
        {
            //new helper method to get the currently logged in owner and query for only their dogs in the database [just the int ownerId = GetCurrentUserId(); part]
            int ownerId = GetCurrentUserId();

            List<Dog> dogs = _dogRepo.GetAllDogs(ownerId);

            return View(dogs);
        }

        // GET: DogController/Details/5
        public ActionResult Details(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // GET: DogController/Create
        // put an [Authorize] annotation above any action for which a user would need to be logged in
        [Authorize]
        public ActionResult Create()
        {
            int ownerId = GetCurrentUserId();

            return View(ownerId);
        }

        // POST: DogController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id 
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.AddDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: DogController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            int currentUser = GetCurrentUserId();

            if (dog == null || dog.OwnerId != currentUser)
            {
                return NotFound();
            }
            return View(dog);
        }

        // POST: DogController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Dog dog)
        {
            try
            {
                // update the dogs OwnerId to the current user's Id 
                dog.OwnerId = GetCurrentUserId();

                _dogRepo.UpdateDog(dog);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // GET: DogController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Dog dog = _dogRepo.GetDogById(id);

            return View(dog);
        }

        // POST: DogController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Dog dog)
        {
            try
            {
                _dogRepo.DeleteDog(id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(dog);
            }
        }

        // Getting the current user in controllers:
        // Getting the id of the current logged in user will be something that we'll need to do many times in our controller, so let's separate this out into its own helper method.
        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
