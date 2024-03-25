using System.IO;
using Microsoft.VisualBasic.FileIO;
using PetMan.Data;
using PetMan.Framework;
using PetMan.Models;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Quality;
using PdfSharp.Snippets.Font;
using System.Diagnostics;

namespace PetMan.Controller
{
    public class ShellController : ControllerBase
    {
        protected Repository Repository;
        public ShellController(DataAccess context)
        {
            Repository = new Repository(context);
        }
        public void Shell(string folder, string ext = "*.jpg") // shell ? path = .../ThuVienPet/ dog || cat 
        {
            if (!Directory.Exists(folder))
            {
                Error("Folder not found");
                return;
            }
            string [] dirs = Directory.GetDirectories(folder);
            foreach (string dir in dirs)
            {
                if (ext == "*.jpg" || ext == "*.jpeg" || ext == "*.png")
                {
                    var files = Directory.GetFiles(dir, ext);
                    Repository.InsertPet(
                        new Pet { 
                            NickName = Path.GetFileName(dir), 
                            Species = Directory.GetParent(dir).Name == "cat" ? Species.cat : Species.dog , 
                            Image = String.Join(',', files)
                        });
                }
            }

            if (dirs.Length > 0)
            {
                Success($"{dirs.Length} items(s) found!");
                return;
            }
            Info("Sorry, No item found!");
        }
        public void ToPdf(int id, string path)
        {
            var pet = Repository.Select(id);

            if (Capabilities.Build.IsCoreBuild)
            GlobalFontSettings.FontResolver = new FailsafeFontResolver();
            var document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            document.Info.Subject = "Just a simple Hello-World program.";

            // Create an empty page in this document.
            var page = document.AddPage();

            // Get an XGraphics object for drawing on this page.
            var gfx = XGraphics.FromPdfPage(page);

            // Create a font.
            var font = new XFont("Times New Roman", 20, XFontStyleEx.Bold);
            

            gfx.DrawString("Pet Information:", font, XBrushes.Red, new XPoint(200,70));
            gfx.DrawLine(new XPen(XColor.FromArgb(50,30,200)), new XPoint(100, 100), new XPoint(500, 100));
        
            gfx.DrawString($"NickName: {pet.NickName}", font, XBrushes.Purple, new XRect(0, 140, page.Width, page.Height), XStringFormats.CenterLeft);
            gfx.DrawString($"Age: {pet.Age}", new XFont("Times New Roman", 10), XBrushes.Purple, new XRect(0, 190, page.Width, page.Height));
            gfx.DrawString($"Physical Description: {pet.PhysicalDescription}", new XFont("Times New Roman", 10),XBrushes.Purple, new XRect(0, 230, page.Width, page.Height), XStringFormats.CenterLeft);
           
            // Save the document...
            document.Save(path);
        }
    }
}