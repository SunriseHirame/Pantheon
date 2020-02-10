using System;

namespace Hirame.Pantheon
{
    public interface ICommand
    {
        void Resolve ();
    }
    
    public struct Command : ICommand
    {
        private readonly Action action;
        
        public void Resolve ()
        {
            action?.Invoke ();
        }
    }
    
}