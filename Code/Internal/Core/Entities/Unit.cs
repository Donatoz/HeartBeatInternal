using System;
using System.Collections.Generic;

using Bolt;
using Ludiq;

using Metozis.Cardistry.Internal.Core.Construct;
using Metozis.Cardistry.Internal.Core.Entities.Units;
using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.Core.Modularity;
using Metozis.Cardistry.Internal.Core.Orders;
using Metozis.Cardistry.Internal.GameFlow;
using Metozis.Cardistry.Internal.GameFlow.LifeCycle;
using Metozis.Cardistry.Internal.Meta;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Core.Units;
using Metozis.Cardistry.Internal.Meta.Descriptors;

using Sirenix.OdinInspector;

using UnityEngine;
using UnityEngine.UI;

namespace Metozis.Cardistry.Internal.Core.Entities
{
    public class Unit : ActingEntity, IControllable
    {
        public UnitMetaDescriptor MetaDescriptor;
        public FlowMacro UnitLogicMacro;
        public Dictionary<string, IOrder> AvailableOrders { get; } = new Dictionary<string, IOrder>();

        [SerializeField]
        [BoxGroup("UI")]
        public Canvas unitCanvas;
        [SerializeField]
        [BoxGroup("UI")]
        public GameObject[] glowyPieces;
        [SerializeField]
        [BoxGroup("UI")]
        public Image artImage;

        public Action OnDeath;

        private FollowModule follow;
        private AnimationModule anim;
        private UnitVisualModule visual;

        protected List<Skill> skills = new List<Skill>();
        
        private void Awake()
        {
            follow = AddModule(new FollowModule(this));
            anim = AddModule(new AnimationModule(this));
            visual = AddModule(new UnitVisualModule(this)
            {
                Container = new UnitVisualModule.UnitVisualContainer
                {
                    UnitCanvas = unitCanvas,
                    GlowyPieces = glowyPieces,
                    ArtImage = artImage
                }
            });

            validationContext = new HashSet<Validator>
            {
                new Validator
                {
                    Name = "HasHealthStat",
                    Predicate = entity => entity.Stats.ContainsKey(Stat.HEALTH_STAT)
                },
                new Validator
                {
                    Name = "HasAttackStat",
                    Predicate = entity => entity.Stats.ContainsKey(Stat.ATTACK_STAT)
                }
            };
        }

        protected override void Start()
        {
            base.Start();
            OnDeath += delegate
            {
                //TODO: Bind animation module to this context
                Destroy(gameObject);
            };
            
            if (validated)
            {
                Stats[Stat.HEALTH_STAT].OnValueChanged += delegate
                {
                    if (Stats[Stat.HEALTH_STAT].EffectiveValue <= 0)
                    {
                        Kill();
                    }
                };
            }
        }

        public override void Initialize()
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            follow.MovementContext = delegate(Entity entity, Vector3 pos)
            {
                entity.transform.position = Vector3.Lerp(entity.transform.position, pos + Vector3.up, Time.deltaTime * 6);
            };

            if (MetaDescriptor != null)
            {
                PopulateWithMeta(MetaDescriptor);
            }
            
            OnSelection += delegate(bool selected)
            {
                anim.PlayAnimation(selected ? "In" : "Out", true);
            };
            
            if (UnitLogicMacro != null)
            {
                var lifecycle = AddModule(new UnitLifeCycle(this, GraphReference.New(UnitLogicMacro, true)));
            }
            
            base.Initialize();
        }

        public override void PopulateWithMeta(IMetaDescriptor<EntityMeta> descriptor)
        {
            var unit = descriptor as UnitMetaDescriptor;
            
            Name = descriptor.GetMeta().Name;
            if (!(descriptor.GetMeta() is UnitMeta unitMeta)) return;

            foreach (var stat in unitMeta.Stats)
            {
                var created = AddStat(stat.StatName, new Stat(stat.CurrentValue, stat.MinMax.x, stat.MinMax.y));
                foreach (var modifier in stat.Modifiers)
                {
                    Stats[stat.StatName].AddModifier(new Modifier {Id = modifier.Id, Value = modifier.Value});
                }

                if (stat.HasUI)
                {
                    created.OnValueChanged += visual.UpdateScheme;
                }
            }

            foreach (var orderSlot in unit.AvailableOrders)
            {
                if (!orderSlot.IsOverride && !MetaManager.Instance.Configuration.DefaultOrders.ContainsKey(orderSlot.OrderName)) continue;
                
                var orderMeta = MetaManager.Instance.Configuration.DefaultOrders[orderSlot.OrderName];
                var order = new FlowOrder(orderSlot.OverrideLogic ? orderSlot.OverrideLogic : orderMeta.OrderLogicMacro);
                AvailableOrders[orderSlot.OrderName] = order;
            }

            foreach (var skill in unit.Skills)
            {
                skills.Add(new Skill(this, skill.Meta));
            }
            
            visual.CreateScheme(unit.VisualScheme);
            visual.Refresh(descriptor.GetMeta(), true);
        }

        public void Kill()
        {
            OnDeath?.Invoke();
        }

        public void GiveOrder(IOrder order, params object[] args)
        {
            Game.Current.Scheduler.Schedule(ScheduleContext.FromOrder(order, this, args));
        }

        public void CastSkill(int localId)
        {
            skills[localId].Cast();
        }
    }
}