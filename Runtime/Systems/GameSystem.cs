namespace Hirame.Pantheon.Core
{
    public abstract class GameSystem<T> : GameSystem where T : GameSystem<T>, new ()
    {
        private static T instance;

        public bool IsActive { get; private set; } = true;
        
        public static T GetOrCreate ()
        {
            return instance ?? CreateNewInstance ();
        }

        private static T CreateNewInstance ()
        {
            var newInstance = new T ();
            
            if (newInstance is IUpdate updateable)
                UpdateLoop.RegisterForUpdate (updateable);

            instance.OnCreated ();
            return newInstance;
        }
        
        public static void Dispose ()
        {
            instance.OnDisposed ();
            instance = null;
        }
    }

    public abstract class GameSystem
    {
        protected virtual void OnCreated () { }
        
        protected virtual void OnDisposed () { }
    }
}