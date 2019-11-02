namespace Hirame.Pantheon
{
    public interface IUpdate
    {
    }
    
    public interface IUpdateListener : IUpdate
    {
        void OnUpdate ();
    }

    public interface IWrapUpUpdate : IUpdate
    {
        void OnWarpUpUpdate ();
    }
    
    public interface IEarlyUpdate : IUpdate
    {
        void OnEarlyUpdate ();
    }

    public interface ICameraUpdate : IUpdate
    {
        void OnCameraUpdate ();
    }
    
    public interface IGameSystemUpdate : IUpdate
    {
        void OnGameSystemUpdate ();
    }
}