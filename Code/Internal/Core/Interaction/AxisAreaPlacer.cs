using System.Linq;
using Sirenix.Utilities;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Interaction
{
    public class AxisAreaPlacer : IAreaPlacer
    {
        public enum PlacerAxis
        {
            XAxis,
            YAxis
        }
        
        public Area Area;
        public float Offset;
        public float ObjectWidth;
        public PlacerAxis Axis;

        public AxisAreaPlacer(Area area, float offset, float objectWidth, PlacerAxis axis)
        {
            Area = area;
            Offset = offset;
            ObjectWidth = objectWidth;
            Axis = axis;
        }
        
        public Vector3 PlaceObject(int idx)
        {
            var leftAnchor = Area.transform.position 
                             + (Axis == PlacerAxis.XAxis ? Area.transform.right  : Area.transform.forward)
                             * (-(Axis == PlacerAxis.XAxis ? Area.transform.localScale.x  : Area.transform.localScale.z) / 2);
            var finalPos = leftAnchor 
                           + (Axis == PlacerAxis.XAxis ? Area.transform.right  : Area.transform.forward) * (Offset + (ObjectWidth * idx));
            return finalPos;
        }
    }
}