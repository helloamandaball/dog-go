using DogGoMVC.Models;
using System.Collections.Generic;

namespace DogGoMVC.Repositories
{
    public interface INeighborhoodRepository
    {
        List<Neighborhood> GetAll();
    }
}
