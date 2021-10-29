using System;
using UnityEngine;

namespace U.Reactor
{
    public class UseTrigger : IuseTrigger
    {

        public UInt32 id_;
        public uint id => id_;
        public event EventHandler<OnTriggerEventArgs> OnTrigger;


        public UseTrigger()
        {
            id_ = S.NewIdShort();
        }


        public void Trigger()
        {
            OnTrigger?.Invoke(this, new OnTriggerEventArgs(id_));
        }

        public class Hook
        {
            public IuseTrigger hook { get; set; }
            public Action<REbaseSelector> OnTrigger {get; set; }
        }


    }

    public class OnTriggerEventArgs : EventArgs
    {
        public OnTriggerEventArgs(UInt32 id)
        {
            this.id = id;
        }

        public UInt32 id { get; private set; }
    }



    public interface IuseTrigger
    {
        UInt32 id { get; }
        event EventHandler<OnTriggerEventArgs> OnTrigger;
    }

}
