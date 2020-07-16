using System;

namespace State
{
    /// <summary>
    /// Amaç: Bir nesnenin durumunu kontrol etmek için kullanılan bir tasarım desenidir. ORM de kullanılan state durumları bu desene örnek verilebilir. 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();

            ModifiedState modifiedState = new ModifiedState();

            modifiedState.DoAction(context);

            AddState addState = new AddState();

            addState.DoAction(context);

            string state = context.GetState().ToString();

            Console.WriteLine(state);

            Console.Read();
        }
    }

    interface IState
    {
        void DoAction(Context context);
    }

    class Context
    {
        private IState state;

        public void SetState(IState state)
        {
            this.state = state;
        }

        public IState GetState()
        {
            return this.state;
        }
    }

    class ModifiedState : IState
    {
        public void DoAction(Context context)
        {
            Console.WriteLine("State: Modified");
            context.SetState(this);
        }

        public override string ToString()
        {
            return "Modified";
        }
    }

    class DeleteState : IState
    {
        public void DoAction(Context context)
        {
            Console.WriteLine("State: Deleted");
            context.SetState(this);
        }

        public override string ToString()
        {
            return "Deleted";
        }
    }

    class AddState : IState
    {
        public void DoAction(Context context)
        {
            Console.WriteLine("State: Added");
            context.SetState(this);
        }

        public override string ToString()
        {
            return "Added";
        }
    }
}
