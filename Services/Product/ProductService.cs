using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmsUpdateCustomer_Api.Data;
using SmsUpdateCustomer_Api.DTOs.Product;
using SmsUpdateCustomer_Api.Helpers;
using SmsUpdateCustomer_Api.Models;
using mProduct = SmsUpdateCustomer_Api.Models.Product.Product;
using mProductGroup = SmsUpdateCustomer_Api.Models.Product.ProductGroup;
using mProductAudit = SmsUpdateCustomer_Api.Models.Product.ProductAudit;
using System.Linq.Dynamic.Core;

namespace SmsUpdateCustomer_Api.Services.Product
{
    public class ProductService : ServiceBase, IProductService
    {
        private readonly AppDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductService> _log;
        private readonly IHttpContextAccessor _httpcontext;


        public ProductService(AppDBContext dBContext, IMapper mapper, ILogger<ProductService> log, IHttpContextAccessor httpcontext) : base(dBContext, mapper, httpcontext)
        {
            _dbContext = dBContext;
            _mapper = mapper;
            _log = log;
            _httpcontext = httpcontext;
        }

        //Product Group
        //public async Task<ServiceResponse<List<GetProductGroupDto>>> GetAllProductGroup()
        //{
        //    var _productGroup = await _dbContext.ProductGroups
        //    .Include(x => x.Products)
        //    .AsNoTracking().ToListAsync();
        //    var dto = _mapper.Map<List<GetProductGroupDto>>(_productGroup);
        //    return ResponseResult.Success(dto);
        //}
        //public async Task<ServiceResponse<GetProductGroupDto>> GetProductGroupById(int ProductGroupId)

        //{
        //    var _productGroup = await _dbContext.ProductGroups
        //    .Include(x => x.Products)
        //    .FirstOrDefaultAsync(x => x.Id == ProductGroupId);
        //    if (_productGroup == null)
        //    {
        //        return ResponseResult.Failure<GetProductGroupDto>("ProductGroup Not Found.");
        //    }
        //    else
        //    {
        //        var dto = _mapper.Map<GetProductGroupDto>(_productGroup);
        //        return ResponseResult.Success(dto);
        //    }
        //}
        //public async Task<ServiceResponseWithPagination<List<mProductGroup>>> GetProductGroupWithFilter(GetProductGroupFilterDto ProductGroupFilter)
        //{
        //    var Queryable = _dbContext.ProductGroups
        //    .Include(x => x.Products)
        //    .AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(ProductGroupFilter.Name))
        //    {
        //        Queryable = Queryable.Where(x => x.Name.Contains(ProductGroupFilter.Name));
        //    }

        //    if (!string.IsNullOrWhiteSpace(ProductGroupFilter.CreateBy))
        //    {
        //        Queryable = Queryable.Where(x => x.CreateBy.Contains(ProductGroupFilter.CreateBy));
        //    }

        //    if (ProductGroupFilter.Status != null)
        //    {
        //        Queryable = Queryable.Where(x => x.Status == ProductGroupFilter.Status);
        //    }


        //    if (!string.IsNullOrWhiteSpace(ProductGroupFilter.OrderingField))
        //    {
        //        try
        //        {
        //            Queryable = Queryable.OrderBy($"{ProductGroupFilter.OrderingField} {(ProductGroupFilter.AscendingOrder ? "ascending" : "descending")}");
        //        }
        //        catch
        //        {
        //            return ResponseResultWithPagination.Failure<List<Models.Product.ProductGroup>>($"Could not order by field: {ProductGroupFilter.OrderingField}");
        //        }

        //      ;
        //    }

        //    var paginationResult = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(Queryable, ProductGroupFilter.RecordsPerPage, ProductGroupFilter.Page);
        //    var dto = await Queryable.Paginate(ProductGroupFilter).ToListAsync();
        //    return ResponseResultWithPagination.Success(dto, paginationResult);
        //}
        //public async Task<ServiceResponse<GetProductGroupDto>> AddProductGroup(AddProductGroupDto newProductGroup)
        //{
        //    var _productgroup = new mProductGroup
        //    {
        //        Name = newProductGroup.Name,
        //        CreateBy = GetUsername(),
        //        CreateDate = DateTime.Now,
        //        Status = true
        //    };

        //    _dbContext.ProductGroups.Add(_productgroup);
        //    await _dbContext.SaveChangesAsync();
        //    var dto = _mapper.Map<GetProductGroupDto>(_productgroup);
        //    return ResponseResult.Success(dto);
        //}
        //public async Task<ServiceResponse<GetProductGroupDto>> UpdateProductGroupById(int ProductGroupId, UpdateProductGroupDto updateProductGroup)
        //{
        //    var _ProductGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Id == ProductGroupId);
        //    if (_ProductGroup == null)
        //    {
        //        return ResponseResult.Failure<GetProductGroupDto>("Product Group Not Found.");
        //    }
        //    else
        //    {
        //        _ProductGroup.Name = updateProductGroup.Name;
        //        _ProductGroup.CreateBy = GetUsername();           //updateProductGroup.CreateBy;
        //        _ProductGroup.CreateDate = Now();                        //updateProductGroup.CreateDate;
        //        _ProductGroup.Status = updateProductGroup.Status;


        //        await _dbContext.SaveChangesAsync();
        //        var dto = _mapper.Map<GetProductGroupDto>(_ProductGroup);
        //        return ResponseResult.Success(dto);
        //    }
        //}
        //public async Task<ServiceResponse<GetProductGroupDto>> DeleteProductGroupById(int ProductGroupId)
        //{
        //    var _ProductGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Id == ProductGroupId);
        //    if (_ProductGroup == null)
        //    {
        //        return ResponseResult.Failure<GetProductGroupDto>("Product Group Not Found.");
        //    }
        //    else
        //    {
        //        _dbContext.ProductGroups.Remove(_ProductGroup);
        //        await _dbContext.SaveChangesAsync();
        //        var dto = _mapper.Map<GetProductGroupDto>(_ProductGroup);
        //        return ResponseResult.Success(dto);
        //    }
        //}

        ////Product
        //public async Task<ServiceResponse<List<GetProductDto>>> GetAllProduct()
        //{
        //    var _product = await _dbContext.Products.AsNoTracking()
        //    .Include(x => x.ProductGroup)
        //    .ToListAsync();
        //    var dto = _mapper.Map<List<GetProductDto>>(_product);
        //    return ResponseResult.Success(dto);
        //}
        //public async Task<ServiceResponse<List<GetProductDto>>> GetProductByGroupId(int ProductGroupId)
        //{
        //    var _product = await _dbContext.Products
        //   .Where(x => x.ProductGroupId == ProductGroupId)
        //   .ToListAsync();
        //    var dto = _mapper.Map<List<GetProductDto>>(_product);
        //    return ResponseResult.Success(dto);
        //}
        //public async Task<ServiceResponse<GetProductDto>> GetProductById(int ProductId)
        //{
        //    var _product = await _dbContext.Products
        //    .Include(x => x.ProductGroup)
        //   .FirstOrDefaultAsync(x => x.Id == ProductId);
        //    if (_product == null)
        //    {
        //        return ResponseResult.Failure<GetProductDto>("Product Not Found.");
        //    }
        //    else
        //    {
        //        var dto = _mapper.Map<GetProductDto>(_product);
        //        return ResponseResult.Success(dto);
        //    }
        //}
        //public async Task<ServiceResponseWithPagination<List<mProduct>>> GetProductWithFilter(GetProductFilterDto ProductFilter)
        //{
        //    var Queryable = _dbContext.Products
        //    .Include(x => x.ProductGroup)
        //    .AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(ProductFilter.Name))
        //    {
        //        Queryable = Queryable.Where(x => x.Name.Contains(ProductFilter.Name));
        //    }

        //    if (ProductFilter.Price != null)
        //    {
        //        Queryable = Queryable.Where(x => x.Price == ProductFilter.Price);
        //    }

        //    if (ProductFilter.StockCount != null)
        //    {
        //        Queryable = Queryable.Where(x => x.StockCount == ProductFilter.StockCount);
        //    }


        //    if (ProductFilter.ProductGroupId != null)
        //    {
        //        Queryable = Queryable.Where(x => x.ProductGroupId == ProductFilter.ProductGroupId);
        //    }

        //    if (!string.IsNullOrWhiteSpace(ProductFilter.OrderingField))
        //    {
        //        try
        //        {
        //            Queryable = Queryable.OrderBy($"{ProductFilter.OrderingField} {(ProductFilter.AscendingOrder ? "ascending" : "descending")}");
        //        }
        //        catch
        //        {
        //            return ResponseResultWithPagination.Failure<List<Models.Product.Product>>($"Could not order by field: {ProductFilter.OrderingField}");
        //        }

        //      ;
        //    }

        //    var paginationResult = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(Queryable, ProductFilter.RecordsPerPage, ProductFilter.Page);
        //    var dto = await Queryable.Paginate(ProductFilter).ToListAsync();
        //    return ResponseResultWithPagination.Success(dto, paginationResult);
        //}
        //public async Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto newProduct)
        //{
        //    var productGroup = await _dbContext.ProductGroups.FirstOrDefaultAsync(x => x.Id == newProduct.ProductGroupId);
        //    if (productGroup == null)
        //    {
        //        return ResponseResult.Failure<GetProductDto>("Product Group Not Found.");
        //    }

        //    var _product = new mProduct
        //    {
        //        Name = newProduct.Name,
        //        Price = newProduct.Price,
        //        StockCount = newProduct.StockCount,
        //        CreateBy = GetUsername(),
        //        CreateDate = Now(),
        //        Status = true,
        //        ProductGroupId = newProduct.ProductGroupId,
        //        ProductGroup = productGroup
        //    };

        //    _dbContext.Products.Add(_product);
        //    await _dbContext.SaveChangesAsync();
        //    var dto = _mapper.Map<GetProductDto>(_product);
        //    return ResponseResult.Success(dto);
        //}
        //public async Task<ServiceResponse<GetProductDto>> UpdateProductById(int ProductId, UpdateProductDto updateProduct)
        //{
        //    var _Product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == ProductId);
        //    if (_Product == null)
        //    {
        //        return ResponseResult.Failure<GetProductDto>("Product Not Found.");
        //    }
        //    else
        //    {

        //        _Product.Name = updateProduct.Name;
        //        _Product.Price = updateProduct.Price;
        //        _Product.StockCount = updateProduct.StockCount;
        //        _Product.CreateBy = GetUsername();           //updateProduct.CreateBy;
        //        _Product.CreateDate = DateTime.Now;    //updateProduct.CreateDate;
        //        _Product.Status = true;
        //        _Product.ProductGroupId = updateProduct.ProductGroupId;

        //        await _dbContext.SaveChangesAsync();
        //        var dto = _mapper.Map<GetProductDto>(_Product);
        //        return ResponseResult.Success(dto);
        //    }
        //}
        //public async Task<ServiceResponse<GetProductDto>> DeleteProductById(int ProductId)
        //{
        //    var _Product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == ProductId);
        //    if (_Product == null)
        //    {
        //        return ResponseResult.Failure<GetProductDto>("Product Not Found.");
        //    }
        //    else
        //    {
        //        _dbContext.Products.Remove(_Product);
        //        await _dbContext.SaveChangesAsync();
        //        var dto = _mapper.Map<GetProductDto>(_Product);
        //        return ResponseResult.Success(dto);
        //    }
        //}

        ////Product Audit
        //public async Task<ServiceResponse<List<GetProductAuditDto>>> GetAllProductAudit()
        //{
        //    var _productAudit = await _dbContext.ProductAudits.AsNoTracking().ToListAsync();
        //    var dto = _mapper.Map<List<GetProductAuditDto>>(_productAudit);
        //    return ResponseResult.Success(dto);
        //}
        //public async Task<ServiceResponse<GetProductAuditDto>> GetProductAuditById(int ProductAuditId)
        //{
        //    var _productAudit = await _dbContext.ProductAudits
        //    .Include(x => x.ProductAuditTypeId)
        //   .FirstOrDefaultAsync(x => x.Id == ProductAuditId);
        //    if (_productAudit == null)
        //    {
        //        return ResponseResult.Failure<GetProductAuditDto>("Transection this Audit Not Found.");
        //    }
        //    else
        //    {
        //        var dto = _mapper.Map<GetProductAuditDto>(_productAudit);
        //        return ResponseResult.Success(dto);
        //    }
        //}
        //public async Task<ServiceResponseWithPagination<List<mProductAudit>>> GetProductAuditWithFilter(GetProductAuditFilterDto ProductAuditFilter)
        //{
        //    var Queryable = _dbContext.ProductAudits
        //   .Include(x => x.ProductAuditType)
        //   .Include(x => x.ProductGroup)
        //   .Include(x => x.Product)
        //   .AsQueryable();

        //    if (!string.IsNullOrWhiteSpace(ProductAuditFilter.Name))
        //    {
        //        Queryable = Queryable.Where(x => x.Name.Contains(ProductAuditFilter.Name));
        //    }

        //    if (ProductAuditFilter.Remark != null)
        //    {
        //        Queryable = Queryable.Where(x => x.Remark == ProductAuditFilter.Remark);
        //    }

        //    if (ProductAuditFilter.ProductGroupId != null)
        //    {
        //        Queryable = Queryable.Where(x => x.ProductGroupId == ProductAuditFilter.ProductGroupId);
        //    }

        //    if (!string.IsNullOrWhiteSpace(ProductAuditFilter.OrderingField))
        //    {
        //        try
        //        {
        //            Queryable = Queryable.OrderBy($"{ProductAuditFilter.OrderingField} {(ProductAuditFilter.AscendingOrder ? "ascending" : "descending")}");
        //        }
        //        catch
        //        {
        //            return ResponseResultWithPagination.Failure<List<mProductAudit>>($"Could not order by field: {ProductAuditFilter.OrderingField}");
        //        }

        //      ;
        //    }

        //    var paginationResult = await _httpcontext.HttpContext.InsertPaginationParametersInResponse(Queryable, ProductAuditFilter.RecordsPerPage, ProductAuditFilter.Page);
        //    var dto = await Queryable.Paginate(ProductAuditFilter).ToListAsync();
        //    return ResponseResultWithPagination.Success(dto, paginationResult);
        //}
        //public async Task<ServiceResponse<GetProductAuditDto>> AddProductAudit(AddProductAuditDto newProductAudit)
        //{
        //    var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == newProductAudit.ProductId);
        //    if (product == null)
        //    {
        //        return ResponseResult.Failure<GetProductAuditDto>("Product Not Found.");
        //    }

        //    var productAudit = new mProductAudit
        //    {
        //        Name = newProductAudit.Name,
        //        StockCount = newProductAudit.StockCount,
        //        AuditAmount = newProductAudit.AuditAmount,
        //        AuditTotalAmount = newProductAudit.AuditTotalAmount,
        //        Remark = newProductAudit.Remark,
        //        CreateBy = GetUsername(),
        //        CreateDate = Now(),
        //        ProductGroupId = newProductAudit.ProductGroupId,
        //        ProductId = newProductAudit.ProductId,
        //        ProductAuditTypeId = newProductAudit.ProductAuditTypeId,
        //    };

        //    _dbContext.ProductAudits.Add(productAudit);
        //    await _dbContext.SaveChangesAsync();
        //    var dto = _mapper.Map<GetProductAuditDto>(productAudit);
        //    return ResponseResult.Success(dto);
        //}
        //public async Task<ServiceResponse<List<GetProductAuditTypeDto>>> GetAllAuditType()
        //{
        //    var _productAuditType = await _dbContext.ProductAuditTypes.AsNoTracking().ToListAsync();
        //    var dto = _mapper.Map<List<GetProductAuditTypeDto>>(_productAuditType);
        //    return ResponseResult.Success(dto);
        //}
    }
}