// <copyright file="PrivateConstructorContractResolver.cs" company="TanvirArjel">
// Copyright (c) TanvirArjel. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace TanvirArjel.Extensions.Microsoft.Caching
{
    /// <summary>
    /// This contract resolver enables deserialization with private constructor.
    /// </summary>
    internal class PrivateConstructorContractResolver : DefaultJsonTypeInfoResolver
    {
        /// <inheritdoc/>
        public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
        {
            JsonTypeInfo jsonTypeInfo = base.GetTypeInfo(type, options);

            if (jsonTypeInfo.Kind == JsonTypeInfoKind.Object && jsonTypeInfo.CreateObject is null)
            {
                // The type doesn't have public constructors
                if (jsonTypeInfo.Type.GetConstructors(BindingFlags.Public | BindingFlags.Instance).Length == 0)
                {
                    jsonTypeInfo.CreateObject = () => Activator.CreateInstance(jsonTypeInfo.Type, true);
                }
            }

            return jsonTypeInfo;
        }
    }
}
