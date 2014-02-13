using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarseerPhysics;
using SpriteUtility.Data;
using SpriteUtility.Services;

namespace SpriteUtility.Stubs
{
    public class ImageDataFactory
    {
        public static void ImportFromExistingDirectoryDialog(Document document)
        {
            var dialog = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                ShowNewFolderButton = false,
                SelectedPath = MainForm.Preferences.LastFolderBrowsed
            };

            ImageViewer.Paused = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                MainForm.Preferences.LastFolderBrowsed = dialog.SelectedPath;
                MainForm.Preferences.CommitChanges();

                var rootFolder = ImportFromFolder(dialog);
                document.Folders.Add(rootFolder);
            }

            ImageViewer.Paused = false;
        }

        public static void ImportFromExistingDirectoryDialog(Folder folder)
        {
            var dialog = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                ShowNewFolderButton = false,
                SelectedPath = MainForm.Preferences.LastFolderBrowsed
            };

            ImageViewer.Paused = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                MainForm.Preferences.LastFolderBrowsed = dialog.SelectedPath;
                MainForm.Preferences.CommitChanges();

                var rootFolder = ImportFromFolder(dialog);
                folder.Folders.Add(rootFolder);
            }

            ImageViewer.Paused = false;
        }

        private static Folder ImportFromFolder(FolderBrowserDialog dialog)
        {
            var rootDirectory = dialog.SelectedPath;
            var rootFolder = new Folder();
            rootFolder.Name = Path.GetFileNameWithoutExtension(rootDirectory);

            var directories = Directory.GetDirectories(rootDirectory);

            List<string> images;
            foreach (var directory in directories)
            {
                var folder = new Folder {Name = Path.GetFileNameWithoutExtension(directory)};

                images = Directory.GetFiles(directory, "*.png", SearchOption.TopDirectoryOnly).ToList();
                images.AddRange(Directory.GetFiles(directory, "*.gif", SearchOption.TopDirectoryOnly));

                foreach (var filename in images)
                {
                    var imageData = CreateFromFilename(filename);
                    folder.Add(imageData);
                }

                rootFolder.Add(folder);
            }

            images = Directory.GetFiles(rootDirectory, "*.png", SearchOption.TopDirectoryOnly).ToList();
            images.AddRange(Directory.GetFiles(rootDirectory, "*.gif", SearchOption.TopDirectoryOnly));

            foreach (var filename in images)
            {
                var imageData = CreateFromFilename(filename);
                rootFolder.Add(imageData);
            }
            return rootFolder;
        }

        public static ImageData CreateFromFilename(string filename)
        {
            var imageData = new ImageData(filename);

            // Since we are adding new images we can stub in some
            //    conventional defaults (currently for trimmed frames only)

            if (MainForm.Preferences.TrimToMinimalNonTransparentArea)
            {
                foreach (var frame in imageData.Frames)
                {
                    AddAttackBoxStub(frame);
                    AddClippingBoxStub(frame);
                    AddPlatformBoxStub(frame);
                    AddDefaultFootBox(frame);
                    AddBodyTrace(frame);
                    SetNaturalCenter(frame);
                }
            }
            return imageData;
        }

        private static void AddBodyTrace(ImageFrame frame)
        {
            using (var ms = new MemoryStream(frame.Data))
            {
                var imageBitmap = Image.FromStream(ms);
                var errorBuilder = new StringBuilder();
                var shape = TraceService.CreateSimpleShape(imageBitmap, 2000, errorBuilder);

                if (shape != null)
                {
                    var bodyGroup = new PolygonGroup(frame) { Name = "Body" };
                    var count = 1;
                    foreach (var polygon in shape.Vertices)
                    {
                        var poly = new Polygon(bodyGroup) { Name = "Polygon " + count };

                        foreach (var point in polygon)
                        {
                            var x = (int)ConvertUnits.ToDisplayUnits(point.X);
                            var y = (int)ConvertUnits.ToDisplayUnits(point.Y);

                            x += (int)(frame.Width * 0.5f);
                            y += (int)(frame.Height * 0.5f);

                            poly.Points.Add(new PolyPoint(x, y, poly));
                        }

                        bodyGroup.Polygons.Add(poly);
                        count++;
                    }

                    frame.PolygonGroups.Add(bodyGroup);
                }
            }
        }

        private static void SetNaturalCenter(ImageFrame frame)
        {
            var left = frame.TrimRectangle.Left;
            var width = frame.TrimRectangle.Width;
            var height = frame.TrimRectangle.Height;
            var top = frame.TrimRectangle.Top;

            frame.CenterPointX = left + (int)(width * 0.5f);
            frame.CenterPointY = top + (int)(height * 0.5f);
        }

        private static void AddDefaultFootBox(ImageFrame frame)
        {
            var footGroup = new PolygonGroup(frame) { Name = "Foot" };
            frame.PolygonGroups.Add(footGroup);
            var foot = new Polygon(footGroup) { Name = "Foot" };

            var bottom = frame.TrimRectangle.Bottom;
            var left = frame.TrimRectangle.Left;
            var right = frame.TrimRectangle.Right;
            var width = frame.TrimRectangle.Width;

            var tl = new PolyPoint(left + (int)(width * 0.25f), bottom - 2, foot);
            var tr = new PolyPoint(right - (int)(width * 0.25f), bottom - 2, foot);
            var br = new PolyPoint(right - (int)(width * 0.25f), bottom, foot);
            var bl = new PolyPoint(left + (int)(width * 0.25f), bottom, foot);
            foot.Points.Add(tl);
            foot.Points.Add(tr);
            foot.Points.Add(br);
            foot.Points.Add(bl);

            footGroup.Polygons.Add(foot);
        }


        private static void AddPlatformBoxStub(ImageFrame frame)
        {
            var platformGroup = new PolygonGroup(frame) { Name = "Platform" };
            frame.PolygonGroups.Add(platformGroup);
            var attack = new Polygon(platformGroup) { Name = "Polygon 1" };
            platformGroup.Polygons.Add(attack);
        }

        private static void AddAttackBoxStub(ImageFrame frame)
        {
            var attackGroup = new PolygonGroup(frame) { Name = "Attack" };
            frame.PolygonGroups.Add(attackGroup);
            var attack = new Polygon(attackGroup) { Name = "Polygon 1" };
            attackGroup.Polygons.Add(attack);
        }

        private static void AddClippingBoxStub(ImageFrame frame)
        {
            var attackGroup = new PolygonGroup(frame) { Name = "Clipping" };
            frame.PolygonGroups.Add(attackGroup);
            var attack = new Polygon(attackGroup) { Name = "Polygon 1" };
            attackGroup.Polygons.Add(attack);
        }
    }
}