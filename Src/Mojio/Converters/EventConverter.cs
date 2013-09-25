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
                case EventType.MojioOn:
                    return new PowerEvent();
                case EventType.TripStart:
                case EventType.TripEnd:
                case EventType.TripStatus:
                    return new TripStatusEvent();
                case EventType.IgnitionOn:
                case EventType.IgnitionOff:
                    return new IgnitionEvent();
                case EventType.FenceEntered:
                case EventType.FenceExited:
                    return new FenceEvent();
                case EventType.GPS:
                    return new GPSEvent();
                case EventType.HardAcceleration:
                case EventType.HardBrake:
                case EventType.HardLeft:
                case EventType.HardRight:
                    return new HardEvent();
                default:
                    return new Event();
            }
        }
    }
}