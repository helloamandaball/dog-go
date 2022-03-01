using DogGoMVC.Models;
using System;
using System.Collections.Generic;

namespace DogGoMVC.Models.ViewModels
{
    public class WalkerViewModel
    {
        public Walker Walker { get; set; }
        public Owner Owner{ get; set; }
        public List<Walk> Walk { get; set; }
    }
}