using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ViewModels
{
    public class SearchCarVM
    {
        public Condition? Condition { get; set; }
        public int? BrandId { get; set; }
        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }
        public string Model { get; set; }
        public Color Color { get; set; }

    }
}
