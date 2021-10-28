using System;
using System.Collections.Generic;
using System.Linq;

namespace U.Reactor
{
    public class UseAddChilds : IuseAddChilds
    {
        public event EventHandler<OnChildAddedEventArgs> OnChildAdded;

        public void AddChild(Func<REbase> child)
        {
            OnChildAdded?.Invoke(this, new OnChildAddedEventArgs(child));
        }

    }

    public class OnChildAddedEventArgs : EventArgs
    {
        public OnChildAddedEventArgs(Func<REbase> child)
        {
            this.child = child;
        }

        public Func<REbase> child { get; private set; }
    }


    public interface IuseAddChilds
    {
        event EventHandler<OnChildAddedEventArgs> OnChildAdded;
    }

}
