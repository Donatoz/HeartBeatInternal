using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Utils
{
    public class TypesTuple
    {
        public string Types = "";
        public string Namespace = "Metozis.Cardistry.Internal.Core";
        public char Separator = ',';

        public List<Type> GetTypes()
        {
            var list = new List<Type>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            foreach (var type in Types.Split(Separator))
            {
                list.Add(Type.GetType(Namespace + "." + type.Trim()));
            }

            return list;
        }
    }
}