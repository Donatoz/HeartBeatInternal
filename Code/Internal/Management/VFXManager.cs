using System.Collections.Generic;
using Metozis.Cardistry.Internal.VFX;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Management
{
    public class VFXManager : MonoBehaviour
    {
        public static VFXManager Instance => ManagersRoot.Instance.Get<VFXManager>();
        
        public Dictionary<string, VFXEntity> Cache;
    }
}