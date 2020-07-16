using System;

namespace Bridge
{
    /// <summary>
    /// Amaç: Bir nesnenin içerisinde soyutlanabilir nesneler varsa onları yönetmeye yarar.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.MessageSenderBase = new MailSender();
            customerManager.Update();

            Console.Read();
        }
    }

    public abstract class MessageSenderBase
    {
        public void Save()
        {
            Console.WriteLine("Message saved");
        }

        public abstract void Send(Body body);
    }

    public class Body
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

    public class MailSender : MessageSenderBase
    {
        public override void Send(Body body)
        {
            Console.WriteLine("Message send by MailSender " + body.Title);
        }
    }

    public class SmsSender : MessageSenderBase
    {
        public override void Send(Body body)
        {
            Console.WriteLine("Message send by SmsSender " + body.Title);
        }
    }

    public class CustomerManager
    {
        public MessageSenderBase MessageSenderBase { get; set; }
        public void Update()
        {
            MessageSenderBase.Send(new Body { Title = "Title" });

            Console.WriteLine("Customer updated");
        }
    }
}
