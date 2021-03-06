﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Boxer.Services;
using FarseerPhysics;
using Settings = Boxer.Properties.Settings;

namespace Boxer.Data
{
    public class ImageDataFactory
    {
        public static void ImportFromExistingDirectoryDialog(Document document)
        {
            var dialog = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                ShowNewFolderButton = false,
                SelectedPath = Settings.Default.LastFolderBrowsed
            };


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.LastFolderBrowsed = dialog.SelectedPath;
                Settings.Default.Save();

                var rootFolder = ImportFromFolder(dialog.SelectedPath);
                document.AddChild(rootFolder);
            }

        }

        public static void ImportFromExistingDirectoryDialog(Folder folder)
        {
            var dialog = new FolderBrowserDialog
            {
                RootFolder = Environment.SpecialFolder.Desktop,
                ShowNewFolderButton = false,
                SelectedPath = Settings.Default.LastFolderBrowsed
            };


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.LastFolderBrowsed = dialog.SelectedPath;
                Settings.Default.Save();

                var rootFolder = ImportFromFolder(dialog.SelectedPath);
                folder.AddChild(rootFolder);
            }

        }

        private static Folder ImportFromFolder(string selectedPath)
        {
            var rootDirectory = selectedPath;
            var rootFolder = new Folder();
            rootFolder.Name = Path.GetFileNameWithoutExtension(rootDirectory);

            var directories = Directory.GetDirectories(rootDirectory);

            List<string> images;
            foreach (var directory in directories)
            {
                var folder = ImportFromFolder(directory);

                rootFolder.AddChild(folder);
            }

            images = Directory.GetFiles(rootDirectory, "*.png", SearchOption.TopDirectoryOnly).ToList();
            images.AddRange(Directory.GetFiles(rootDirectory, "*.gif", SearchOption.TopDirectoryOnly));

            foreach (var filename in images)
            {
                var imageData = CreateFromFilename(filename);
                rootFolder.AddChild(imageData);
            }
            return rootFolder;
        }

        public static ImageData CreateFromFilename(string filename)
        {
            var imageData = new ImageData(filename);

            // Since we are adding new images we can stub in some
            //    conventional defaults (currently for trimmed frames only)

            if (Settings.Default.TrimToMinimalNonTransparentArea)
            {
                foreach (ImageFrame frame in imageData.Children)
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
                    var bodyGroup = new PolygonGroup() { Name = "Body" };
                    var count = 1;
                    foreach (var polygon in shape.Vertices)
                    {
                        var poly = new Polygon() { Name = "Polygon " + count };


                        foreach (var point in polygon)
                        {
                            var x = (int)ConvertUnits.ToDisplayUnits(point.X);
                            var y = (int)ConvertUnits.ToDisplayUnits(point.Y);

                            x += (int)(frame.Width * 0.5f);
                            y += (int)(frame.Height * 0.5f);

                            poly.AddChild(new PolyPoint(x, y));
                        }

                        bodyGroup.AddChild(poly);
                        count++;
                    }

                    frame.AddChild(bodyGroup);
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
            var footGroup = new PolygonGroup() { Name = "Foot" };
            frame.AddChild(footGroup);
            var foot = new Polygon() { Name = "Foot" };

            var bottom = frame.TrimRectangle.Bottom;
            var left = frame.TrimRectangle.Left;
            var right = frame.TrimRectangle.Right;
            var width = frame.TrimRectangle.Width;

            var tl = new PolyPoint(left + (int)(width * 0.25f), bottom - 2);
            var tr = new PolyPoint(right - (int)(width * 0.25f), bottom - 2);
            var br = new PolyPoint(right - (int)(width * 0.25f), bottom);
            var bl = new PolyPoint(left + (int)(width * 0.25f), bottom);
            foot.AddChild(tl);
            foot.AddChild(tr);
            foot.AddChild(br);
            foot.AddChild(bl);

            footGroup.AddChild(foot);
        }


        private static void AddPlatformBoxStub(ImageFrame frame)
        {
            var platformGroup = new PolygonGroup() { Name = "Platform" };
            frame.AddChild(platformGroup);
            var attack = new Polygon() { Name = "Polygon 1" };
            platformGroup.AddChild(attack);
        }

        private static void AddAttackBoxStub(ImageFrame frame)
        {
            var attackGroup = new PolygonGroup() { Name = "Attack" };
            frame.AddChild(attackGroup);
            var attack = new Polygon() { Name = "Polygon 1" };
            attackGroup.AddChild(attack);
        }

        private static void AddClippingBoxStub(ImageFrame frame)
        {
            var attackGroup = new PolygonGroup() { Name = "Clipping" };
            frame.AddChild(attackGroup);
            var attack = new Polygon() { Name = "Polygon 1" };
            attackGroup.AddChild(attack);
        }
    }
}
