namespace Metozis.Cardistry.Internal.Core.Interaction
{
    public interface ISelectable
    {
        bool Selected { get; }
        void Select();
        void Deselect();
    }
}