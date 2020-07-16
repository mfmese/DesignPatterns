using System;

namespace Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            Book book = new Book
            {
                Author = "Victor Hugo",
                Title = "Deniz İşçileri",
                Isbn = "11111"
            };

            book.ShowBook();

            CareTaker history = new CareTaker();
            history.Memento = book.CreateUndo();

            book.Isbn = "2222";
            book.ShowBook();

            book.RestoreFromUndo(history.Memento);

            book.ShowBook();

            Console.Read();
        }
    }

    class Book
    {
        private string _isbn; 
        private string _title;
        private string _author;
        public DateTime LastEdited { get; set; }

        public string Author { get => _author; set { _author = value; SetLastEdited(); } }
        public string Title { get => _title; set { _title = value; SetLastEdited(); } }
        public string Isbn { get => _isbn; set { _isbn = value; SetLastEdited(); } }

        private void SetLastEdited()
        {
            LastEdited = DateTime.UtcNow;
        }

        public Memento CreateUndo()
        {
            return new Memento(_isbn, _title, _author, LastEdited);
        }

        public void RestoreFromUndo(Memento memento)
        {
            _isbn = memento.Isbn;
            _title = memento.Title;
            _author = memento.Author;
            LastEdited = memento.LastEdited;
        }

        public void ShowBook()
        {
            Console.WriteLine("Isbn: {0}, Title: {1}, Author: {2}, LastEdited: {3} ", _isbn, _title, _author, LastEdited);
        }
    }

    class Memento
    {
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime LastEdited { get; set; }

        public Memento(string isbn, string title, string author, DateTime lastEdited)
        {
            Isbn = isbn;
            Title = title;
            Author = author;
            LastEdited = lastEdited;
        }        
    }

    class CareTaker
    {
        public Memento Memento { get; set; }
    }
}
