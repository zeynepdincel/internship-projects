using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Notepad
{
    class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public Note(int ıd, string title, string content, DateTime created, DateTime lastModified)
        {
            Id = ıd;
            Title = title;
            Content = content;
            Created = created;
            LastModified = lastModified;
        }
        public override string ToString()
        {
            return $"{Id} | {Title} | {Created:yyyy-MM-dd HH:mm}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            NoteManager noteManager = new NoteManager();
            int idCounter = 1;

            while (true)
            {
                Console.WriteLine("1. List notes");
                Console.WriteLine("2. Add note");
                Console.WriteLine("3. Delete note");
                Console.WriteLine("4. Update note");
                Console.WriteLine("5. Exit");
                Console.Write("Select an option: ");
                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        noteManager.ListNotes();
                        break;
                    case "2":
                        Console.Write("Enter title: ");
                        string title = Console.ReadLine();
                        Console.Write("Enter content: ");
                        string content = Console.ReadLine();
                        noteManager.AddNote(new Note(idCounter++, title, content, DateTime.Now, DateTime.Now));
                        break;
                    case "3":
                        Console.Write("Enter id: ");
                        int id = int.Parse(Console.ReadLine());
                        noteManager.DeleteNotes(id);
                        break;
                    case "4":
                        Console.Write("Enter id: ");
                        int id2 = int.Parse(Console.ReadLine());
                        Console.Write("Enter new title: ");
                        string newTitle = Console.ReadLine();
                        Console.Write("Enter new content: ");
                        string newContent = Console.ReadLine();
                        noteManager.UpdateNotes(id2, newTitle, newContent);
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }
    }
    class NoteManager
    {
        private const string FILE_NAME = "notes.txt";
        //public List<Note> LoadNotes()
        //{
        //    List<Note> notes = new List<Note>();
        //    if (File.Exists("notes.txt"))
        //    {
        //        var lines = File.ReadAllLines("notes.txt");
        //        foreach (var line in lines)
        //        {
        //            var parts = line.Split(' ');
        //            if (parts.Length == 5)
        //            {
        //                int id = int.Parse(parts[0]);
        //                string title = parts[1];
        //                string content = parts[2];
        //                DateTime created = DateTime.Parse(parts[3]);
        //                DateTime lastModified = DateTime.Parse(parts[4]);
        //                notes.Add(new Note(id, title, content,created,lastModified) { Created = created });
        //            }

        //        }
        //    }
        //    return notes;
        //}
        public static List<Note> LoadNotes()
        {
            List<Note> notes = new List<Note>();
            if (!File.Exists("notes.txt"))
            {
                Console.WriteLine("Not dosyası bulunamadı. Yeni bir dosya oluşturulacak.");
                return notes;
            }

            string[] lines = File.ReadAllLines("notes.txt");


            foreach (var line in lines)
            {
                Console.WriteLine("Dosyadan Okunan Satır: " + line); // Debug için ekledik.

                var parts = line.Split(' ');
                if (parts.Length == 5)
                {
                    int id;
                    DateTime createdAt, lastModified;

                    if (int.TryParse(parts[0].Trim(), out id) &&
                        DateTime.TryParse(parts[3].Trim(), out createdAt) &&
                        DateTime.TryParse(parts[4].Trim(), out lastModified)) // Yeni LastModified kontrolü
                    {
                        string title = parts[1].Trim();
                        string content = parts[2].Trim();

                        notes.Add(new Note(id, title, content, createdAt, lastModified)
                        {
                            Created = createdAt,
                            LastModified = lastModified // Yeni alanı ekledik
                        });
                    }
                }
            } return notes;
        }
        public void SaveNotes(List<Note> notes)
        {
            if (notes.Count == 0)
            {
                Console.WriteLine("Kaydedilecek not bulunamadı!");
                return;
            }

            List<string> lines = notes.Select(n => $"{n.Id} | {n.Title} | {n.Content} | {n.Created}").ToList();

            try
            {
                File.WriteAllLines("notes.txt", lines);
                Console.WriteLine("Notlar başarıyla kaydedildi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Dosyaya yazılırken hata oluştu: " + ex.Message);
            }
        }
        public void AddNote(Note note)
        {
            List<Note> notes = LoadNotes();
            notes.Add(note);
            SaveNotes(notes);
        }
        public void ListNotes()
        {
            List<Note> notes = LoadNotes();
            if(notes==null)
            {
                Console.WriteLine("No notes found");
                return;
            }
            foreach (var note in notes)
            {
                Console.WriteLine(note);

            }
        }
        public void DeleteNotes(int id)
        {
            List<Note> notes = LoadNotes();
            notes.RemoveAll(n => n.Id == id);
            SaveNotes(notes);
        }
        public void UpdateNotes(int id, string newTitle, string newContent)
        {
            List<Note> notes = LoadNotes();
            var note=notes.FirstOrDefault(n => n.Id == id);
            if (note == null)
            {
                Console.WriteLine("Note not found");
                return;
            }
            note.Title = newTitle;
            note.Content = newContent;
            note.LastModified = DateTime.Now;
            SaveNotes(notes);
        }
    }
    
}
