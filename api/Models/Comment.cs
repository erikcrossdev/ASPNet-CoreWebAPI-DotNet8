using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Comment
    {
        //Navigation properties allow us to navigate through models. 
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreateOne { get; set; } = DateTime.Now;
        public int? StockId { get; set; } //? means that this is a nullable, it can store an int or a null
        public Stock? stock { get; set; }
    }
}