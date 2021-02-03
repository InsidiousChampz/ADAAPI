using System.Collections.Generic;
using System.Threading.Tasks;
using STANDARDAPI.DTOs.Product;
using STANDARDAPI.Models;

namespace STANDARDAPI.Services.Product
{
    public interface IProductService
    {
        //Product Group.
        Task<ServiceResponse<List<GetProductGroupDto>>> GetAllProductGroup();
        Task<ServiceResponse<GetProductGroupDto>> GetProductGroupById(int ProductGroupId);
        Task<ServiceResponse<GetProductGroupDto>> AddProductGroup(AddProductGroupDto newProductGroup);
        Task<ServiceResponse<GetProductGroupDto>> UpdateProductGroupById(int ProductGroupId, UpdateProductGroupDto updateProductGroup);
        Task<ServiceResponse<GetProductGroupDto>> DeleteProductGroupById(int ProductGroupId);

        // Product.
        Task<ServiceResponse<List<GetProductDto>>> GetAllProduct();
        Task<ServiceResponse<GetProductDto>> GetProductById(int ProductId);
        Task<ServiceResponse<GetProductDto>> AddProduct(AddProductDto newProduct);
        Task<ServiceResponse<GetProductDto>> UpdateProductById(int ProductId, UpdateProductDto updateProduct);
        Task<ServiceResponse<GetProductDto>> DeleteProductById(int ProductId);




    }
}