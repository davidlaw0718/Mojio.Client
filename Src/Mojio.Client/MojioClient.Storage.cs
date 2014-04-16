﻿using Mojio.Events;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mojio.Client
{
    public partial class MojioClient
    {
        public Task<bool> SetStoredAsync<T> (Guid id, string key, string value) {
            var type = typeof(T);
            return SetStoredAsync (type, id, key, value);
        }

        public Task<bool> SetStoredAsync (Type type, Guid id, string key, string value) {
            string action = Map[type];
            var request = GetRequest(Request(action, id, "store", key), Method.PUT);
            request.AddBody(value);

            return RequestAsync (request).ContinueWith (t => {
                var response = t.Result;
                return response.StatusCode == HttpStatusCode.OK
                    || response.StatusCode == HttpStatusCode.Created;
            });
        }

        public Task<string> GetStoredAsync<T> (Guid id, string key) {
            var type = typeof(T);
            return GetStoredAsync (type, id, key);
        }

        public Task<String> GetStoredAsync (Type type, Guid id, string key) {
            string action = Map[type];
            var request = GetRequest(Request(action, id, "store", key), Method.GET);

            return RequestAsync (request).ContinueWith (t => {
                var response = t.Result;

                // Invalid response.
                // TODO: we should add methods to pass back the HttpStatusCode and message
                if (response.StatusCode != HttpStatusCode.OK)
                    return null;

                return Deserialize<String> (response.Content);
            });
        }

        /// <summary>
        /// Save storage value.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetStored(GuidEntity entity, string key, object value)
        {
            return SetStored(entity.GetType(), entity.Id, key, value);
        }

        /// <summary>
        /// Save storage value.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool SetStored(Type type, Guid id, string key, object value)
        {
            string action = Map[type];
            var request = GetRequest(Request(action, id, "store", key), Method.PUT);
            request.AddBody(value);

            var response = RestClient.Execute(request);
            return response.StatusCode == HttpStatusCode.OK
                || response.StatusCode == HttpStatusCode.Created;
        }

        public string GetStored(GuidEntity entity, string key)
        {
            return GetStored(entity.GetType(), entity.Id, key);
        }

        public T GetStored<T>(GuidEntity entity, string key)
             where T : new()
        {
            return GetStored<T>(entity.GetType(), entity.Id, key);
        }

        public string GetStored(Type type, Guid id, string key)
        {
            string action = Map[type];
            var request = GetRequest(Request(action, id, "store", key), Method.GET);

            var response = RestClient.Execute(request);

            // Invalid response.
            // TODO: we should add methods to pass back the HttpStatusCode and message
            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            // TODO: This isn't exactly a good way of doing this. But we need an 
            //   alternative way to get string responses.
            var deserializer = new RSJsonSerializer();
            return deserializer.Deserialize<string>(response);
        }

        public T GetStored<T>(Type type, Guid id, string key)
            where T : new()
        {
            string action = Map[type];
            var request = GetRequest(Request(action, id, "store", key), Method.GET);

            var response = RestClient.Execute<T>(request);
            return response.Data;
        }

        public bool DeleteStored(GuidEntity entity, string key)
        {
            return DeleteStored(entity.GetType(), entity.Id, key);
        }

        public bool DeleteStored(Type type, Guid id, string key)
        {
            string action = Map[type];
            var request = GetRequest(Request(action, id, "store", key), Method.DELETE);

            var response = RestClient.Execute(request);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}