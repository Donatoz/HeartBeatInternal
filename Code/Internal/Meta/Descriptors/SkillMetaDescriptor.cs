using System.Collections.Generic;
using Metozis.Cardistry.Internal.Meta.Core.Units;
using Metozis.Cardistry.Internal.Meta.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Descriptors
{
    [CreateAssetMenu(fileName = "New skill", menuName = "Metozis/Meta/Skill")]
    public class SkillMetaDescriptor : SerializedScriptableObject, IMetaDescriptor<SkillMeta>
    {
        public SkillMeta Meta;
        public SkillMeta GetMeta()
        {
            return Meta;
        }
    }
}