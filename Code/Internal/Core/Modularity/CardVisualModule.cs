using System;
using System.Collections.Generic;
using Metozis.Cardistry.Extensions;
using Metozis.Cardistry.Internal.Core.Construct;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.GameFlow;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Core.CardVisualSchemes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public class CardVisualModule : VisualModule
    {
        public struct CardVisualContainer
        {
            //UI
            public Canvas CardCanvas;
            public Image ArtImage;

            //Mesh
            public MeshRenderer CardRenderer;
        }

        private Card card;
        
        public CardVisualContainer Container;
        
        private readonly HashSet<GameObject> features = new HashSet<GameObject>();
        private readonly HashSet<ParticleSystem> particles = new HashSet<ParticleSystem>();
        
        public CardVisualModule(Card target, CardVisualContainer initContainer, bool enabled = true) : base(target, enabled)
        {
            card = target;
            Container = initContainer;
        }

        public override void Refresh(EntityMeta meta, bool hard = false)
        {
            base.Refresh(meta, hard);
            
            var cardMeta = meta as CardMeta;
            Container.ArtImage.sprite = cardMeta.Art;
            var artRect = Container.ArtImage.GetComponent<RectTransform>();
            artRect.anchoredPosition3D = new Vector3(cardMeta.ArtTransform.x, cardMeta.ArtTransform.y, 0);
            artRect.sizeDelta = new Vector2(cardMeta.ArtTransform.z, cardMeta.ArtTransform.w);

            if (!hard) return;
            
            foreach (var feature in features)
            {
                MonoBehaviour.Destroy(feature);
            }

            foreach (var feature in card.MetaDescriptor.Features)
            {
                features.Add(ManagersRoot.Instance.Get<UIManager>()
                    .CreateUIObject(feature.UIObject , Container.CardCanvas.gameObject));
            }

            foreach (var particleSystem in particles)
            {
                MonoBehaviour.Destroy(particleSystem);
            }

            var rarity = ManagersRoot.Instance.Get<MetaManager>().Configuration.GetRarity(cardMeta.Rarity);
            var front = Container.CardCanvas.transform.Find("Front");
            
            Container.CardRenderer.material.SetColor("_MainColor", rarity.HDRColor);
            front.Find("Outline").GetComponent<Image>().material.SetColor("_Tint", rarity.HDRColor);
            var rarityUi = rarity.CreateUI(card);
            if (rarityUi == null) return;
            foreach (var ps in rarityUi)
            {
                var psc = ps.GetComponent<ParticleSystem>();
                if (psc != null)
                {
                    particles.Add(psc);
                }
            }
        }
    }
}