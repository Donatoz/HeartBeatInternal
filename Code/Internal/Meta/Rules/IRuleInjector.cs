namespace Metozis.Cardistry.Internal.Meta.Rules
{
    public interface IRuleInjector
    {
        void ConfigureInput();
        void ConfigureOrders();
        void InitializeUI();
        void InitializeUIEvents();
    }
}