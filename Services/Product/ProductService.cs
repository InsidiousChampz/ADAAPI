using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TEMPLETEAPI.Data;
using TEMPLETEAPI.DTOs.Product;
using TEMPLETEAPI.Models;

namespace TEMPLETEAPI.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _log;

        public ProductService(AppDBContext dBContext, IMapper mapper, ILogger<ProductService> log)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
        }
        public async Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto newProduct)
        {
            var _product = new TEMPLETEAPI.Models.Product.Product
            {
                Name = newProduct.Name,
                Price = newProduct.Price,
                StockCount = newProduct.StockCount,
                ProductGroupId = newProduct.ProductGroupId

            };

            _dbContext.Products.Add(_product);
            await _dbContext.SaveChangesAsync();
            var dto = _mapper.Map<GetProductDto>(_product);
            return ResponseResult.Success(dto);
        }
        public async Task<ServiceResponse<GetProductDto>> UpdateProductById(int ProductId, UpdateProductDto updateProduct)
        {
            var _Product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == ProductId);
            if (_Product == null)
            {
                return ResponseResult.Failure<GetProductDto>("Product Not Found.");
            }
            else
            {

                _Product.Name = updateProduct.Name;
                _Product.Price = updateProduct.Price;
                _Product.StockCount = updateProduct.StockCount;
                _Product.ProductGroupId = updateProduct.ProductGroupId;

                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetProductDto>(_Product);
                return ResponseResult.Success(dto);
            }
        }
        public async Task<ServiceResponse<GetProductDto>> DeleteProductById(int ProductId)
        {
            var _Product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == ProductId);
            if (_Product == null)
            {
                return ResponseResult.Failure<GetProductDto>("Product Not Found.");
            }
            else
            {
                _dbContext.Products.Remove(_Product);
                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetProductDto>(_Product);
                return ResponseResult.Success(dto);
            }
        }
        public async Task<ServiceResponse<List<GetProductDto>>> GetAllProduct()
        {
            var _product = await _dbContext.Products.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<GetProductDto>>(_product);
            return ResponseResult.Success(dto);
        }
        public async Task<ServiceResponse<GetProductDto>> GetProductById(int ProductId)
        {
            var _product = await _dbContext.Products
           .FirstOrDefaultAsync(x => x.Id == ProductId);
            if (_product == null)
            {
                return ResponseResult.Failure<GetProductDto>("Product Not Found.");
            }
            else
            {
                var dto = _mapper.Map<GetProductDto>(_product);
                return ResponseResult.Success(dto);
            }
        }
        public async Task<ServiceResponse<List<GetProductGroupDto>>> GetAllProductGroup()
        {
            var _productGroup = await _dbContext.ProductGroups.AsNoTracking().ToListAsync();
            var dto = _mapper.Map<List<GetProductGroupDto>>(_productGroup);
            return ResponseResult.Success(dto);
        }
        public async Task<ServiceResponse<GetProductGroupDto>> GetProductGroupById(int ProductGroupId)
        {
            var _productGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Id == ProductGroupId);
            if (_productGroup == null)
            {
                return ResponseResult.Failure<GetProductGroupDto>("ProductGroup Not Found.");
            }
            else
            {
                var dto = _mapper.Map<GetProductGroupDto>(_productGroup);
                return ResponseResult.Success(dto);
            }
        }
        public async Task<ServiceResponse<GetProductGroupDto>> AddProductGroup(AddProductGroupDto newProductGroup)
        {
            var _productgroup = new TEMPLETEAPI.Models.Product.ProductGroup
            {
                Name = newProductGroup.Name,
            };

            _dbContext.ProductGroups.Add(_productgroup);
            await _dbContext.SaveChangesAsync();
            var dto = _mapper.Map<GetProductGroupDto>(_productgroup);
            return ResponseResult.Success(dto);
        }
        public async Task<ServiceResponse<GetProductGroupDto>> UpdateProductGroupById(int ProductGroupId, UpdateProductGroupDto updateProductGroup)
        {
            var _ProductGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Id == ProductGroupId);
            if (_ProductGroup == null)
            {
                return ResponseResult.Failure<GetProductGroupDto>("Product Group Not Found.");
            }
            else
            {
                _ProductGroup.Name = updateProductGroup.Name;

                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetProductGroupDto>(_ProductGroup);
                return ResponseResult.Success(dto);
            }
        }
        public async Task<ServiceResponse<GetProductGroupDto>> DeleteProductGroupById(int ProductGroupId)
        {
            var _ProductGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Id == ProductGroupId);
            if (_ProductGroup == null)
            {
                return ResponseResult.Failure<GetProductGroupDto>("Product Group Not Found.");
            }
            else
            {
                _dbContext.ProductGroups.Remove(_ProductGroup);
                await _dbContext.SaveChangesAsync();
                var dto = _mapper.Map<GetProductGroupDto>(_ProductGroup);
                return ResponseResult.Success(dto);
            }
        }
    }
}