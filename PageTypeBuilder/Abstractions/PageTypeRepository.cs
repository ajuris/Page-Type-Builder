using System.Collections.Generic;
using System.Linq;
using EPiServer.DataAbstraction;

namespace PageTypeBuilder.Abstractions
{
    public class PageTypeRepository : IPageTypeRepository
    {
        //private EPiServer.DataAbstraction.IContentTypeRepository _contentTypeRepository;
        //public PageTypeRepository()
        //{
        //    _contentTypeRepository = EPiServer.ServiceLocation.ServiceLocator.Current.GetInstance<IContentTypeRepository>();
        //}

        public virtual IPageType Load(string name)
        {
            var pageType = PageType.Load(name);
            //var pageType = (PageType)_contentTypeRepository.Load(name).CreateWritableClone();
            if (pageType == null)
                return null;
            return new WrappedPageType(pageType);
        }

        public virtual IPageType Load(System.Guid guid)
        {
            var pageType = PageType.Load(guid);
            //var pageType = (PageType)_contentTypeRepository.Load(guid).CreateWritableClone();
            if (pageType == null)
                return null;
            return new WrappedPageType(pageType);
        }

        public virtual IPageType Load(int id)
        {
            var pageType = PageType.Load(id);
            //var pageType = (PageType)_contentTypeRepository.Load(id).CreateWritableClone();
            if (pageType == null)
                return null;
            return new WrappedPageType(pageType);
        }

        public virtual IEnumerable<IPageType> List()
        {
            return PageType.List().Select(pageType => new WrappedPageType(pageType)).Cast<IPageType>();
            //return _contentTypeRepository.List().Select(pageType => new WrappedPageType((PageType)pageType)).Cast<IPageType>();
        }

        public virtual void Save(IPageType pageTypeToSave)
        {
            pageTypeToSave.Save();
        }

        public virtual IPageType CreateNew()
        {
            return new NativePageType();
        }

        public void Delete(IPageType pageType)
        {
            pageType.Delete();
        }
    }
}