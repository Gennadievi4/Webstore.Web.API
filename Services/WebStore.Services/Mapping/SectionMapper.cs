﻿using WebStore.Domain.DTO.Products;
using WebStore.Domain.Entitys;

namespace WebStore.Services.Mapping
{
    public static class SectionMapper
    {
        public static SectionDTO ToDTO(this Section Section) => Section is null
            ? null
            : new SectionDTO(
                Section.Id, 
                Section.Name, 
                Section.Order, 
                Section.ParentId);

        public static Section FromDTO(this SectionDTO SectionDTO) => SectionDTO is null
            ? null
            : new Section
            {
                Id = SectionDTO.Id,
                Name = SectionDTO.Name,
                Order = SectionDTO.Order,
                ParentId = SectionDTO.ParentId,
            };
    }
}
