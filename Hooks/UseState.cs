using System;
using System.Collections.Generic;
using System.Linq;

namespace U.Reactor
{
    public class UseState<TValue> : IuseState
    {

        private Stack<TValue> statesQueue = new Stack<TValue>();
        public TValue value => statesQueue.Peek();

        public event EventHandler OnStateChange;


        public UseState()
        {
            statesQueue.Push(default(TValue));
        }

        public UseState(TValue value)
        {
            statesQueue.Push(value);
        }

        public void SetState(TValue value)
        {

            statesQueue.Push(value);
            
            OnStateChange?.Invoke(this, new EventArgs());
        }

        public void SetState()
        {
            OnStateChange?.Invoke(this, new EventArgs());
        }

        public void PrevState()
        {
            if(statesQueue.Count()>1)
                statesQueue.Pop();

            OnStateChange?.Invoke(this, new EventArgs());
        }

        public void PrevState(int number)
        {
            if (number < 1)
                return;

            for (int i = 0; i < number; i++)
            {
                if (statesQueue.Count() > 1)
                    statesQueue.Pop();
            }

            OnStateChange?.Invoke(this, new EventArgs());
        }

    }

    public interface IuseState
    {
        event EventHandler OnStateChange;
    }

}
