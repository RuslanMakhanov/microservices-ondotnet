﻿using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories 
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
    {
        public ICatalogContext _context { get; }
        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }
        async Task<Product> IProductRepository.GetProduct(string id)
        {
            return await _context
                .Products
                .Find(p=>p.Id == id)
                .FirstOrDefaultAsync();
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProducts()
        {
            return await _context
                .Products
                .Find(p => true)
                .ToListAsync();
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductsByBrand(string brandName)
        {
            return await _context
                .Products
                .Find(p=>p.Brands.Name.ToLower()  == brandName.ToLower())
                .ToListAsync();
        }

        async Task<IEnumerable<Product>> IProductRepository.GetProductsByName(string name)
        {
            return await _context
                .Products
                .Find(p=>p.Name.ToLower() == name.ToLower())
                .ToListAsync();
        }

        async Task<Product> IProductRepository.CreateProduct(Product product)
        {
            await _context.Products.InsertOneAsync(product);
            return product;
        }

        async Task<bool> IProductRepository.DeleteProduct(string id)
        {
            var deletedProduct = await _context
                .Products
                .DeleteOneAsync(p => p.Id == id);
            return deletedProduct.IsAcknowledged;
        }

        Task<IEnumerable<ProductBrand>> IBrandRepository.GetAllBrands()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Type>> ITypesRepository.GetAllTypes()
        {
            throw new NotImplementedException();
        }       

        Task<bool> IProductRepository.UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
