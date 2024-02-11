using dz3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Serialization;

namespace dz3.Controllers
{
    public class HomeController : Controller
    {
        private const string filePath = "notes.xml";

        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Create()
        {
            Note note = new Note()
            {
                Title = "My first note",
                Text = "This is my first note",
                CreationDate = DateTime.Now,
                Tags = new List<string> { "first", "note" }
            };
            return View(note);
        }

        public ActionResult Show(Note note)
        {

            ViewData["MyMessage"] = "Hello World";
            note.CreationDate = DateTime.Now;
            SaveNoteToFile(note);
            return View("Create", note);
        }

        public IActionResult ShowNotes()
        {
            return View();
        }

        public IActionResult LoadFromFile()
        {
            List<Note> notes = LoadNotesFromFile();
            if (notes == null)
            {
                return View("ShowNotes", new List<Note>());
            }
            return View("ShowNotes", notes);
        }

        private void SaveNoteToFile(Note note)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Note));

                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    serializer.Serialize(writer, note);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving note to file: {ex.Message}");
            }
        }

        private List<Note> LoadNotesFromFile()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Note));

                using (StreamReader reader = new StreamReader(filePath))
                {
                    List<Note> notes = new List<Note>();
                    while (!reader.EndOfStream)
                    {
                        Note note = (Note)serializer.Deserialize(reader);
                        notes.Add(note);
                    }
                    return notes;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading notes from file: {ex.Message}");
                return new List<Note>();
            }
        }   


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
