namespace Hirame.Pantheon
{
    public sealed class LifeCycleOnDisable : LifeCycleEventBase
    {
        private void OnDisable ()
        {
            RaiseEvent ();
        }
    }

}