
namespace ElectrStore
{
    public interface IProductsRepository
    {
        IQueryable<ProductRecord> GetWishProducts(string userId);
        Task<(bool success, string? errorMessage)> SaveFileAsync(ProductRecord product, IFormFile formFile);
        public string GetPic(ProductRecord product);

        public string GetFolder(ProductRecord product);

        public void DelFolderWithFiles(string path);
    }

    public class ProductsRepository : IProductsRepository
    {
        public ProductsRepository(IConfiguration configuration, StoreContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
        }

        private IConfiguration _configuration { get; init; }
        private StoreContext _dbContext { get; init; }

        private string GetDir(ProductRecord product)
        {
            return Path.Combine(_configuration.GetSection("Storage").Value, product.Id);
        }

        public IQueryable<ProductRecord> GetWishProducts(string userId)
        {
            /*var productIds = _dbContext
                .WishList
                .Where(x => x.UserId == userId)
                .Select(x => x.ProductId);
            var products = _dbContext
                .Product
                .Where(p => productIds.Contains(p.Id))
                ;
            return products;*/
            return null;
        }

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
            if (product.OnPreview)
            {
                product.PreviewName = fileName;
            }
            else
            {
                product.PreviewName = prev;
            }

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
    }
}