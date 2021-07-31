using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Interaction
{
    public interface IAreaPlaceModifier
    {
        Vector3 Modify(Vector3 pos, int idx, int maxIdx);
    }
}