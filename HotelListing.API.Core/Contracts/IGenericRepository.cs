﻿using HotelListing.API.Core.Models;

namespace HotelListing.API.Core.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);

        Task<TResult> GetAsync<TResult>(int? id);

        Task<List<TResult>> GetAllAsync<TResult>();

        Task<PagedResult<TResult>> GetAllSync<TResult>(QueryParameters queryParameters);

        Task<T> AddAsync(T entity);

        Task<TResult> AddAsync<TSource, TResult>(TSource source);

        Task UpdateAsync(T entity);

        Task UpdateAsync<TSource>(int id, TSource source);

        Task DeleteAsync(int id);

        Task<bool> Exists(int id);
    }
}
