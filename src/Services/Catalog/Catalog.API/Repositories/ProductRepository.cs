using Catalog.API.Data;
using Catalog.API.Entitties;
using MongoDB.Driver;
using System.Xml.Linq;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext??throw new ArgumentNullException(nameof(catalogContext)) ;
        }

        public async Task CreateProductAsync(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(p => p.Id, id);
            DeleteResult deleteResult = await _catalogContext
                .Products
                .DeleteOneAsync(id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public Task<Product> GetProductAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _catalogContext
                .Products
                .Find( p => p.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string categoryName)
        {
            FilterDefinition<Product> filte = Builders<Product>.Filter.Eq(P => P.Category, categoryName);
            return await _catalogContext
                .Products
                .Find(filte)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            FilterDefinition<Product> filte = Builders<Product>.Filter.Eq(P => P.Name , name);
            return await _catalogContext
                .Products
                .Find(filte)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _catalogContext
                .Products
                .Find(p => true)
                .ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var updateResult = await _catalogContext
                .Products
                .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
