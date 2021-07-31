using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta;
using Metozis.Cardistry.Internal.Meta.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public class UnitVisualModule : VisualModule
    {
        public struct UnitVisualContainer
        {
            //UI
            public Canvas UnitCanvas;
            public Image ArtImage;
            
            //Mesh
            public GameObject[] GlowyPieces;
        }

        public UnitVisualContainer Container;

        public UnitVisualModule(Entity target, bool enabled = true) : base(target, enabled)
        {
        }

        public override void Refresh(EntityMeta meta, bool hard = false)
        {
            base.Refresh(meta, hard);

            var unitMeta = meta as UnitMeta;
            
            var rarity = ManagersRoot.Instance.Get<MetaManager>().Configuration.GetRarity(unitMeta.Rarity);
            Container.ArtImage.sprite = unitMeta.Art;
            var imageRect = Container.ArtImage.GetComponent<RectTransform>();
            imageRect.anchoredPosition3D = new Vector3(unitMeta.ArtTransform.x, unitMeta.ArtTransform.y, 0);
            imageRect.sizeDelta = new Vector2(unitMeta.ArtTransform.z, unitMeta.ArtTransform.w);

            foreach (var piece in Container.GlowyPieces)
            {
                piece.GetComponent<MeshRenderer>().material.SetColor("_MainColor", rarity.HDRColor);
            }
        }
    }
}