using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlyFashion.Models
{
    public class Item
    {
        [Required(ErrorMessage = "Please enter the type of item you want to sell.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Please enter the size of your item.")]
        public string Size { get; set; }

        [Required(ErrorMessage = "Please enter the colour of item.")]
        public string Colour { get; set; }

        [Required(ErrorMessage = "Please enter the season someone would wear this item.")]
        public string Season { get; set; }

        [Required(ErrorMessage = "Please enter your contact and further details.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please the item price.")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Please enter a title for your item.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter the link to the image of your item.")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Please enter the condition of the item.")]
        public string Condition { get; set; }

        
        
        public Item(string type, string colour, string season, string description, double price, string title, string image, string condition, string size)
        {
            Type = type;
            Colour = colour;
            Season = season;
            Description = description;
            Price = price;
            Title = title;
            Image = image;
            Condition = condition;
            Size = size;

        }

        public Item()
        {

        }
    }
}
