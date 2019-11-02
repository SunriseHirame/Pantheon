namespace Hirame.Pantheon.Core
{
    public abstract class GameSystem<T> : GameSystem where T : GameSystem<T>, new ()
    {
        private static T instance;
        
        public static T GetOrCreate ()
        {
            if (instance != null)
                return instance;
            
            instance = new T ();
            
            if (instance is IUpdate updateable)
                UpdateLoop.RegisterForUpdate (updateable);
            
            return instance;
        }
    }

    public abstract class GameSystem
    {
    }
}