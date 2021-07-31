using System.Linq;
using Metozis.Cardistry.Internal.Meta.Descriptors;
using UnityEditor;

namespace Metozis.Cardistry.Internal.DataHandling.Databases
{
    public static class UnitDatabase
    {
        public static UnitMetaDescriptor GetUnit(string id)
        {
            var units = AssetDatabase.FindAssets("t:UnitMetaDescriptor");
            return units
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<UnitMetaDescriptor>)
                .FirstOrDefault(unit => unit.Meta.EntityId == id);
        }
    }
}