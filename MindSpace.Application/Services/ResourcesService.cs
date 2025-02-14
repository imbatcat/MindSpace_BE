using AutoMapper;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Interfaces.Services;
using MindSpace.Domain.Interfaces.Specifications;

namespace MindSpace.Application.Services
{
    public class ResourcesService : IResourcesService
    {
        public Task<int> CountResourcesWithSpecAsync(ISpecification<Resource> resourceSpec)
        {
            throw new NotImplementedException();
        }

        public Task<Resource?> CreateResourceAsArticle(Resource resource, int schoolManagerId)
        {
            throw new NotImplementedException();
        }

        public Task<Resource?> CreateResourceAsPaper(Resource resource, int schoolManagerId)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceSection> CreateResourceSection(ResourceSection section)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DisableResource(Resource resource)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Resource>> GetResourceAsync(ISpecification<Resource> resourceSpec, IConfigurationProvider mappingConfiguration)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Resource>> GetResourcesAsync(ISpecification<Resource> resourceSpec, IConfigurationProvider mappingConfiguration)
        {
            throw new NotImplementedException();
        }

        public ResourceType[] GetResourceTypes()
        {
            throw new NotImplementedException();
        }
    }
}
