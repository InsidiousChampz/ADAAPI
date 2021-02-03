using System.Collections.Generic;
using System.Threading.Tasks;
using STANDARDAPI.DTOs.Product;
using STANDARDAPI.Models;

namespace STANDARDAPI.Services.Product
{
    public interface IProductService
    {
        Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto newProduct);
        Task<ServiceResponse<GetProductGroupDto>> AddProductGroup(AddProductGroupDto newProductGroup);
        Task<ServiceResponse<GetProductDto>> UpdateProductById(int ProductId, UpdateProductDto updateProduct);
        Task<ServiceResponse<GetProductGroupDto>> UpdateProductGroupById(int ProductGroupId, UpdateProductGroupDto updateProductGroup);
        Task<ServiceResponse<GetProductDto>> DeleteProductById(int ProductId);
        Task<ServiceResponse<GetProductGroupDto>> DeleteProductGroupById(int ProductGroupId);
        Task<ServiceResponse<List<GetProductDto>>> GetAllProduct();
        Task<ServiceResponse<GetProductDto>> GetProductById(int ProductId);
        Task<ServiceResponse<List<GetProductGroupDto>>> GetAllProductGroup();
        Task<ServiceResponse<GetProductGroupDto>> GetProductGroupById(int ProductGroupId);
    }
}