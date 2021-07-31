using Metozis.Cardistry.Internal.Core.Reactive;

namespace Metozis.Cardistry.Internal.VFX
{
    public class TimedEffect : VFXEntity
    {
        public float DeathDelay;
        public float Duration;

        private void Start()
        {
            ReactiveUtils.Delay(Duration + DeathDelay, Finish);
        }
    }
}