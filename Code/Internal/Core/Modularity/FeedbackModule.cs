using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Entities;
using MoreMountains.Feedbacks;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public class FeedbackModule : Module
    {
        private readonly Dictionary<string, MMFeedbacks> feedbacksMap;
        
        public FeedbackModule(Entity target, bool enabled = true) : base(target, enabled)
        {
            feedbacksMap = new Dictionary<string, MMFeedbacks>();
        }

        public void Play()
        {
            
        }
    }
}