﻿using EPiServer.DataAbstraction;

namespace PageTypeBuilder.Abstractions
{
    public interface IPageDefinitionRepository
    {
        void Save(PageDefinition pageDefinition);
        PageDefinitionCollection List(int pageTypeId);
        void Delete(PageDefinition pageDefinition);
        PageDefinition Load(int id);
    }
}
