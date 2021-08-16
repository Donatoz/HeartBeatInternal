using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Metozis.Cardistry.Internal.UI.Events
{
    public abstract class UIEvent : SerializedMonoBehaviour
    {
        public UIEventReceiver Receiver;
        public UIEventEmissionContext EmissionContext;

        public void Invoke()
        {
            Receiver.Receive(EmissionContext.EventName);
        }
    }
}