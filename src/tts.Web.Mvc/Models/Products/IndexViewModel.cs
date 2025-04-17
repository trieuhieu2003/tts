using Microsoft.AspNetCore.Mvc.Rendering;
using tts.Products.Dto;
using System.Collections.Generic;

namespace tts.Web.Models.Products
{
    public class IndexViewModel
    {

        public ProductDto Product { get; set; }

        public IReadOnlyList<ProductDto> Products { get; }

        public IReadOnlyList<SelectListItem> Categories { get; set; }

        public IndexViewModel(IReadOnlyList<ProductDto> products, IReadOnlyList<SelectListItem> categories) 
        {
            Products = products;
            Categories = categories;
        }

        public IndexViewModel()
        {
        }
    }
}
