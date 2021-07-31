using System;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Management;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.UI
{
    public abstract class UIEntity : SerializedMonoBehaviour
    {
        public string BindId;

        protected virtual void Awake()
        {
            UIManager.Instance.RegisterEntity(this);
        }

        protected virtual void OnDestroy()
        {
            UIManager.Instance.ForgetEntity(this);
        }
    }
}