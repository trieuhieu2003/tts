using Abp.Application.Services.Dto;

namespace tts.Products.Dto
{
    public class PagedProductDto : PagedAndSortedResultRequestDto
    {


        public string Keyword { get; set; }
        public decimal? MinPrice { get; set; }  // Tham số lọc giá tối thiểu
        public decimal? MaxPrice { get; set; }  // Tham số lọc giá tối đa



        //public PagedProductDto()
        //{
        //    MaxResultCount = 15;
        //}
    }
}
