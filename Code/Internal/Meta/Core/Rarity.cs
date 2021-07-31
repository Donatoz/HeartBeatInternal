using System;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Modularity;
using Metozis.Cardistry.Internal.Meta.Core.CardVisualSchemes;
using UnityEngine;
using UnityEngine.UI;

namespace Metozis.Cardistry.Internal.Meta.Core
{
    [Serializable]
    public class Rarity
    {
        public string Name;
        public int Value;
        public Color Color;
        [ColorUsage(true, true)]
        public Color HDRColor;
        public VisualPack Visual;

        private static Dictionary<Type, Func<Rarity, Entity, GameObject[]>> uiGenerators = new Dictionary<Type, Func<Rarity, Entity, GameObject[]>>
        {
            {
                typeof(Card),
                delegate(Rarity r, Entity entity)
                {
                    var card = entity as Card;
                    if (card == null) return null;
                    
                    var front = card.GetModule<CardVisualModule>().Container.CardCanvas.transform.Find("Front");
                    if (r.Visual == null) return null;
                    var objects = new List<GameObject>();
                    if (r.Visual.Materials != null && r.Visual.Materials.ContainsKey("CardEdges"))
                    {
                        var edges = front.Find("EdgeEffects");
                        edges.GetComponent<Image>().material = r.Visual.Materials["CardEdges"];
                        edges.gameObject.SetActive(true);
                    }
                    if (r.Visual.Particles != null && r.Visual.Particles.ContainsKey("CardOuterEffect"))
                    {
                        objects.Add(MonoBehaviour.Instantiate(r.Visual.Particles["CardOuterEffect"], card.transform.GetChild(0)).gameObject);
                    }

                    return objects.ToArray();
                }
            }
        };

        public GameObject[] CreateUI(Entity entity)
        {
            return uiGenerators[entity.GetType()].Invoke(this, entity);
        }
    }
}