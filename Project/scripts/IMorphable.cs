namespace Gamelogic
{
    /// <summary>
    /// Interface that makes an entitiy have 2 alternatives, morphed and not
    /// </summary>
    public interface IMorphable
    {
        public bool IsMorphed {get; set;}
        public void ToggleMorph();
    }
}