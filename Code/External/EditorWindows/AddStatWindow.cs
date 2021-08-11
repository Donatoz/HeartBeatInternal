using System.Collections.Generic;
using Metozis.Cardistry.Internal.Meta.Core;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Metozis.Cardistry.Code.External.Editor.Windows
{
    public class AddStatWindow : OdinEditorWindow
    {
        [Title("Add custom stat", TitleAlignment = TitleAlignments.Centered, Bold = true, HorizontalLine = true)]
        public string StatName;
        public int BaseValue;
        public Vector2Int MinMax;

        private StatMeta meta;
        private List<StatMeta> container;
        
        public static void Open(StatMeta meta, List<StatMeta> container)
        {
            var wnd = GetWindow<AddStatWindow>();
            wnd.meta = meta;
            wnd.container = container;
        }
        
        [Button(ButtonSizes.Large)]
        private void Add()
        {
            meta.StatName = StatName;
            meta.CurrentValue = BaseValue;
            meta.MinMax = MinMax;
            container.Add(meta);
            Close();
        }
    }
}