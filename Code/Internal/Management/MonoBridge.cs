using UnityEngine;

namespace Metozis.Cardistry.Internal.Management
{
    public class MonoBridge : MonoBehaviour
    {
        public static MonoBehaviour Instance { get; private set; }

        private void Start()
        {
            Instance = this;
        }
    }
}