﻿using FirelloProject.Models;

namespace FirelloProject.ViewModels
{
    public class ProductCreateVM
    {
        public  string ?Name { get; set; }
        public double Price { get; set; }
        public int? CategoryId { get; set; }
        public IFormFile[]? Photos { get; set; }
       
    }
}
