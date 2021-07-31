using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Modularity;
using Metozis.Cardistry.Internal.Core.Utils;
using Metozis.Cardistry.Internal.GameFlow;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Descriptors;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Interaction
{
    /// <summary>
    /// Simple observable collection of entities.
    /// </summary>
    public class Area : Entity, IEnumerable<Entity>
    {
        public Player Owner;
        public Action<Entity> OnEntityEnter;
        public Action<Entity> OnEntityExit;
        public TypeValidator Validator;
        public bool Updatable;

        private readonly IList<Entity> members = new List<Entity>();
        public IAreaPlacer AreaPlacer;
        public List<IAreaPlaceModifier> Modifiers = new List<IAreaPlaceModifier>();

        protected virtual void Awake()
        {
            OnEntityEnter += delegate
            {
                Refresh();
            };
            OnEntityExit += delegate
            {
                Refresh();
            };
        }

        public void Refresh()
        {
            for (int i = 0; i < members.Count; i++)
            {
                if (members[i] == null)
                {
                    RemoveAt(i);
                    continue;
                }
                if (members[i].HasModule<FollowModule>())
                {
                    members[i].GetModule<FollowModule>().EnableMovement = true;
                    var finalPos = AreaPlacer.PlaceObject(i);
                    foreach (var modifier in Modifiers)
                    {
                        finalPos = modifier.Modify(finalPos, i, members.Count);
                    }
                    members[i].GetModule<FollowModule>().MakeOffset(finalPos);
                }
            }
        }

        protected override void Update()
        {
            base.Update();
            if(!Updatable) return;
            Refresh();
        }

        public override void PopulateWithMeta(IMetaDescriptor<EntityMeta> descriptor)
        {
            throw new NotImplementedException();
        }

        public bool Validate(Entity e)
        {
            return Validator.Validate(e.GetType());
        }

        public void Add(Entity e)
        {
            members.Add(e);
            OnEntityEnter?.Invoke(e);
            e.OnDestroyed += delegate
            {
                Remove(e);
            };
        }

        public Entity Get(int idx)
        {
            return members[idx];
        }

        public void Remove(Entity e)
        {
            members.Remove(e);
            OnEntityExit?.Invoke(e);
        }

        public void RemoveAt(int idx)
        {
            var e = members[idx];
            members.RemoveAt(idx);
            OnEntityExit?.Invoke(e);
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return members.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}