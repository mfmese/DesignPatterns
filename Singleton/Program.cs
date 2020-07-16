using System;

namespace Singleton
{
    /// <summary>
    /// Amaç:Amaç: Bir nesne örneğinden sadece bir defa üretilip her zaman kullanılmasını amaçlar. Singleton olarak oluşturulan nesne static olduğu için bellekte her zaman yer kaplamaktadır. 
    /// Bu nedenle sürekli olarak tüketilmesi gereken ve birçok kişi tarafından tüketilmesi gereken durumlarda singleton kullanılmalıdır. 
    /// Nesne içeriği sürekli değişmeyen durumlarda singleton kullanılmalıdır çünkü static olduğundan dolayı güncellenmesi gereken durumlarda IIS restart edilmesi gerekecektir. 
    /// Multithread işlemlerde farklı kullanıcılar aynı anda nesne create etmek isterse nesne birden fazla kez oluşmuş olacaktır. Bu nedenle nesne create edilmesi süreci lock edilmelidir.
    /// Kullanımı: var object = Object.CreateAsSingleton()
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //Görüldüğü üzere CustomerManager nesnemizi new keyword ile create edemiyoruz çünkü CustomerManager nesnemizin constructor'ını private olarak işaretledik.
            //CustomerManager customerManager = new CustomerManager();


            //Gerçek kullanımı aşağıdaki gibi CreateAsSingleton fonksiyonu çağrılarak CustomerManager nesnesi yoksa create edilir varsa bellekte var olan CustomerManager nesnesi getirilir.
            var customerManager = CustomerManager.CreateAsSingleton();
            customerManager.Save();

            //Aşağıdaki gibi CustomerManager nesnesi tekrar create edilmeye çalışıldığında nesne bir üstte create edilen nesneyi çağırdığı görülmektedir.
            var customerManager2 = CustomerManager.CreateAsSingleton();
            customerManager2.Save();

            Console.Read();
        }
    }

    class CustomerManager
    {
        //CustomerManager nesnesini bellekte sürekli olarak tutulmasını sağlamak için static bir değişken tanımlıyoruz.
        private static CustomerManager customerManager;

        private static object lockObject = new object();

        //CustomerManager nesnesinin dışarıdan new yapılarak create edilmesini engellemek için constructor private olarak işaretliyoruz.
        private CustomerManager()
        {

        }

        public static CustomerManager CreateAsSingleton()
        {
            //Multithread işlemlerde aynı anda farklı kullanıcılar CustomerManager nesnesini create etmek isteyebilir. Bu durumda CustomerManager nesnesi birden fazla kez oluşmuş olacaktır.
            //Bu durumu önlemek (Thread safe yapmak) için lock (lockObject) işlemini yapıyoruz. Bu sayede birinci kullanıcı create işlemini yaparken diğer kullanıcı bloğu beklemek zorundadır.
            lock (lockObject)
            {
                //CustomerManager nesnesi daha önceden create edilmişse var olanı döndür yoksa yeni bir CustomerManager nesnesi oluştur ve static değişkenimize atama yaparak geri döndür.
                if (customerManager == null)
                {
                    Console.WriteLine("CustomerManager nesnesi ilk defa create ediliyor");
                    customerManager = new CustomerManager();
                }
                else
                {
                    Console.WriteLine("CustomerManager nesnesi daha önceden create edilmiş");
                }
            }
            return customerManager;
        }

        //CustomerManager nesnesi static olarak dışarıya açıldığı için methodları static olmamalıdır.
        public void Save()
        {
            Console.WriteLine("Müşteri başarıyla kaydedildi");
        }
    }
}
