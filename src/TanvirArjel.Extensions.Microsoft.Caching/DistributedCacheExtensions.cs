// <copyright file="DistributedCacheExtensions.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace TanvirArjel.Extensions.Microsoft.Caching
{
    /// <summary>
    /// The class con.
    /// </summary>
    public static class DistributedCacheExtensions
    {
        /// <summary>
        /// Asynchronously gets the value from the specified cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the stored data.</typeparam>
        /// <param name="distributedCache">The cache in which data is to be stored.</param>
        /// <param name="key">A string identifying the requested value.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        public static async Task<T> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(key, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                T t = JsonSerializer.Deserialize<T>(utf8Bytes);
                return t;
            }

            return default;
        }

        /// <summary>
        /// Asynchronously store the value in the specified cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the data to be stored.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key to store the data in.</param>
        /// <param name="obj">The data to be stored in the cache.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task SetAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            T obj,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
             .SetSlidingExpiration(TimeSpan.FromDays(7));
            byte[] utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<T>(obj);
            await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously store the value in the specified cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the data to be stored.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key to store the data in.</param>
        /// <param name="obj">The data to be stored in the cache.</param>
        /// <param name="offset">The timespan for the slidding expiration of the cache.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task SetAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            T obj,
            TimeSpan offset,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (offset == null)
            {
                throw new ArgumentNullException(nameof(offset));
            }

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions()
             .SetSlidingExpiration(offset);
            byte[] utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<T>(obj);
            await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously store the value in the specified cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the data to be stored.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key to store the data in.</param>
        /// <param name="obj">The data to be stored in the cache.</param>
        /// <param name="options">An object of <see cref="DistributedCacheEntryOptions"/>.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task SetAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            T obj,
            DistributedCacheEntryOptions options,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            byte[] utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<T>(obj);
            await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Asynchronously add the item to the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="item">The item to be added to the existing item list in the cache.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task AddToListAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            T item,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    itemList.Add(item);

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(7));
                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously add the item to the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="item">The item to be added to the existing item list in the cache.</param>
        /// <param name="offset">The timespan for the slidding expiration of the cache.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task AddToListAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            T item,
            TimeSpan offset,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (offset == null)
            {
                throw new ArgumentNullException(nameof(offset));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    itemList.Add(item);

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(offset);
                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously add the item to the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="item">The item to be added to the existing item list in the cache.</param>
        /// <param name="options">An object of <see cref="DistributedCacheEntryOptions"/>.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task AddToListAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            T item,
            DistributedCacheEntryOptions options,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    itemList.Add(item);

                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously add the item to the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <typeparam name="TKey">The tyep of the order by property.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="item">The item to be added to the existing item list in the cache.</param>
        /// <param name="orderBy">A <see cref="Func{T, TResult}"/> expression to sort the list.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task AddToListAsync<T, TKey>(
            this IDistributedCache distributedCache,
            string cacheKey,
            T item,
            Func<T, TKey> orderBy,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (orderBy == null)
            {
                throw new ArgumentNullException(nameof(orderBy));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    itemList.Add(item);
                    itemList = itemList.OrderBy(orderBy).ToList();

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(7));
                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously add the item to the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <typeparam name="TKey">The tyep of the order by property.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="item">The item to be added to the existing item list in the cache.</param>
        /// <param name="orderBy">A <see cref="Func{T, TResult}"/> expression to sort the list.</param>
        /// <param name="offset">The timespan for the slidding expiration of the cache.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task AddToListAsync<T, TKey>(
            this IDistributedCache distributedCache,
            string cacheKey,
            T item,
            Func<T, TKey> orderBy,
            TimeSpan offset,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (orderBy == null)
            {
                throw new ArgumentNullException(nameof(orderBy));
            }

            if (offset == null)
            {
                throw new ArgumentNullException(nameof(offset));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    itemList.Add(item);
                    itemList = itemList.OrderBy(orderBy).ToList();

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(offset);
                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously add the item to the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <typeparam name="TKey">The tyep of the order by property.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="item">The item to be added to the existing item list in the cache.</param>
        /// <param name="orderBy">A <see cref="Func{T, TResult}"/> expression to sort the list.</param>
        /// <param name="options">An object of <see cref="DistributedCacheEntryOptions"/>.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task AddToListAsync<T, TKey>(
            this IDistributedCache distributedCache,
            string cacheKey,
            T item,
            Func<T, TKey> orderBy,
            DistributedCacheEntryOptions options,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (orderBy == null)
            {
                throw new ArgumentNullException(nameof(orderBy));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    itemList.Add(item);
                    itemList = itemList.OrderBy(orderBy).ToList();

                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously update the item in the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="predicate">The condition by which item will be identified.</param>
        /// <param name="updatedItem">The updated item that will replace the existing value of item.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task UpdateInListAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            Predicate<T> predicate,
            T updatedItem,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (updatedItem == null)
            {
                throw new ArgumentNullException(nameof(updatedItem));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    int itemIndex = itemList.FindIndex(predicate);
                    itemList[itemIndex] = updatedItem;

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(7));
                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously update the item in the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="predicate">The condition by which item will be identified.</param>
        /// <param name="updatedItem">The updated item that will replace the existing value of item.</param>
        /// <param name="offset">The timespan for the slidding expiration of the cache.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task UpdateInListAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            Predicate<T> predicate,
            T updatedItem,
            TimeSpan offset,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (updatedItem == null)
            {
                throw new ArgumentNullException(nameof(updatedItem));
            }

            if (offset == null)
            {
                throw new ArgumentNullException(nameof(offset));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    int itemIndex = itemList.FindIndex(predicate);
                    itemList[itemIndex] = updatedItem;

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(offset);
                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously update the item in the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="predicate">The condition by which item will be identified.</param>
        /// <param name="updatedItem">The updated item that will replace the existing value of item.</param>
        /// <param name="options">An object of <see cref="DistributedCacheEntryOptions"/>.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task UpdateInListAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            Predicate<T> predicate,
            T updatedItem,
            DistributedCacheEntryOptions options,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (updatedItem == null)
            {
                throw new ArgumentNullException(nameof(updatedItem));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    int itemIndex = itemList.FindIndex(predicate);
                    itemList[itemIndex] = updatedItem;

                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously update the item in the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <typeparam name="TKey">The tyep of the order by property.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="predicate">The condition by which item will be identified.</param>
        /// <param name="updatedItem">The updated item that will replace the existing value of item.</param>
        /// <param name="orderBy">A <see cref="Func{T, TResult}"/> expression to sort the list.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task UpdateInListAsync<T, TKey>(
            this IDistributedCache distributedCache,
            string cacheKey,
            Predicate<T> predicate,
            T updatedItem,
            Func<T, TKey> orderBy,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (updatedItem == null)
            {
                throw new ArgumentNullException(nameof(updatedItem));
            }

            if (orderBy == null)
            {
                throw new ArgumentNullException(nameof(orderBy));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    int itemIndex = itemList.FindIndex(predicate);
                    itemList[itemIndex] = updatedItem;

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(7));
                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously update the item in the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <typeparam name="TKey">The tyep of the order by property.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="predicate">The condition by which item will be identified.</param>
        /// <param name="updatedItem">The updated item that will replace the existing value of item.</param>
        /// <param name="orderBy">A <see cref="Func{T, TResult}"/> expression to sort the list.</param>
        /// <param name="offset">The timespan for the slidding expiration of the cache.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task UpdateInListAsync<T, TKey>(
            this IDistributedCache distributedCache,
            string cacheKey,
            Predicate<T> predicate,
            T updatedItem,
            Func<T, TKey> orderBy,
            TimeSpan offset,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (updatedItem == null)
            {
                throw new ArgumentNullException(nameof(updatedItem));
            }

            if (orderBy == null)
            {
                throw new ArgumentNullException(nameof(orderBy));
            }

            if (offset == null)
            {
                throw new ArgumentNullException(nameof(offset));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    int itemIndex = itemList.FindIndex(predicate);
                    itemList[itemIndex] = updatedItem;

                    DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(offset);
                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously update the item in the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <typeparam name="TKey">The tyep of the order by property.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="predicate">The condition by which item will be identified.</param>
        /// <param name="updatedItem">The updated item that will replace the existing value of item.</param>
        /// <param name="orderBy">A <see cref="Func{T, TResult}"/> expression to sort the list.</param>
        /// <param name="options">An object of <see cref="DistributedCacheEntryOptions"/>.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task UpdateInListAsync<T, TKey>(
            this IDistributedCache distributedCache,
            string cacheKey,
            Predicate<T> predicate,
            T updatedItem,
            Func<T, TKey> orderBy,
            DistributedCacheEntryOptions options,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (updatedItem == null)
            {
                throw new ArgumentNullException(nameof(updatedItem));
            }

            if (orderBy == null)
            {
                throw new ArgumentNullException(nameof(orderBy));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    int itemIndex = itemList.FindIndex(predicate);
                    itemList[itemIndex] = updatedItem;

                    utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                    await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Asynchronously remove the item from the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="predicate">The condition by which item will be identified.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task RemoveFromListAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            Predicate<T> predicate,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    T itemToBeRemoved = itemList.Find(predicate);

                    if (itemToBeRemoved != null)
                    {
                        itemList.Remove(itemToBeRemoved);

                        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(7));
                        utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                        await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                    }
                }
            }
        }

        /// <summary>
        /// Asynchronously remove the item from the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="predicate">The condition by which item will be identified.</param>
        /// <param name="offset">The timespan for the slidding expiration of the cache.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task RemoveFromListAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            Predicate<T> predicate,
            TimeSpan offset,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (offset == null)
            {
                throw new ArgumentNullException(nameof(offset));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    T itemToBeRemoved = itemList.Find(predicate);

                    if (itemToBeRemoved != null)
                    {
                        itemList.Remove(itemToBeRemoved);

                        DistributedCacheEntryOptions options = new DistributedCacheEntryOptions().SetSlidingExpiration(offset);
                        utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                        await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                    }
                }
            }
        }

        /// <summary>
        /// Asynchronously remove the item from the item list already stored in the cache with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="distributedCache">The cache in which data is stored.</param>
        /// <param name="cacheKey">The key of the existing item list in the cache.</param>
        /// <param name="predicate">The condition by which item will be identified.</param>
        /// <param name="options">An object of <see cref="DistributedCacheEntryOptions"/>.</param>
        /// <param name="token">Optional. The <see cref="CancellationToken"/> used to propagate notifications
        /// that the operation should be canceled.
        /// </param>
        /// <returns>The <see cref="Task{TResult}"/> that represents the asynchronous operation.</returns>
        public static async Task RemoveFromListAsync<T>(
            this IDistributedCache distributedCache,
            string cacheKey,
            Predicate<T> predicate,
            DistributedCacheEntryOptions options,
            CancellationToken token = default)
        {
            if (distributedCache == null)
            {
                throw new ArgumentNullException(nameof(distributedCache));
            }

            if (cacheKey == null)
            {
                throw new ArgumentNullException(nameof(cacheKey));
            }

            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            byte[] utf8Bytes = await distributedCache.GetAsync(cacheKey, token).ConfigureAwait(false);

            if (utf8Bytes != null)
            {
                List<T> itemList = JsonSerializer.Deserialize<List<T>>(utf8Bytes);

                if (itemList != null)
                {
                    T itemToBeRemoved = itemList.Find(predicate);

                    if (itemToBeRemoved != null)
                    {
                        itemList.Remove(itemToBeRemoved);

                        utf8Bytes = JsonSerializer.SerializeToUtf8Bytes<List<T>>(itemList);
                        await distributedCache.SetAsync(cacheKey, utf8Bytes, options, token).ConfigureAwait(false);
                    }
                }
            }
        }
    }
}
