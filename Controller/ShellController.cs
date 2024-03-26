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
using System.ComponentModel;

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
        public void ToPdf(int id, string path = "")
        {
            var pet = Repository.Select(id);

            if (Capabilities.Build.IsCoreBuild)
            GlobalFontSettings.FontResolver = new FailsafeFontResolver();
            var document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";
            document.Info.Subject = "Just a simple Hello-World program.";

            // Create an empty page in this document.
            var page = document.AddPage();
            page.Size = PageSize.A4;

            // Get an XGraphics object for drawing on this page.
            var gfx = XGraphics.FromPdfPage(page);

            // Create a font.
            var font = new XFont("Times New Roman", 20, XFontStyleEx.Bold);
            var fontBody = new XFont("Times New Roman", 10);

            gfx.DrawString("Pet Information", font, XBrushes.Red, new XPoint(230,70));
            gfx.DrawLine(new XPen(XColor.FromArgb(50,30,200)), new XPoint(100, 100), new XPoint(500, 100));
        
            gfx.DrawString($"NickName: {pet.NickName}", fontBody, XBrushes.Black, new XRect(0, -300, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString($"Age: {pet.Age}", fontBody, XBrushes.Black, new XRect(0, -275, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString($"Physical Description: {pet.PhysicalDescription}", fontBody, XBrushes.Black, new XRect(0, -250, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString($"Personality Description: {pet.PersonalityDescription}", fontBody, XBrushes.Black, new XRect(0, -225, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString($"Adopt: {pet.Adopt}", fontBody, XBrushes.Black, new XRect(0, -200, page.Width, page.Height), XStringFormats.Center);
            gfx.DrawString($"Species: {pet.Species}", fontBody, XBrushes.Black, new XRect(0, -175, page.Width, page.Height), XStringFormats.Center);
            
            var petImage = pet.Image;
            var startIndex = petImage.IndexOf(',');
            var images = petImage.Split(new [] { ','}, startIndex);
            int currentY = 290;
            int width = 180;
            int height = 180;

            for (int i = 0; i < images.Length; i ++)
            {
                
                if (i % 2 == 0) 
                {
                    gfx.DrawImage(XImage.FromFile(images[i]), 100, currentY, width, height);
                }
                else
                {
                    gfx.DrawImage(XImage.FromFile(images[i]), 320, currentY, width, height);
                    currentY += 200;
                }
            }
            // Save the document...
            document.Save(path);

            Repository.UpdatePet(id, new Pet { 
                                        NickName = pet.NickName, 
                                        Age = pet.Age,
                                        Species = pet.Species,
                                        PhysicalDescription = pet.PhysicalDescription, 
                                        PersonalityDescription = pet.PersonalityDescription,
                                        Adopt = pet.Adopt,
                                        FileReport = path
                                        });

            Success($"Report Pet {pet.NickName} saved!");
        }
        public void ReadReport(int id)
        {
            var pet = Repository.Select(id);
            if (pet == null)
            {
                Error("Pet not found!");
                return;
            }
            if (!File.Exists(pet.FileReport))
            {
                Error("Report not found!");
                return;
            }
            try {
            Process myProc = new Process();
                myProc.StartInfo = new ProcessStartInfo(pet.FileReport) { UseShellExecute = true };
                myProc.Start();
                Success($"You are reading report the pet '{pet.NickName}'");
            } 
            catch(Win32Exception w) {
                Console.WriteLine(w.Message);
                Console.WriteLine(w.ErrorCode.ToString());
                Console.WriteLine(w.NativeErrorCode.ToString());
                Console.WriteLine(w.StackTrace);
                Console.WriteLine(w.Source);
                Exception e=w.GetBaseException();
                Console.WriteLine(e.Message);
            }
        }
        public void Clear(bool process = false)
        {
            if (!process)
            {
                Confirm("Do you want to clear the shell ?", "shell clear");
            }
            Repository.Clear();
            Success("The shell has been cleared!");
        }
    }
}