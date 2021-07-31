using System;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Management;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Metozis.Cardistry.Internal.Meta.UI
{
    [CreateAssetMenu(fileName = "New UI Descriptor", menuName = "Metozis/UI/UI Object", order = 50)]
    public class UIObjectMetaDescriptor : SerializedScriptableObject
    {
        [Required]
        public string GameojbectName;
        public string Root;
        public Sprite ImageSprite;
        public Vector2 AnchorMin;
        public Vector2 AnchorMax;
        public Vector2 Pivot;
        public Color Tint;
        public Vector3 RectPosition;
        public Vector2 RectSize;
        public float RotationZ;
        public Vector3 RectScale;
        public List<UIObjectMetaDescriptor> Children;
        public List<IUIObjectFeature> Features;
    }

    public interface IUIObjectFeature
    {
        void ModifyUI(GameObject target);
    }
}