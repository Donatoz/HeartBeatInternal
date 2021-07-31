using System;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Meta;
using Metozis.Cardistry.Internal.VFX;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Management
{
    public class ManagersRoot : MonoBehaviour
    {
        #region Singleton

        private static ManagersRoot instance;

        public static ManagersRoot Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.Find("Managers").GetComponent<ManagersRoot>();
                }

                return instance;
            }
        }

        #endregion

        private readonly Dictionary<Type, MonoBehaviour> managers = new Dictionary<Type, MonoBehaviour>();

        private void Awake()
        {
            managers[typeof(InputManager)] = GetComponent<InputManager>();
            managers[typeof(SessionManager)] = GetComponent<SessionManager>();
            managers[typeof(UIManager)] = GetComponent<UIManager>();
            managers[typeof(MetaManager)] = GetComponent<MetaManager>();
            managers[typeof(SelectionManager)] = GetComponent<SelectionManager>();
            managers[typeof(VisualScheduler)] = GetComponent<VisualScheduler>();
        }

        public T Get<T>() where T : MonoBehaviour
        {
            return managers[typeof(T)] as T;
        }
    }
}