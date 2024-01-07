using DegitalDelight.Areas.Admin.Services.Interfaces;
using DegitalDelight.Data;
using DegitalDelight.Models;
using DegitalDelight.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using System;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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
            return await _context.Products.Include(x => x.ProductType).Include(x => x.ProductDetails).Include(x => x.Comments).ThenInclude(x => x.User).Where(x => x.Id == productId && !x.IsDeleted).FirstOrDefaultAsync();
        }

        public Task<Product> GetProductById(List<int> productIds)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.Where(x => !x.IsDeleted).ToListAsync();
        }
        public async Task<List<Product>> GetSomeProducts()
        {
            var products = await _context.Products.Where(x => !x.IsDeleted).ToListAsync();
            var random = new Random();
            products = products.OrderBy(Id => random.NextDouble()).Take(8).ToList();
            return products;
        }
        public async Task<List<Product>> GetNewProducts()
        {
            var products = await _context.Products.Where(x => !x.IsDeleted).ToListAsync();
            products = products.OrderByDescending(x => x.CreatedDate).Take(4).ToList();
            return products;
        }
        public async Task<List<Product>> GetHotProducts()
        {
            var products = await _context.Products.Where(x => !x.IsDeleted).ToListAsync();
            products = products.OrderByDescending(x => x.Stock).Take(8).ToList();
            return products;
        }
        public async Task<List<Product>> GetProductList(int productTypeId, int minPrice, int maxPrice, string sort)
        {
            var products = await _context.Products.Include(x => x.ProductType)
                .Where(x =>
                x.ProductType.Id == productTypeId
                && x.Price >= minPrice
                && x.Price <= maxPrice
                && !x.IsDeleted
                ).ToListAsync();
            if (sort == "Latest")
            {
                products = products.OrderBy(x => x.CreatedDate).ToList();
            }
            if (sort == "Oldest")
            {
                products = products.OrderByDescending(x => x.CreatedDate).ToList();
            }
            if (sort == "PriceAsc")
            {
                products = products.OrderBy(x => x.Price).ToList();
            }
            if (sort == "PriceDesc")
            {
                products = products.OrderByDescending(x => x.Price).ToList();
            }
            return products;
        }

        public async Task<List<Product>> Search(string input)
        {
            var products = await _context.Products.Include(x => x.ProductType)
                .Where(x => (x.Name.Contains(input)
                || x.ProductType.Name.Contains(input)
                || x.Brand.Contains(input))
                && !x.IsDeleted
                ).ToListAsync();
            return products;
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

        public async Task<int> GetProductDiscount(int productId)
        {
            var product = await _context.Products.Include(x => x.Discounts).Where(x => x.Id == productId && !x.IsDeleted).FirstOrDefaultAsync();
            int maxAmount = 0;
            foreach (var discount in product.Discounts)
            {
                if (discount.Amount > maxAmount)
                {
                    if (discount.Amount > 100)
                    {
                        discount.Amount = discount.Amount / product.Price * 100;
                    }
                    maxAmount = discount.Amount;
                }
            }
            return maxAmount;
        }

        public async Task<int> GetProductPriceAfterDiscount(int productId)
        {
            var product = await _context.Products.Include(x => x.Discounts).Where(x => x.Id == productId && !x.IsDeleted).FirstOrDefaultAsync();
            int maxAmount = 0;
            foreach (var discount in product.Discounts)
            {
                if (discount.Amount < 100)
                {
                    discount.Amount = product.Price * discount.Amount / 100;
                    if (discount.Amount > maxAmount)
                    {
                        maxAmount = discount.Amount;
                    }
                }
            }
            return product.Price - maxAmount;
        }
    }
}
