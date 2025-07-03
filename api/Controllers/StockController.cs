using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet] //get = read
        public IActionResult GetAll()
        {
            var stocks = _context.Stocks.ToList().Select(s => s.ToStockDTO()); //use the mapper to convert the stock model to stock DTO
            return Ok(stocks); //returns 200 OK with the list of stocks           
        }

        [HttpGet("{id}")] //get by id, .Net will use model binding to extract the string into an int 
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stocks.Find(id);
            if (stock == null)
            {
                return NotFound(); //returns 404 Not Found if stock is not found
            }
            return Ok(stock.ToStockDTO()); //returns 200 OK with the stock
        }

        [HttpPost] //post = create
        public IActionResult Create([FromBody] CreateStockRequestDTO stockDTO)
        {
            var stockModel = stockDTO.ToStockFromCreateDTO(); //use the mapper to convert the stock DTO to stock model
            _context.Stocks.Add(stockModel); //add the stock model to the context
            _context.SaveChanges(); //save changes to the database

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDTO()); //returns 201 Created with the stock DTO and the location of the created resource
        }
    }
}