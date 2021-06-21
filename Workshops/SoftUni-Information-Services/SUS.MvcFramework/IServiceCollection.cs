using System;

namespace SUS.MvcFramework
{
    public interface IServiceCollection
    {
        void Add<TSource, TDestination>();

        void Add<TSource>();

        object CreateInstance(Type obj);
    }
}