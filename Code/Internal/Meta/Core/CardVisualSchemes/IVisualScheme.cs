using System;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Entities;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Core.CardVisualSchemes
{
    public interface IVisualScheme
    {
        IEnumerable<VisualSchemeObject> CreateUI(Entity e);
    }

    public struct VisualSchemeObject
    {
        public GameObject Bind;
        public Action<Entity, GameObject> UpdateContext;

        public void Update(Entity e)
        {
            UpdateContext?.Invoke(e, Bind);
        }
    }
}