﻿using Mojio.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Mojio.Converters
{
    public class EventConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Event).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType,
          object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            var target = Create(objectType, jsonObject, serializer);
            serializer.Populate(jsonObject.CreateReader(), target);
            return target;
        }

        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        protected Event Create(Type objectType, JObject jsonObject,JsonSerializer serializer)
        {
            // default type
            EventType eventType = EventType.Information;

            if (jsonObject["EventType"] != null)
                eventType = jsonObject["EventType"].ToObject<EventType>(serializer);

            switch (eventType)
            {
                case EventType.IgnitionOn:
                case EventType.IgnitionOff:
                    return new IgnitionEvent();
                case EventType.GPS:
                    return new GPSEvent();
                case EventType.TripEnd:
                    return new TripEndEvent();
                default:
                    return new Event();
            }
        }
    }
}