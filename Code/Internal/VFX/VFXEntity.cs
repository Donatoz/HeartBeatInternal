using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.VFX
{
    public abstract class VFXEntity : SerializedMonoBehaviour
    {
        public bool Complete;
        public bool Started;

        public Action OnStart;
        public Action OnDeath;

        public Dictionary<string, Action> CustomEvents = new Dictionary<string, Action>();

        protected virtual void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            OnDeath?.Invoke();
        }

        public void Finish()
        {
            Complete = true;
        }

        public virtual void Place()
        {
            gameObject.SetActive(true);
            Started = true;
            OnStart?.Invoke();
        }
    }
}