using System;
using Bolt;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Core.Units
{
    [Serializable]
    public class ItemMeta : ActingEntityMeta
    {
        public int Amount = 1;
        public FlowMacro ItemLogicMacro;
    }
}