namespace Hirame.Pantheon
{
    public sealed class LifeCycleOnEnable : LifeCycleEventBase
    {
        private void OnEnable ()
        {
            RaiseEvent ();
        }
    }

}