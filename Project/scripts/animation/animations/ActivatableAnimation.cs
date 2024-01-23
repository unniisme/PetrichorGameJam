using Gamelogic;

namespace AnimationHandling
{
    public partial class ActivatableAnimation : AnimationHandler
    {
        AnimationState inactive;
        AnimationState active;
        public override void _Ready()
        {
            base._Ready();

            inactive = AddState("Inactive", "inactive");
            active = AddState("Active", "active");

            GetParent<IActivatable>().OnActivityChangedEvent += OnActivityChangedEventHandler;
        }

        private void OnActivityChangedEventHandler(bool val)
        {
            if (val)
            {
                Transition(active);
            }
            else
            {
                Transition(inactive);
            }
        }
    }
}