using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.UI
{
    [CreateAssetMenu(fileName = "New scheme", menuName = "Metozis/UI/Color scheme")]
    public class ColorScheme : SerializedScriptableObject
    {
        public Color WindowBodyColor;
    }
}