using System;
using System.Collections.Generic;
using System.Linq;
using EPiServer.DataAbstraction.RuntimeModel;
using EPiServer.ServiceLocation;
using PageTypeBuilder.Discovery;
using PageTypeBuilder.Reflection;

namespace PageTypeBuilder
{
    /// <summary>
    /// Filters the PTB page types from the CMS discovery process, to make sure a page type is either owned by PTB or CMS
    /// </summary>
    [ServiceConfiguration(typeof(IContentTypeModelFilter))]
    public class ContentModelFilter : IContentTypeModelFilter
    {
        /// <summary>
        /// Default imlementation of <see cref="IContentTypeModelFilter"/>. this implementation remove all the pageTypedefinitions.types from a list of types
        /// </summary>
        public IList<Type> Filter(IList<Type> types)
        {
            var definitions = new PageTypeDefinitionLocator(new AppDomainAssemblyLocator()).GetPageTypeDefinitions();

            foreach (Type t in definitions.Select(d => d.Type))
            {
                types.Remove(t);            
            }

            return types;
        }
    }
}
