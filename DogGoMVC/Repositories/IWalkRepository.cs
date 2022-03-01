using System.Collections.Generic;
using DogGoMVC.Models;

namespace DogGoMVC.Repositories
{
    public interface IWalkRepository
    {
        List<Walk> GetAllWalks();
        List<Walk> GetWalksByWalkerId(int it);
        //Walker GetWalkerById(int id);
        //List<Walker> GetWalkersInNeighborhood(int neighborhoodId);
        //void AddWalk(Walk walk);
    }
}