using System;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Descriptors;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Entities
{
    public class EntityFactory<T> where T : Entity
    {
        public Action<Entity> AdditionalPass;
        
        public virtual T Create(IMetaDescriptor<EntityMeta> descriptor, CreationPass pass)
        {
            var prefab = Resources.Load<GameObject>(pass.PrefabPath);
            if (prefab.GetComponent<Entity>() == null)
            {
                return null;
            }
            
            var instance = MonoBehaviour.Instantiate(prefab);
            var entity = instance.GetComponent<Entity>();

            pass.PrePass?.Invoke(entity);
            
            entity.Initialize();
            entity.PopulateWithMeta(descriptor);
            
            pass.PostPass?.Invoke(entity);
            AdditionalPass?.Invoke(entity);
            
            return instance.GetComponent(typeof(T)) as T;
        }
    }
}