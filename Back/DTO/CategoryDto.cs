﻿namespace Back.DTO
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
