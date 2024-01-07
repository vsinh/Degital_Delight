using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System;

namespace DegitalDelight.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        [HttpGet("{productId}")]
        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Products.Include(x => x.Warranty).Include(x => x.ProductType).Include(x => x.ProductDetails).Include(x => x.Comments).ThenInclude(x => x.User).Where(x => x.Id == productId && !x.IsDeleted).FirstOrDefaultAsync();
        }

        public Task<Product> GetProductById(List<int> productIds)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.Where(x => !x.IsDeleted).Include(x => x.Warranty).Include(x => x.ProductType).ToListAsync();
        }
        public async Task<List<Product>> GetProductList(int productTypeId, int minPrice, int maxPrice, string sort)
        {
            return await _context.Products.Include(x => x.ProductType)
                .Where(x =>
                x.ProductType.Id == productTypeId
                && x.Price >= minPrice
                && x.Price <= maxPrice
                && !x.IsDeleted
            ).ToListAsync();
        }

        public Task<List<Product>> GetProductsByCategory(int categoryId)
        {
            return _context.Products.Include(x => x.ProductType).Where(x => x.ProductType.Id == categoryId).ToListAsync();
        }

        public async Task<List<ProductType>> GetProductTypes()
        {
            return await _context.ProductTypes.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<List<Product>> GetRelatedProduct(Product product)
        {
            Random random = new Random();
            var listProduct = await _context.Products.Where(x => x.Id != product.Id && x.ProductType.Id == product.ProductType.Id).OrderBy(Id => random.NextDouble()).Take(8).ToListAsync();
            return listProduct;
        }
    }
}
