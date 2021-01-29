using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entitys;

namespace WebStore.Services.Mapping
{
    public static class BrandMapper
    {
        public static BrandDTO ToDTO(this Brand Brand) => Brand is null
            ? null
            : new BrandDTO(
                Brand.Id, 
                Brand.Name, 
                Brand.Order);

        public static Brand FromDTO(this BrandDTO Brand) => Brand is null
            ? null
            : new Brand
            {
                Id = Brand.id,
                Name = Brand.Name,
                Order = Brand.Order,
            };
    }
}
