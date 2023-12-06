﻿namespace MLGStore.Services.DTOs
{
    public class ArticleDTO
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public StoreDTO Store { get; set; }
    }
}
