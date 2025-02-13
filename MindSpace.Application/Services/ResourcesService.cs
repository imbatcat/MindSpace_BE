using AutoMapper;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Drafts.Blogs;
using MindSpace.Domain.Entities.Resources;
using MindSpace.Domain.Interfaces.Repos;
using MindSpace.Domain.Interfaces.Services;
using MindSpace.Domain.Interfaces.Specifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Application.Services
{
    internal class ResourcesService : IResourcesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ResourcesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> CountResourcesWithSpecAsync(ISpecification<Resource> resourceSpec)
        {
            return _unitOfWork.Repository<Resource>().CountAsync(resourceSpec);
        }

        public Task<IReadOnlyList<Resource>> GetResourcesAsync(ISpecification<Resource> resourceSpec, IConfigurationProvider mappingConfig)
        {
            return _unitOfWork.Repository<Resource>()
                .GetAllWithSpecProjectedAsync<Resource>(resourceSpec, mappingConfig);
        }

        public ResourceType[] GetResourceTypes()
        {
            return Enum.GetValues<ResourceType>();
        }

        public Task<Resource?> CreateResourceAsArticle(Resource resource, int schoolManagerId)
        {
            return null;
        }

        public Task<Resource?> CreateResourceAsPaper(Resource resource, int schoolManagerId)
        {
            return null;
        }

        public Task<ResourceSection> CreateResourceSection(ResourceSection section)
        {
            return null;
        }

        public Task<Resource?> CreateResourceAsBlog(BlogDraft blogDraft, int schoolManagerId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Resource>> GetResourceAsync(ISpecification<Resource> resourceSpec, IConfigurationProvider mappingConfiguration)
        {
            throw new NotImplementedException();
        }
    }
}
