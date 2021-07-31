using System.Linq;
using Metozis.Cardistry.Internal.Meta.Descriptors;
using UnityEditor;

namespace Metozis.Cardistry.Internal.DataHandling.Databases
{
    public static class CardDatabase
    {
        public static CardMetaDescriptor GetCard(string id)
        {
            var cards = AssetDatabase.FindAssets("t:CardMetaDescriptor");
            return cards
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<CardMetaDescriptor>)
                .FirstOrDefault(card => card.Meta.EntityId == id);
        }
    }
}