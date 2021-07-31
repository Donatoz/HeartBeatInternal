using Metozis.Cardistry.Internal.Core.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Interaction.AreaModifiers
{
    public enum RelativeVector
    {
        Right,
        Forward,
        Up
    }
    
    public class LinearPositionModifier : IAreaPlaceModifier
    {
        public Vector3Distribution PositionSample;
        public bool Relative;
        [ShowIf("Relative")]
        public Area Area;
        [ShowIf("Relative")]
        public RelativeVector RelativeVector;
        public float Multiplier = 1;
        public VectorOperation Operation;
        
        public Vector3 Modify(Vector3 pos, int idx, int maxIdx)
        {
            if (Relative)
            {
                PositionSample.Origin = RelativeVector switch
                {
                    RelativeVector.Up => Area.transform.up,
                    RelativeVector.Forward => Area.transform.forward,
                    _ => Area.transform.right
                };
            }
            var sample = PositionSample.Evaluate((float)(idx + 1) / maxIdx);
            return ResolveOperation(pos, sample * Multiplier );
        }

        private Vector3 ResolveOperation(Vector3 one, Vector3 two)
        {
            return Operation == VectorOperation.Add ? one + two : one - two;
        }
    }
}