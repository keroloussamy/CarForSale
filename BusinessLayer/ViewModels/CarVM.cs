﻿using DAL;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLayer.ViewModels
{
    public class CarVM
    {
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }
        [Required]
        public double Mileage { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Engine { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public Condition Condition { get; set; }
        [Required]
        public string Image { get; set; }

        [ForeignKey("Dealer")]
        public string DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
