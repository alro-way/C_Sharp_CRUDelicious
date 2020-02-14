using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace C_Sharp_CRUDelicious.Models
{
    public class Dish
    {
        // auto-implemented properties need to match the columns in your table
        // the [Key] attribute is used to mark the Model property being used for your table's Primary Key
        [Key]
        public int DishesId{ get; set; }
        [Required]
        public string Name{ get; set; }
        // MySQL VARCHAR and TEXT types can be represeted by a string
        [Required]
        public string Chef { get; set; }
        [Required]
        [Range(1,5, ErrorMessage = "TAstiness must be in range between 1 and 5")]
        public int Tastiness { get; set; }
        [Required]
        [Range(1,10000000000000, ErrorMessage = "Calories could not be 0")]
        public int Calories { get; set; }
        [Required]
        public string Description { get; set; }
        // The MySQL DATETIME type can be represented by a DateTime
        public DateTime Created_at {get;set;} = DateTime.Now;
        public DateTime Updated_at {get;set;} = DateTime.Now;
    }
}