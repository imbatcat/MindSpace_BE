﻿using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Resources;

namespace MindSpace.Application.Interfaces.Services
{
    public interface IResourcesService
    {
        public Resource? CreateResourceAsArticle(Resource resource, int schoolManagerId);
        public ResourceType[] GetResourceTypes();
    }
}
