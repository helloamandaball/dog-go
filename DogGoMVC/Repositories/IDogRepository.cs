using DogGoMVC.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace DogGoMVC.Repositories
{
    public interface IDogRepository
    {
        List<Dog> GetAllDogs(int ownerId);
        Dog GetDogById(int id);
        List<Dog> GetDogsByOwnerId(int id);

        void AddDog(Dog dog);
        void UpdateDog(Dog dog);
        void DeleteDog(int dogId);

    }
}