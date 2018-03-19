using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cti.Hardware.Extension.Shapes;
using Cti.Hardware.ScanDevice.Base;

using System.Drawing;
namespace GJD
{
    class ShapeCreateHelper
    {
        #region Fields
        #endregion

        #region Private methods
        #endregion

        #region Public Methods
        public ShapeCreateHelper()
        {
        }

        public static ShapeBase CreateLineShape(float lineStartX, float lineStartY, float lineEndX, float lineEndY)
        {
            Line line = new Line();
            line.StartPoint.X = lineStartX;
            line.StartPoint.Y = lineStartY;
            line.EndPoint.X = lineEndX;
            line.EndPoint.Y = lineEndY;
            line.Color = Color.DeepPink;

            return line;

        }

        public static Circle CreateCircleEntity(float x, float y, float r, float cutterwidth, bool inner)
        {
            Circle circle = new Circle();
            circle.CenterPoint = new Point3D(x, y, 0);
            circle.Radius = r;
            circle.Color = Color.BlueViolet;
            circle.CutterCompensationWidth = cutterwidth;
            if (inner)
            {
                circle.DirectionOfCutterCompensation = CutterCompensationDirection.Inner;
            }
            else
            {
                circle.DirectionOfCutterCompensation = CutterCompensationDirection.Outer;
            }
            return circle;
        }

        public static ShapeBase CreateArcShape(float x, float y, float r, float startangle, float angle)
        {
            Arc arc = new Arc();
            arc.CenterPoint = new Point3D(x, y, 0);
            arc.Radius = r;
            arc.StartAngle = startangle;
            arc.SweepAngle = angle;
            arc.Color = Color.Purple;


            return arc;
        }

        public static ShapeBase CreatePolylineShape(float scaleFactor)
        {
            Polyline polyline = new Polyline();
            List<Point3D> verticeList = new List<Point3D>();
            verticeList.Add(new Point3D(-10 * scaleFactor, -10 * scaleFactor, 0));
            verticeList.Add(new Point3D(-20 * scaleFactor, 0, 0));
            verticeList.Add(new Point3D(-10 * scaleFactor, 10 * scaleFactor, 0));
            verticeList.Add(new Point3D(10 * scaleFactor, 10 * scaleFactor, 0));
            verticeList.Add(new Point3D(20 * scaleFactor, 0, 0));
            verticeList.Add(new Point3D(10 * scaleFactor, -10 * scaleFactor, 0));

            polyline.Closed = true;
            polyline.CutterCompensationWidth = 0.1f * scaleFactor;
            polyline.DirectionOfCutterCompensation = CutterCompensationDirection.Inner;

            polyline.Vertices = verticeList;
            polyline.Color = Color.Orchid;

            return polyline;
        }

        public static ShapeBase CreateDotShape(float scaleFactor)
        {
            Dot dot = new Dot();
            dot.X = 25 * scaleFactor;
            dot.Y = 25 * scaleFactor;
            dot.Z = 25 * scaleFactor;
            dot.Color = Color.Brown;

            return dot;
        }

        //public static ShapeBase CreateArrowEntity(float scaleFactor)
        //{
        //    ArrowShape arrow = new ArrowShape();
        //    arrow.StartPoint.Copy(0, 20 * scaleFactor, 0);
        //    arrow.StartPoint.Copy(20 * scaleFactor, 30 * scaleFactor, 0);
        //    arrow.Color = Color.Cyan;

        //    return arrow;
        //}

        public static ShapeBase CreateTextEntity(float scaleFactor)
        {
            HorizontalText text = new HorizontalText();
            text.Text = "A";
            text.TextHeight = 5 * scaleFactor;
            text.FontFaceName = "Arial";
            text.FontStyle = FontStyle.Regular;
            text.Location.X = 0;
            text.Location.Y = 25 * scaleFactor;
            text.Angle = (float)(30f / 180 * Math.PI);
            text.Color = Color.Violet;

            return text;
        }

        public static ShapeBase CreateBarcodeEntity(float scaleFactor)
        {
            LinearBarcode barcode = new LinearBarcode();
            barcode.Text = "1234";
            barcode.Height = 6 * scaleFactor;
            barcode.Width = 12 * scaleFactor;
            barcode.Location.X = -20 * scaleFactor;
            barcode.Location.Y = 20 * scaleFactor;
            barcode.BarcodeType = BarcodeType.Codabar;
            barcode.HatchPattern = BarcodeHatchPattern.CreateLineHatchPattern(0.025f * scaleFactor, true, true);
            barcode.Color = Color.Black;

            return barcode;
        }

        public static ShapeBase CreateDataMatrixEntity(float scaleFactor)
        {

            DataMatrixBarcode dataMatrix = new DataMatrixBarcode();
            dataMatrix.Text = "1";
            dataMatrix.Height = 10 * scaleFactor;
            dataMatrix.DataMatrixSize = DataMatrixSize.S16x16;
            dataMatrix.AutoExpand = true;
            dataMatrix.Location.X = 10 * scaleFactor;
            dataMatrix.Location.Y = -10 * scaleFactor;
            dataMatrix.HatchPattern = BarcodeHatchPattern.CreateLineHatchPattern(0.25f * scaleFactor, true, true);
            dataMatrix.Color = Color.YellowGreen;

            return dataMatrix;
        }

        public static ShapeBase CreateQRCodeEntity(float scaleFactor)
        {

            QRCodeBarcode qrcode = new QRCodeBarcode();
            qrcode.Text = "1";
            qrcode.Height = 10 * scaleFactor;
            qrcode.CodeSize = QRCodeSize.S21x21;
            qrcode.AutoExpand = true;
            qrcode.Location.X = -25 * scaleFactor;
            qrcode.Location.Y = -25 * scaleFactor;
            qrcode.Color = Color.Cyan;

            return qrcode;
        }

        public static ShapeBase CreateHatchEntity(float scaleFactor)
        {
            Point3D boundaryCircleCenterPoint = new Point3D(20 * scaleFactor, -20 * scaleFactor, 0);
            float boundaryCircleRadius = 15 * scaleFactor;

            Hatch hatch = new Hatch();
            hatch.AddHatchPatternLine(0, HatchLineBorderGapDirection.Inward, 0.75f * scaleFactor, 30 * (float)Math.PI / 180, 0, 0,
                HatchLineStyle.Unidirectional, false, HatchOffsetAlgorithm.DirectOffset,
                HatchCornerStyle.Sharp);
            hatch.AddCircle(boundaryCircleCenterPoint.X, boundaryCircleCenterPoint.Y, boundaryCircleCenterPoint.Z, boundaryCircleRadius);
            hatch.Color = Color.DarkTurquoise;

            return hatch;
        }
        #endregion
    }
}
