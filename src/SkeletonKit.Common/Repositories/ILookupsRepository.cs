﻿using SkeletonKit.Common.Entities;

namespace SkeletonKit.Common.Repositories
{
    public interface ILookupsRepository
    {
        Task<IEnumerable<T>> GetAll<T>(string lookup) where T : Lookups;
        Task<T> Get<T>(string code, string lookup) where T : Lookups;
    }
}
