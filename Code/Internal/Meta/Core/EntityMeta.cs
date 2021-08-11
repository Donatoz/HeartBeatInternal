using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Core
{
    [Serializable]
    public class EntityMeta
    {
        [TitleGroup("Entity options", Alignment = TitleAlignments.Centered)]
        public string Name;
        [TitleGroup("Entity options", Alignment = TitleAlignments.Centered)]
        public string EntityId;
    }
}