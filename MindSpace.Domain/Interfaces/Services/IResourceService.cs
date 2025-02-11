using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.SupportingPrograms;
using MindSpace.Domain.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Domain.Interfaces.Services
{
    public interface IResourceService
    {
        public Task<IReadOnlyList<Resource>> GetResourcesAsync(ISpecification<Resource> resourceSpec);
        public Task<int> CountResourcesWithSpecAsync(ISpecification<Resource> resourceSpec);

        public Task<IReadOnlyList<ResourceType>> GetResourceTypes();
        public Task<Resource?> CreateResourceAsArticle(Resource resource);
        public Task<Resource?> CreateResourceAsPaper(Resource resource);
        public Task<Resource?> CreateResourceAsBlog(string blogDraftId);
    }
}
