using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SUS.MvcFramework
{
    public class ServiceCollection : IServiceCollection
    {
        //Dependency container
        private protected readonly Dictionary<Type, Type> DependencyDictionary = new Dictionary<Type, Type>();
        public void Add<TSource, TDestination>()
        {
            this.DependencyDictionary[typeof(TSource)] = typeof(TDestination);
        }

        public void Add<TSource>()
        {
            this.DependencyDictionary[typeof(TSource)] = typeof(TSource);
        }

        public object CreateInstance(Type type)
        {
            //Todo: Implement this method!
            if (this.DependencyDictionary.ContainsKey(type))
            {
                type = this.DependencyDictionary[type];
            }

            var constructor = type
                .GetConstructors()
                .OrderBy(x => x.GetParameters().Count())
                .FirstOrDefault();

            ParameterInfo[] parameters = constructor.GetParameters();

            List<object> parameterValues = new List<object>();

            foreach (var parameter in parameters)
            {
                object currentParameterValue = CreateInstance(parameter.ParameterType);

                parameterValues.Add(currentParameterValue);
            }

            object createdObject = constructor.Invoke(parameterValues.ToArray());

            return createdObject;
        }
    }
}