using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Core.CardVisualSchemes;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public class VisualModule : Module
    {
        protected readonly HashSet<VisualSchemeObject> schemeObjects = new HashSet<VisualSchemeObject>();

        public VisualModule(Entity target, bool enabled = true) : base(target, enabled)
        {
        }

        public virtual void Refresh(EntityMeta meta, bool hard = false)
        {
            foreach (var cardVisualSchemeObject in schemeObjects)
            {
                cardVisualSchemeObject.Update(target);
            }
        }
        
        public void CreateScheme(IVisualScheme scheme)
        {
            foreach (var schemeObject in schemeObjects)
            {
                MonoBehaviour.Destroy(schemeObject.Bind);
            }
            schemeObjects.Clear();

            foreach (var obj in scheme.CreateUI(target))
            {
                schemeObjects.Add(obj);
            }
        }

        public void UpdateScheme()
        {
            foreach (var visualSchemeObject in schemeObjects)
            {
                visualSchemeObject.Update(target);
            }
        }
    }
}