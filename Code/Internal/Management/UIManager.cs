using System;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.GameFlow;
using Metozis.Cardistry.Internal.Meta.UI;
using Metozis.Cardistry.Internal.UI;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace Metozis.Cardistry.Internal.Management
{
    public class UIManager : SerializedMonoBehaviour
    {
        public static UIManager Instance => ManagersRoot.Instance.Get<UIManager>();
        
        public GameObject UIObjectTemplate;
        public Dictionary<string, GameObject> ObjectsCache;
        public Dictionary<string, UIObjectMetaDescriptor> UIObjectsCache;
        
        [ColorUsage(true, true)]
        public Color AllyUnitColor;
        [ColorUsage(true, true)]
        public Color EnemyUnitColor;

        private Dictionary<string, UIEntity> entityCache = new Dictionary<string, UIEntity>();
        public IReadOnlyDictionary<string, UIEntity> EntityCache => entityCache;

        public Action<UIEntity> OnEntityRegistered;

        public void RegisterEntity(UIEntity e)
        {
            entityCache[e.BindId] = e;
            OnEntityRegistered?.Invoke(e);
        }

        public void ForgetEntity(UIEntity e)
        {
            if (entityCache.ContainsKey(e.BindId))
            {
                entityCache.Remove(e.BindId);
            }
        }

        public GameObject CreateUIObject(UIObjectMetaDescriptor descriptor, GameObject parent)
        {
            var obj = Instantiate(UIObjectTemplate, parent.transform);
            var rect = obj.GetComponent<RectTransform>();
            var image = obj.GetComponent<Image>();

            obj.name = descriptor.GameojbectName;
            
            var currentParent = parent.transform;
            if (!descriptor.Root.IsNullOrWhitespace())
            {
                foreach (var s in descriptor.Root.Split('/'))
                {
                    currentParent = currentParent.transform.Find(s);
                    if (currentParent == null)
                    {
                        Debug.LogError($"Tried to find child {s} - not found.");
                        break;
                    }
                }
            }

            obj.transform.parent = currentParent.transform;

            image.sprite = descriptor.ImageSprite;
            image.color = descriptor.Tint;
            
            rect.anchorMin = descriptor.AnchorMin;
            rect.anchorMax = descriptor.AnchorMax;
            rect.pivot = descriptor.Pivot;
            rect.anchoredPosition3D = descriptor.RectPosition;
            rect.eulerAngles = new Vector3(90, 0, descriptor.RotationZ);
            rect.sizeDelta = descriptor.RectSize;
            rect.localScale = descriptor.RectScale;

            foreach (var feature in descriptor.Features)
            {
                feature.ModifyUI(obj);
            }

            foreach (var child in descriptor.Children)
            {
                CreateUIObject(child, obj);
            }
            
            return obj;
        }
    }
}