using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ElectrStore
{
    [Authorize(Roles = "Owner")]
    public class ProductEditModel : PageModel, IHasProduct
    {
        public ProductEditModel(StoreContext context, INormalizer normalizer,
            IProductsRepository productsRepository)
        {
            _context = context;
            _normalizer = normalizer;
            _productsRepository = productsRepository;
        }
        private StoreContext _context { get; init; }
        private INormalizer _normalizer { get; init; }

        private IProductsRepository _productsRepository { get; init; }

        public ProductRecord ProductRecord { get; set; }
        public bool IsNewRec { get; set; }
        [BindProperty] public string? ProductId { get; set; }
        
        [BindProperty] public string? ProductCategory { get; set; }
        [BindProperty] public string? Name { get; set; }
        [BindProperty] public int ProductPrice { get; set; }
        
        [BindProperty] public int ProductQuantity { get; set; }
        
        [BindProperty] public bool ProductHotDeal { get; set; }
        
        [BindProperty] public int ProductDiscountPercent { get; set; }
        
        [BindProperty] public bool ProductIsDiscount { get; set; }
        [BindProperty] public string? PriceInput { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest();
            }
            ProductRecord = await _context.ProductRecords.FirstOrDefaultAsync(m => m.Id == id);
            if (ProductRecord == null)
            {
                return NotFound();
            }
            ProductId = id;
            //TODO: Тут используется CategoryId, очевидно, что должна быть потом ещё одна таблица
            ProductCategory = ProductRecord.CategoryId;
            Name = ProductRecord.ProductName;
            PriceInput = ProductRecord.PriceInput;
            ProductQuantity = ProductRecord.Quantity;
            ProductHotDeal = ProductRecord.IsHotDeal;
            ProductDiscountPercent = ProductRecord.DiscountPercent;
            ProductIsDiscount = ProductRecord.IsDiscount;
            return Page();
        }

        public async Task<IActionResult> OnPostSetQuantityAsync()
        {
            if (string.IsNullOrWhiteSpace(ProductId))
            {
                return BadRequest();
            }
            ProductRecord = await _context.Set<ProductRecord>()
                .FirstOrDefaultAsync(x => x.Id == ProductId);
            if (ProductRecord is null)
            {
                return NotFound();
            }

            ProductRecord.Quantity = ProductQuantity;
            _context.Update(ProductRecord);
            await _context.SaveChangesAsync();
            //TODO: Спросить как перенаправлять на страницу с поиском, а не где все товары
            return RedirectToPage("/Products/Pages/ProductIndex");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(ProductId))
            {
                return BadRequest();
            }
            ProductRecord = await _context.Set<ProductRecord>()
                .FirstOrDefaultAsync(x => x.Id == ProductId);
            if (ProductRecord is null)
            {
                return NotFound();
            }

            ProductRecord.CategoryId = ProductCategory;
            ProductRecord.ProductName = Name;
            ProductRecord.Price = _normalizer.GetNormStrRu(PriceInput);
            ProductRecord.IsHotDeal = ProductHotDeal;
            ProductRecord.DiscountPercent = ProductDiscountPercent ;
            ProductRecord.IsDiscount = ProductIsDiscount;
            _context.Update(ProductRecord);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Products/Pages/ProductIndex");
        }
//TODO: Реализовать удаление карточки товара

        private bool ProductExists(string id)
        {
            return _context.ProductRecords.Any(e => e.Id == id);
        }
    }
}