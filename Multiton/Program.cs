using System;
using System.Collections.Generic;
using System.Linq;

namespace Multiton
{
    class Program
    {
        static void Main(string[] args)
        {
            Camera camera1 = Camera.CreateSingleton("NIKON");
            Camera camera2 = Camera.CreateSingleton("CANON");
            Camera camera3 = Camera.CreateSingleton("NIKON");
            Camera camera4 = Camera.CreateSingleton("NIKON");


            Console.WriteLine("Camera:{0}, Id: {1}",camera1.GetName(), camera1.Id);
            Console.WriteLine("Camera:{0}, Id: {1}", camera2.GetName(), camera2.Id);
            Console.WriteLine("Camera:{0}, Id: {1}", camera3.GetName(), camera3.Id);
            Console.WriteLine("Camera:{0}, Id: {1}", camera4.GetName(), camera4.Id);

            Console.Read();
        }
    }

    class Camera
    {
        static Dictionary<string, Camera> _cameras = new Dictionary<string, Camera>();
        private static object _lock = new object();
        public Guid Id { get; set; }   

        private Camera()
        {
            Id = Guid.NewGuid();
        }

        public static Camera CreateSingleton(string name)
        {
            lock (_lock)
            {
                if (!_cameras.ContainsKey(name))
                {
                    _cameras.Add(name, new Camera());
                }
            }

            return _cameras[name];
        }

        public string GetName()
        {
            return _cameras.FirstOrDefault(x => x.Value == this).Key;
        }
    }
}
