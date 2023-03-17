﻿using FirelloProject.Models;

namespace FirelloProject.ViewModels
{
    public class SaleVM
    {
        public AppUser User { get; set; }
        public DateTime SaleDate { get; set; }
        public List<SalesProducts> SalesProducts { get; set; }
    }
}
