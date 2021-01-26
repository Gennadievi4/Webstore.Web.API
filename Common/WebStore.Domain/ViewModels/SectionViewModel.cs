using System.Collections.Generic;
using WebStore.Domain.Entitys.Base.Interfaces;

namespace WebStore.Domain.ViewModels
{
    public class SectionViewModel : INamedEntity, IOrderedEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int Order { get; set; }

        public List<SectionViewModel> ChildItems { get; set; } = new List<SectionViewModel>();
        public SectionViewModel ParentSection { get; set; }

        public int ProductCount { get; set; }
    }
}