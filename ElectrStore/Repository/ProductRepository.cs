using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ElectrStore
{
    public interface IProductRepository
    {
        IQueryable<ProductRecord> GetWishProducts(string userId);
        Task<(bool success, string? errorMessage)> SaveFileAsync(ProductRecord product, IFormFile formFile);
        public string GetPic(ProductRecord product);

        public string GetFolder(ProductRecord product);

        public void DelFolderWithFiles(string path);
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

        public IQueryable<ProductRecord> GetWishProducts(string userId)
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
            return null;
        }
        /*public string GetPic(ProductRecord product)
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
        }*/
        public async Task<(bool success, string? errorMessage)> SaveFileAsync(ProductRecord product, IFormFile formFile)
        {
            return (true, null);
        }
        /*public async Task<(bool success, string? errorMessage)> SaveFileAsync(ProductRecord product, IFormFile formFile)
        {
            string? prev = product.PreviewName;
            string dir = GetDir(product);
            if (!System.IO.Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string fileName = Path.GetFileName(formFile.FileName);
            Console.WriteLine("Path.GetFileName(formFile.FileName)");
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = "picture.png";
            string path = Path.Combine(dir, fileName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }*/
        /*await using var stream = System.IO.File.Open(path, FileMode.CreateNew);
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

        return (true, null);*/

        public void DelFolderWithFiles(string path)
        {
            
        }//DelDirectoryWithFiles
        
        /*public void DelFolderWithFiles(string path)//DelDirectoryWithFiles
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
            }*/

    }
}