﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mojio.Serialization
{
    public abstract partial class TypeEnumDiscriminatorMap
    {        
        public string Property { get; set; }

        public static Dictionary<Type, TypeEnumDiscriminatorMap> Maps { get; private set; }

        static TypeEnumDiscriminatorMap<D> Map<B, D>(Expression<Func<B, D>> property)
            where D : struct
        {
            Type baseType = typeof(B);
            TypeEnumDiscriminatorMap<D> map;

            if (!Maps.ContainsKey(baseType))
            {
                map = new TypeEnumDiscriminatorMap<D>();
                Maps.Add(baseType, map);
            }
            else
                map = Maps[baseType] as TypeEnumDiscriminatorMap<D>;

            map.Property = (property.Body as MemberExpression).Member.Name;

            return map;
        }
        
        static public TypeEnumDiscriminatorMap GetMap(Type type)
        {
            if (Maps.ContainsKey(type))
                return Maps[type];
            return null;
        }
        public abstract Type Find(string discriminator);
        public abstract object Create(string discriminator = null);
    }

    class TypeEnumDiscriminatorMap<D> : TypeEnumDiscriminatorMap
        where D : struct
    {
        public Dictionary<D, Type> SubTypes { get; set; }

        public TypeEnumDiscriminatorMap<D> Contains<S>(D disc)
        {
            var type = typeof(S);
            if (SubTypes == null)
                SubTypes = new Dictionary<D, Type>();
            SubTypes.Add(disc, type);
            return this;
        }

        public override Type Find(string discriminator)
        {
            D disc = default(D);
            if (!string.IsNullOrEmpty(discriminator))
            {
                if (!Enum.TryParse<D>(discriminator, out disc))
                    throw new ArgumentException(typeof(D) + " does not contain " + discriminator);

                if (!SubTypes.ContainsKey(disc))
                    throw new ArgumentException("Map not contain " + discriminator);
            }

            return SubTypes[disc];
        }

        public override object Create(string discriminator = null)
        {
            var type = Find(discriminator);
            return Activator.CreateInstance(type);
        }
    }

}
