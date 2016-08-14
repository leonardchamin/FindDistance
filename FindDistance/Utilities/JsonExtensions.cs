﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace FindDistance.Utilities
{
    public static class JsonExtensions
    {
        public static string ToJson<T>(this T obj, bool includeNull = true)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = new JsonConverter[] {new StringEnumConverter()},
                NullValueHandling = includeNull ? NullValueHandling.Include : NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(obj, settings);
        }


    }
}