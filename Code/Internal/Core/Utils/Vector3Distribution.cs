using System;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Utils
{
    [Serializable]
    public struct Vector3Distribution
    {
        public Vector3 Origin;
        public Curve XCurve;
        public Curve YCurve;
        public Curve ZCurve;
        public bool Add;
        
        public Vector3 Evaluate(float t)
        {
            return Add
                ? new Vector3(Origin.x + XCurve.Evaluate(t), Origin.y + YCurve.Evaluate(t), Origin.z + ZCurve.Evaluate(t)) 
                : new Vector3(Origin.x * XCurve.Evaluate(t), Origin.y * YCurve.Evaluate(t), Origin.z * ZCurve.Evaluate(t));
        }
    }
}