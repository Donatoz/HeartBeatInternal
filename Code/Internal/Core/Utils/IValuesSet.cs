using System;
using Ludiq;

namespace Metozis.Cardistry.Internal.Core.Utils
{
    public interface IValuesSet
    {
        object[] GetValues();
    }
    
    [Serializable]
    public struct ValuesTuple<T> : IValuesSet
    {
        public T[] Values;
        
        public object[] GetValues()
        {
            return Values as object[];
        }
    }
    
    [Serializable]
    public struct ValuesTuple<T1, T2> : IValuesSet
    {
        public T1[] FirstTypeValues;
        public T2[] SecondTypeValues;
        
        public object[] GetValues()
        {
            var values = FirstTypeValues;
            values.AddRange(SecondTypeValues);
            return values as object[];
        }
    }
    
    [Serializable]
    public struct ValueEnumeration<T> : IValuesSet
    {
        public T Value;
        
        public object[] GetValues()
        {
            return new object[] {Value};
        }
    }
    
    [Serializable]
    public struct ValueEnumeration<T1, T2> : IValuesSet
    {
        public T1 Value1;
        public T2 Value2;
        
        public object[] GetValues()
        {
            return new object[] {Value1, Value2};
        }
    }
    
    [Serializable]
    public struct ValueEnumeration<T1, T2, T3> : IValuesSet
    {
        public T1 Value1;
        public T2 Value2;
        public T3 Value3;
        
        public object[] GetValues()
        {
            return new object[] {Value1, Value2, Value3};
        }
    }
}