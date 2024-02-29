using Back.DTO;
using Back.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Back.Controllers
{
    [EnableCors("AllowAny")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            _context = context;

        }

        // GET: api/Categories
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return _context.Categories;
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<Category> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<string> PutCategory(int id, [FromForm] CategoryDto categoryDto)
        {
            if (id != categoryDto.CategoryId)
            {
                return "修改商品種類失敗";
            }
            Category category = await _context.Categories.FindAsync(id);
            category.CategoryName = categoryDto.CategoryName;
            category.Description = categoryDto.Description;
            if (categoryDto.ImageFile != null)
            {
                using (BinaryReader br = new BinaryReader(categoryDto.ImageFile.OpenReadStream()))
                {
                    category.Picture = br.ReadBytes(((int)categoryDto.ImageFile.Length));
                }
            }


            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return "修改商品種類失敗";
                }
                else
                {
                    throw;
                }
            }

            return "修改商品種類成功";
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostCategory([FromForm] CategoryDto CategoryDto)
        {
            var category = new Category
            {
                CategoryName = CategoryDto.CategoryName,
                Description = CategoryDto.Description,
            };

            if (CategoryDto.ImageFile != null)
            {
                using (BinaryReader br = new BinaryReader(CategoryDto.ImageFile.OpenReadStream()))
                {
                    category.Picture = br.ReadBytes((int)CategoryDto.ImageFile.Length);
                }
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return $"商品種類編號:{category.CategoryId}";
        }



        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<string> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return "商品種類刪除失敗";
            }
            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return "商品種類刪除失敗";
            }
            return "商品種類刪除成功";
        }

        // POST: api/Categories/Filter
        [HttpPost("Filter")]
        public async Task<IEnumerable<Category>> FilterCategories([FromBody] Category category)
        {
            return _context.Categories.Where(x => x.CategoryId == category.CategoryId
                                             || x.CategoryName.Contains(category.CategoryName)
                                             || x.Description.Contains(category.Description));
        }

        [HttpGet("GetPicture/{id}")]
        public async Task<FileResult> GetPicture(int id)
        {
            string fileName = Path.Combine("StaticFiles", "images", "noimage.png");
            Category? c = await _context.Categories.FindAsync(id);
            byte[] ImageContent = c.Picture != null ? c.Picture : System.IO.File.ReadAllBytes(fileName);
            return File(ImageContent, "image/png");
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
