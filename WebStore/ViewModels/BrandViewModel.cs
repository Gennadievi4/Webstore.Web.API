using WebStore.Domain.Entitys.Base.Interfaces;

namespace WebStore.ViewModels
{
    public class BrandViewModel : INamedEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int ProductCount { get; set; }
    }
}
