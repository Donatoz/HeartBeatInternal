using Metozis.Cardistry.Internal.Core.Reactive;
using UnityEngine;


namespace Metozis.Cardistry.Internal.UI
{
    public abstract class UIGraphics : UIEntity
    {
        public ReactiveValue<Color> Color { get; protected set; }
        public ReactiveValue<Vector2> RectSize { get; protected set; }
        public ReactiveValue<float> Rotation { get; protected set; }
        
        // For inspector
        [SerializeField]
        protected bool readInitialValues;

        protected override void Awake()
        {
            base.Awake();
            
            Color = new ReactiveValue<Color>(UnityEngine.Color.white);
            RectSize = new ReactiveValue<Vector2>(new Vector2(1, 1));
            Rotation = new ReactiveValue<float>(0);
        }
    }
}