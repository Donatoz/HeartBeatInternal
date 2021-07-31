using System;
using Bolt;
using Ludiq;
using Metozis.Cardistry.Internal.Core.Construct;
using Metozis.Cardistry.Internal.Core.Entities.Cards;
using Metozis.Cardistry.Internal.Core.Modularity;
using Metozis.Cardistry.Internal.GameFlow.LifeCycle;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Descriptors;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Metozis.Cardistry.Internal.Core.Entities
{
    public class Card : ActingEntity
    {
        [BoxGroup("Card")]
        public FlowMacro CardLogicMacro;
        [BoxGroup("Card")]
        public CardMetaDescriptor MetaDescriptor;
        
        [BoxGroup("UI")] 
        public Image ArtImage;

        private DragModule drag;
        private FollowModule follow;
        private CardVisualModule visual;
        private AnimationModule anim;
        private LookAtModule look;
        
        private void Awake()
        {
            drag = AddModule(new DragModule(this));
            follow = AddModule(new FollowModule(this));
            visual = AddModule(new CardVisualModule(this, new CardVisualModule.CardVisualContainer
            {
                ArtImage = ArtImage,
                CardRenderer = transform.GetChild(0).GetComponent<MeshRenderer>(),
                CardCanvas = transform.GetChild(0).GetComponentInChildren<Canvas>()
            }));
            anim = AddModule(new AnimationModule(this));
            anim.AnimationEvents["CardPlayEnd"] = delegate
            {
                anim.OnAnimationEnd.Invoke("CardPlay");
            };
            look = AddModule(new LookAtModule(this, Camera.main.transform.GetChild(0)));
            look.DefaultContext = (entity, rot) =>
            {
                entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation, rot, Time.deltaTime * 10);
            };
            look.ResetContext();
            
            anim.OnAnimationEnd += delegate(string anim)
            {
                if (anim == "CardPlay")
                {
                    Destroy(gameObject);
                }
            };

            follow.EnableMovement = true;
            
            if (CardLogicMacro != null)
            {
                var lifeCycle = AddModule(new CardLifeCycle(this, GraphReference.New(CardLogicMacro, true)));
                lifeCycle.OnDeath += delegate
                {
                    follow.Enabled = false;
                };
                lifeCycle.OnPlayBegin += delegate
                {
                    anim.PlayAnimation("CardPlay");
                };
            }
        }

        public override void Initialize()
        {
            follow.MovementContext = delegate(Entity entity, Vector3 pos)
            {
                entity.transform.position = Vector3.Lerp(entity.transform.position, pos + Vector3.up, Time.deltaTime * 6);
            };

            drag.OnDrag += delegate(Vector3 pos)
            {
                follow.MakeOffset(pos);
            };

            if (MetaDescriptor != null)
            {
                PopulateWithMeta(MetaDescriptor);
            }
            
            base.Initialize();
        }

        public override void PopulateWithMeta(IMetaDescriptor<EntityMeta> descriptor)
        {
            var meta = descriptor.GetMeta();
            var cardDescriptor = descriptor as CardMetaDescriptor;
            MetaDescriptor = cardDescriptor;
            
            Name = meta.Name;
            if (!(meta is CardMeta cardMeta)) return;

            foreach (var stat in cardMeta.Stats)
            {
                AddStat(stat.StatName, new Stat(stat.CurrentValue, stat.MinMax.x, stat.MinMax.y));
                foreach (var modifier in stat.Modifiers)
                {
                    Stats[stat.StatName].AddModifier(new Modifier {Id = modifier.Id, Value = modifier.Value});
                }
            }

            foreach (var feature in cardDescriptor.Features)
            {
                feature.Resolve(this);
            }
            
            visual.CreateScheme(cardDescriptor.VisualScheme);
            
            visual.Refresh(cardMeta, true);
        }

        public override void HandleAnimationEvent(string evt)
        {
            anim.HandleAnimationEvent(evt);
        }
    }
}