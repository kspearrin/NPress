using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPress.Core.Domains;
using NPress.Core.Repositories;

namespace NPress.Core.Services
{
    public class PageService : IPageService
    {
        private readonly IPageRepository m_pageRepository;

        public PageService(
            IPageRepository pageRepository)
        {
            m_pageRepository = pageRepository;
        }

        public async Task<Page> GetPageByIdAsync(string id)
        {
            return await m_pageRepository.GetByIdAsync(id);
        }

        public async Task<Page> GetPageBySlugAsync(string slug)
        {
            return await m_pageRepository.GetBySlugAsync(slug);
        }

        public async Task CreatePageAsync(Page page)
        {
            if(!string.IsNullOrWhiteSpace(page.Id))
            {
                // TODO: Error
            }

            await m_pageRepository.CreateAsync(page);
        }

        public async Task UpdatePageAsync(Page page)
        {
            if(string.IsNullOrWhiteSpace(page.Id))
            {
                // TODO: Error
            }

            page.RevisionDateTime = DateTime.UtcNow;

            await m_pageRepository.ReplaceAsync(page);
        }
    }
}
