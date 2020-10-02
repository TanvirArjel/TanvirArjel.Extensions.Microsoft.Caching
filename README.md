# TanvirArjel.Extensions.Microsoft.Caching
This library extended the functionality of `IDistributedCache` interface to make the caching mechanism more easier in .NET 5.0 and .NET Core.

## How do I get started

First install the lastest version of `TanvirArjel.Extensions.Microsoft.Caching` nuget package into your project as follows:

    Install-Package TanvirArjel.Extensions.Microsoft.Caching
    
## Basic usage

    using  TanvirArjel.Extensions.Microsoft.Caching
    
    // To set
    Employee employee = new Employee
    {
       Id = 1,
       Name = "Tanvir"
    };
    
    string cacheKey = "Employee1";
    await _distributedCache.SetAsync<Employee>(cacheKey, employee);
    
    // To retrive
    Employee = await _distributedCache.GetAsync<Employee>(cacheKey);
    
# Available methods
  1. `GetAsync<T>()`
  2. `SetAsync<T>()` - 3 overloads
  3. `AddToListAsync<T>()` - 3 overloads
  4. `AddToListAsync<T, TKey>()` - 3 overloads
  5. `UpdateInListAsync<T>()` - 3 overloads
  6. `UpdateInListAsync<T, TKey>()` - 3 overloads
  7. `RemoveFromListAsync()` - 3 overloads
  
  ## Thank you!
