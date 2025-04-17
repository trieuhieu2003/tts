﻿using Abp.AutoMapper;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace tts.Products.Dto
{
    [AutoMapTo(typeof(Product))]

    public class ProductListDto
    {
        //public int Id { get; set; }

        [Required]
        [StringLength(Product.MaxNameLength)]
        public string Name { get; set; }
        [Required]

        public decimal Price { get; set; }
        //[Required]

        public IFormFile ImageUrl { get; set; }

        public int Discount { get; set; }

        public int CategoryId { get; set; }
        public string NameCategory { get; set; }



    }
}
