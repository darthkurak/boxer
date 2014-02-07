using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics.Common.PolygonManipulation;
using fwd;

namespace SpriteUtility.Services
{
    public static class TraceService
    {
        public static void SetDisplayUnitToSimUnitRatio(float simulationRatio)
        {
            ConvertUnits.SetDisplayUnitToSimUnitRatio(simulationRatio);
        }

        public static Shape CreateSimpleShape(string imagePath, int timeout, StringBuilder spool, bool holeDetection = true, bool strict = false, TriangulationAlgorithm algorithm = TriangulationAlgorithm.Bayazit)
        {
            Shape shape = null;
            Action action = () =>
            {
                try
                {
                    shape = BuildShape(imagePath, spool, holeDetection, strict, algorithm);
                }
                catch
                {
                    var image = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                    spool.AppendFormat("{0} - Error creating shape for image", image);
                    spool.AppendLine();
                }
            };

            var result = action.BeginInvoke(null, null);
            if (!result.AsyncWaitHandle.WaitOne(timeout))
            {
                var image = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                spool.AppendFormat("{0} - Timed out attempting to create shape", image);
                spool.AppendLine();
            }

            return shape;
        }

        public static Shape CreateComplexShape(string imagePath, int timeout, StringBuilder spool, float hullTolerance, byte alphaTolerance, bool multiPartDetection, bool holeDetection = true, bool strict = false, TriangulationAlgorithm algorithm = TriangulationAlgorithm.Bayazit)
        {
            Shape shape = null;
            Action action = () =>
            {
                try
                {
                    shape = BuildShape(imagePath, spool, hullTolerance, alphaTolerance, multiPartDetection, holeDetection, strict, algorithm);
                }
                catch
                {
                    var image = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                    spool.AppendFormat("{0} - Error creating shape for image", image);
                    spool.AppendLine();
                }
            };

            var result = action.BeginInvoke(null, null);
            if (!result.AsyncWaitHandle.WaitOne(timeout))
            {
                var image = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                spool.AppendFormat("{0} - Timed out attempting to create shape", image);
                spool.AppendLine();
            }

            return shape;
        }

        private static Shape BuildShape(string imagePath, StringBuilder spool, bool holeDetection, bool strict, TriangulationAlgorithm algorithm)
        {
            var image = Image.FromFile(imagePath);
            var data = LoadImageData(image);
            var polygon = PolygonTools.CreatePolygon(data, image.Width, holeDetection);

            var polygons = new List<Vertices>{ polygon };
            return ScaleConvertAndPartition(imagePath, spool, strict, image, polygons, algorithm);
        }

        private static Shape BuildShape(string imagePath, StringBuilder spool, float hullTolerance, byte alphaTolerance, bool multiPartDetection, bool holeDetection = true, bool strict = false, TriangulationAlgorithm algorithm = TriangulationAlgorithm.Bayazit)
        {
            var image = Image.FromFile(imagePath);
            var data = LoadImageData(image);
            var polygons = PolygonTools.CreatePolygon(data, image.Width, hullTolerance, alphaTolerance, multiPartDetection, holeDetection);

            return ScaleConvertAndPartition(imagePath, spool, strict, image, polygons, algorithm);
        }

        private static Shape ScaleConvertAndPartition(string imagePath, StringBuilder spool, bool strict, Image image, IEnumerable<Vertices> polygons, TriangulationAlgorithm algorithm)
        {
            var scale = ConvertUnits.ToSimUnits(1, 1);
            var width = ConvertUnits.ToSimUnits(image.Width);
            var height = ConvertUnits.ToSimUnits(image.Height);
            var translation = new Vector2(-width, -height)*0.5f;

            var final = new List<List<Vector2>>();

            foreach (var polygon in polygons)
            {
                polygon.Scale(scale);
                polygon.Translate(translation);

                var thisPolygon = SimplifyTools.CollinearSimplify(polygon);

                if (strict)
                {
                    var errors = thisPolygon.CheckPolygon();
                    if (errors != PolygonError.NoError)
                    {
                        var imageName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                        spool.AppendFormat("{1}: Invalid shape ({0})", errors, imageName);
                        spool.AppendLine();
                        return null;
                    }
                }
                
                try
                {
                    var partition = Triangulate.ConvexPartition(thisPolygon, algorithm);
                    var vertices = partition.Select(verts => verts.Select(v => new Vector2(v.X, v.Y)).ToList());
                    final.AddRange(vertices);
                }
                catch
                {
                    var imageName = System.IO.Path.GetFileNameWithoutExtension(imagePath);
                    spool.AppendFormat("{0}: Cannot triangulate polygon", imageName);
                    spool.AppendLine();
                }
            }
            
            var shape = new Shape
            {
                Vertices = final
            };
            return shape;
        }

        public static unsafe uint[] LoadImageData(Image image)
        {
            var bitmap = (Bitmap)image;
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Size.Width, bitmap.Size.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            var data = new uint[image.Width * image.Height];
            var ptr = (uint*)bitmapData.Scan0.ToPointer();
            for (var x = 0; x < image.Width; x++)
            {
                for (var y = 0; y < image.Height; y++)
                {
                    var index = x + y * image.Width;
                    data[index] = ptr[index];
                }
            }
            bitmap.UnlockBits(bitmapData);
            return data;
        }
    }
}
