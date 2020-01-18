using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlyFashion.Models
{
    public class Item
    {
        public string Type { get; set; }

        public string Colour { get; set; }

        public string Season { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string Title { get; set; }

        public string TimestampUtc { get; set; }

        public Item(string title, string description)
        {

            Title = title;
            Description = description;

        }

        public Item()
        {

        }
    }
}
