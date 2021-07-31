using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta
{
    [CreateAssetMenu(fileName = "New pack", menuName = "Metozis/Meta/Visual pack", order = 50)]
    public class VisualPack : SerializedScriptableObject
    {
        public Dictionary<string, Material> Materials;
        public Dictionary<string, ParticleSystem> Particles;
    }
}