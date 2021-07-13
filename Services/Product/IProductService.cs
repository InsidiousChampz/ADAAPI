using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerProFileAPI.DTOs.Product;
using CustomerProFileAPI.Models;
using CustomerProFileAPI.Models.Product;

namespace CustomerProFileAPI.Services.Product
{
    public interface IProductService
    {

        //Product Group.
        Task<ServiceResponse<List<GetProductGroupDto>>> GetAllProductGroup();
        Task<ServiceResponse<GetProductGroupDto>> GetProductGroupById(int ProductGroupId);
        Task<ServiceResponseWithPagination<List<Models.Product.ProductGroup>>> GetProductGroupWithFilter(GetProductGroupFilterDto ProductGroupFilter);
        Task<ServiceResponse<GetProductGroupDto>> AddProductGroup(AddProductGroupDto newProductGroup);
        Task<ServiceResponse<GetProductGroupDto>> UpdateProductGroupById(int ProductGroupId, UpdateProductGroupDto updateProductGroup);
        Task<ServiceResponse<GetProductGroupDto>> DeleteProductGroupById(int ProductGroupId);

        // Product.
        Task<ServiceResponse<List<GetProductDto>>> GetAllProduct();
        Task<ServiceResponse<List<GetProductDto>>> GetProductByGroupId(int ProductGroupId);
        Task<ServiceResponse<GetProductDto>> GetProductById(int ProductId);
        Task<ServiceResponseWithPagination<List<Models.Product.Product>>> GetProductWithFilter(GetProductFilterDto ProductFilter);
        Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto newProduct);
        Task<ServiceResponse<GetProductDto>> UpdateProductById(int ProductId, UpdateProductDto updateProduct);
        Task<ServiceResponse<GetProductDto>> DeleteProductById(int ProductId);

        //Product Audit.
        Task<ServiceResponse<List<GetProductAuditDto>>> GetAllProductAudit();
        Task<ServiceResponse<GetProductAuditDto>> GetProductAuditById(int ProductAuditId);
        Task<ServiceResponseWithPagination<List<ProductAudit>>> GetProductAuditWithFilter(GetProductAuditFilterDto ProductAuditFilter);
        Task<ServiceResponse<GetProductAuditDto>> AddProductAudit(AddProductAuditDto newProductAudit);

        //Product Audit Type.
        Task<ServiceResponse<List<GetProductAuditTypeDto>>> GetAllAuditType();




    }
}