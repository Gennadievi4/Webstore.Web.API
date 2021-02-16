using System.Collections.Generic;

namespace WebStore.Domain.DTO.Products
{
    public record PageProductsDTO(IEnumerable<ProductDTO> Products, int TotalCount);
}
