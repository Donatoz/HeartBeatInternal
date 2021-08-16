using System;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Metozis.Cardistry.Internal.UI.Grouping
{
    public class LayoutPlacer : UIEntity
    {
        public Vector3Distribution PositionOverIndex;
        public Curve RotationOverIndex;
        
        private void FixedUpdate()
        {
            Refresh();
        }

        protected virtual void Refresh()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);
                child.transform.localRotation = Quaternion.Euler(0, 0, RotationOverIndex.Evaluate((float)i / Mathf.Max(1, transform.childCount - 1)));
                child.transform.localPosition = PositionOverIndex.Evaluate((float) i / Mathf.Max(1, transform.childCount - 1));
            }
        }
    }
}