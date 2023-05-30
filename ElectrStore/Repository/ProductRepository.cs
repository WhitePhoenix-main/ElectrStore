namespace ElectrStore
{
    public interface IProductRepository
    {
        IQueryable<ProductRecord>? GetWishProducts(string userId);
        Task<(bool success, string? errorMessage)> SaveFileAsync(ProductRecord product, IFormFile formFile);
        public string GetPic(ProductRecord product);

        public string GetFolder(ProductRecord product);

        public void DelFolderWithFiles(string path);
        public string GetNormalizedProductPrice(double price);
    }

    public class ProductRepository : IProductRepository
    {
        public ProductRepository(IConfiguration configuration, StoreContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        private IConfiguration _configuration { get; init; }
        private StoreContext _dbContext { get; init; }

        private string GetDir(ProductRecord product)
        {
            return Path.Combine(_configuration.GetSection("Storage").Value, product.Id.ToString());
        }

        public IQueryable<ProductRecord>? GetWishProducts(string userId)
        {
            return null;
        }
        /*public IQueryable<ProductRecord> GetWishProducts(string userId)
        {
            var productIds = _dbContext
                .WishList
                .Where(x => x.UserId == userId)
                .Select(x => x.ProductId);
            var products = _dbContext
                .Product
                .Where(p => productIds.Contains(p.Id))
                ;
            return products;
        }*/

        public string GetFolder(ProductRecord product)
        {
            return GetDir(product);
        }
        
        public string GetPic(ProductRecord product)
        {
            if (product.PreviewName is null)
            {
                return _configuration.GetSection("DefaultPic").Value;
            }
            else
            {
                var path = Path.Combine(GetDir(product), product.PreviewName);
                return path.Substring(path.IndexOf("\\pics"));
            }
        }

        public async Task<(bool success, string? errorMessage)> SaveFileAsync(ProductRecord product, IFormFile formFile)
        {
            var prev = product.PreviewName;
            var dir = GetDir(product);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            var fileName = Path.GetFileName(formFile.FileName);
            Console.WriteLine("Path.GetFileName(formFile.FileName)");
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = "picture.png";
            var path = Path.Combine(dir, fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            await using var stream = File.Open(path, FileMode.CreateNew);
            await formFile.CopyToAsync(stream);
            stream.Close();
            product.PreviewName = product.OnPreview ? fileName : prev;
            return (true, null);
        }

        public void DelFolderWithFiles(string path)//DelDirectoryWithFiles
            {
                if (Directory.Exists(path))
                {
                    var files = Directory.GetFiles(path);
                    foreach (var file in files)
                    {
                        File.Delete(Path.Combine(path, file));
                    }
                    var directories = Directory.GetDirectories(path);
                    foreach (var dirPath in directories)
                    {
                        DelFolderWithFiles(dirPath);
                    }
                    Directory.Delete(path);
                }
            }

        public string GetNormalizedProductPrice(double price)
            //TODO: В зависимости от локали
        {
            var normPrice = price.ToString();
            var result = "";
            if (normPrice.EndsWith("00"))
            {
                result = normPrice.Substring(0, normPrice.Length - 2) + "," + normPrice.Substring(normPrice.Length - 2);
            }

            return result;
        }

    }
}