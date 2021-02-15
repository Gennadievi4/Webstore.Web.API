using System.Collections.Generic;
using WebStore.Domain.ViewModels;

namespace WebStore.ViewModels
{
    public record SelectableSectionViewModel(IEnumerable<SectionViewModel> Sections, int? SectionId, int? ParentSectionId);
}
