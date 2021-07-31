using System;
using System.Collections;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Modularity;
using Metozis.Cardistry.Internal.Core.Reactive;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Descriptors;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Entities
{
    public abstract class Entity : SerializedMonoBehaviour
    {
        public string Name;
        public Guid EntityId;

        public Action OnDestroyed;
        public Action OnInitialized;

        private readonly HashSet<IRuntimeModule> runtimeModules = new HashSet<IRuntimeModule>();
        private readonly Dictionary<Type, Module> modulesCache = new Dictionary<Type, Module>();

        private bool initialized;

        public T AddModule<T>(T module) where T : Module
        {
            modulesCache[module.GetType()] = module;
            if (module is IRuntimeModule runtimeModule)
            {
                runtimeModules.Add(runtimeModule);
            }

            return module;
        }

        public bool HasModule<T>() where T : Module
        {
            return modulesCache.ContainsKey(typeof(T));
        }

        public bool TryRemoveModule(Type moduleType)
        {
            try
            {
                modulesCache.Remove(moduleType);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public T GetModule<T>() where T : Module
        {
            try
            {
                return (T) modulesCache[typeof(T)];
            }
            catch (KeyNotFoundException)
            {
                return default;
            }
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }

        protected virtual void Start()
        {
            if (!initialized)
            {
                Initialize();
            }
        }

        protected virtual void Update()
        {
            foreach (var runtimeModule in runtimeModules)
            {
                runtimeModule.Update();
            }
        }

        public virtual void Initialize()
        {
            EntityId = Guid.NewGuid();
            if (Name.IsNullOrWhitespace())
            {
                Name = $"Entity {EntityId}";
            }

            OnInitialized?.Invoke();
            initialized = true;
        }

        public abstract void PopulateWithMeta(IMetaDescriptor<EntityMeta> descriptor);
    }
}