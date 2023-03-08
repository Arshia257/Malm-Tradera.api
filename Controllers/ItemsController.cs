using MalmöTradera.api.Data;
using MalmöTradera.api.Model;
using MalmöTradera.api.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MalmöTradera.api.Controllers
{   
    [ApiController]
    [Route("/api/v1/Items")]
    public class ItemsController : Controller
    {
        private readonly MalmöTraderaContext _context;
        private readonly IConfiguration _config;

        private readonly string _ImageBaseUrl;

        public ItemsController(MalmöTraderaContext context, IConfiguration config ) {
            _context = context;
            _config = config;  
            _ImageBaseUrl = config.GetSection("apiImageUrl").Value;

        }


        [HttpGet()]

        public async Task<IActionResult> ListAll()
        {
            var Results = await _context.Item
            .Where(c => c.ISSold == false)
            .Select(A => new {
            Id = A.id,
            Name = A.Name,
            Price = A.value,
            Description = A.Description,
            ImageUrl = string.IsNullOrEmpty(A.ImageUrl) ? _ImageBaseUrl + "no-Image.png" : _ImageBaseUrl + A.ImageUrl
            }).ToListAsync();

            return Ok(Results);
        }


        [HttpGet("{id}")]

        public async Task <IActionResult> ListById (int id)
        {
            var Results = await _context.Item.Select(C => new {
                id = C.id,
                Name = C.Name,
                Price = C.value,
                Description = C.Description,
                ImageUrl = string.IsNullOrEmpty(C.ImageUrl) ? _ImageBaseUrl + "no-Image.png" : _ImageBaseUrl + C.ImageUrl
            }).SingleOrDefaultAsync(R => R.id == id);

            return Ok(Results);
        }


        [HttpPost()]

        
            public async Task<IActionResult> Add ( ItemPostViewModel _Item )
            {
                if (!ModelState.IsValid) return BadRequest("Information saknas");

                var ItemToAdd = new ItemsModel {
                    Name = _Item.Name,
                    value = _Item.value,
                    Description = _Item.Description,
                    ImageUrl = _Item.ImageUrl
                };

                try
                {
                    await _context.Item.AddAsync(ItemToAdd);

                    if (await _context.SaveChangesAsync() > 0)
                    {
                        return CreatedAtAction(nameof(ListById), new{id = ItemToAdd.id}, new{
                            id = ItemToAdd.id,
                            Name = ItemToAdd.Name,
                            Price = ItemToAdd.value,
                            Description = ItemToAdd.Description,
                            ImageUrl = string.IsNullOrEmpty(ItemToAdd.ImageUrl) ? "no-Image.png" : _ImageBaseUrl + ItemToAdd.ImageUrl
                        });
                    }
                return StatusCode(500, "Internal error");
                }

                catch (Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                     return StatusCode(500, "Internal error");  

                }
            }

            [HttpDelete("{id}")]
            
                public async Task<IActionResult> Create (int id)
                {
                     var items = await _context.Item.FindAsync(id);

                     if (items is null) return NotFound("Hittade inte bilen");

                    _context.Item.Remove(items);

                    if (await _context.SaveChangesAsync() > 0)
                    return NoContent();

                    return StatusCode(500, "Internal Server Error");
                }
            }
        }
