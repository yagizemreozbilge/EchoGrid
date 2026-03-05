namespace EchoGrid.Interaction
{
    using Player;
    
    // Any object in the game that the player (or an echo clone) can interact with must implement this interface.
    // This allows decoupled architectures instead of hardcoding Switch, Door etc.
    public interface IInteractable
    {
        bool IsInteractable { get; }
        void Interact(PlayerController player);
    }
}
