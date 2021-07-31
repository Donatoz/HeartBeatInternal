using System;
using LeTai.TrueShadow;
using Metozis.Cardistry.Internal.Management;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.UI
{
    [Serializable]
    public class UIObjectShadowFeature : IUIObjectFeature
    {
        public float Size;
        public float Spread;
        public Color Color;
        
        public void ModifyUI(GameObject target)
        {
            var shadow = target.AddComponent<TrueShadow>();
            shadow.Color = Color;
            shadow.Spread = Spread;
            shadow.Size = Size;
        }
    }
}