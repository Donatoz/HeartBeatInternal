using System;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Utils
{
    [Serializable]
    public class Curve
    {
        public AnimationCurve AnimationCurve = AnimationCurve.Constant(0, 1, 1);
        public float Multiplier = 1;

        public float Evaluate(float t)
        {
            return AnimationCurve.Evaluate(t) * Multiplier;
        }
    }
}