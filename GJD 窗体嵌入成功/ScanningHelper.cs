using System;
using System.Collections.Generic;
using Cti.Hardware.ScanDevice;
using Cti.Hardware.ScanDevice.Base;
using Cti.Hardware.Extension.Shapes;
using Cti.Hardware.Extension.Algorithm;
using Cti.Hardware.Extension.Util;
namespace GJD
{
    public sealed class ScanningHelper
    {
        #region Fields

        const string LaserPropertyKey = "LaserProperty";

        #endregion

        #region Private methods
        //Add shape to VectorImage
        private static void AddShapeToVectorImage(VectorImage vectorImage, ShapeBase shape, DistanceUnit currentUnit)
        {
            if (shape.Markable)
            {
                switch (shape.ShapeType)
                {
                    case ShapeType.Line:
                        Line line = (Line)shape;
                        vectorImage.AddLine(line.StartPoint.X, line.StartPoint.Y, line.StartPoint.Z, line.EndPoint.X, line.EndPoint.Y, line.EndPoint.Z);
                        break;
                    case ShapeType.Circle:
                        Circle circle = (Circle)shape;
                        vectorImage.AddCircle(circle.CenterPoint.X, circle.CenterPoint.Y, circle.CenterPoint.Z, circle.Radius);
                        break;
                    case ShapeType.Arc:
                        Arc arc = (Arc)shape;
                        vectorImage.AddArc(arc.CenterPoint.X, arc.CenterPoint.Y, arc.CenterPoint.Z, arc.Radius, arc.StartAngle, arc.SweepAngle);
                        break;
                    case ShapeType.Polyline:
                        Polyline polyline = (Polyline)shape;
                        IList<Point3D> verticeList = polyline.Vertices;
                        List<Point3D> point3DList = new List<Point3D>();
                        for (int vertIndex = 0; vertIndex < verticeList.Count; vertIndex++)
                        {
                            Point3D vertex = verticeList[vertIndex];
                            point3DList.Add(new Point3D(vertex.X, vertex.Y, vertex.Z));
                        }
                        vectorImage.AddPolyline(point3DList);
                        break;
                    case ShapeType.Dot:
                        Dot dot = (Dot)shape;
                        vectorImage.AddDot(dot.X, dot.Y, dot.Z, 1);
                        break;

                }
            }
        }

        private static void AddHatch(VectorImage vectorImage, Hatch hatch, DistanceUnit currentUnit)
        {
            HatchShape hatchShape = new HatchShape();

            List<HatchPattern> hatchPatternList = new List<HatchPattern>();
            foreach (HatchPattern hatchPattern in hatch.HatchPatternList)
            {
                hatchShape.AddHatchPattern(hatchPattern);
            }

            foreach (ShapeBase boundryShape in hatch.BoundaryShapeList)
            {
                AddToHatchBoundaries(boundryShape, hatchShape);
            }
            vectorImage.AddHatchShape(hatchShape, 0);
        }

        static void AddToHatchBoundaries(ShapeBase shape, HatchShape hatchShape)
        {
            switch (shape.ShapeType)
            {
                case ShapeType.Arc:
                    Arc arcShape = shape as Arc;
                    hatchShape.AddArc2D(arcShape.CenterPoint.X, arcShape.CenterPoint.Y, arcShape.Radius, arcShape.StartAngle, arcShape.SweepAngle);
                    break;
                case ShapeType.Circle:
                    Circle circleShape = shape as Circle;
                    hatchShape.AddCircle2D(circleShape.CenterPoint.X, circleShape.CenterPoint.Y, circleShape.Radius);
                    break;

                case ShapeType.Group:
                    Group groupShape = shape as Group;
                    foreach (ShapeBase groupSubShape in groupShape.ShapeList)
                    {
                        AddToHatchBoundaries(groupSubShape, hatchShape);
                    }
                    break;

                case ShapeType.Line:
                    Line lineShape = shape as Line;
                    hatchShape.AddLine2D(lineShape.StartPoint.X, lineShape.StartPoint.Y, lineShape.EndPoint.X, lineShape.EndPoint.Y);
                    break;
                case ShapeType.Polyline:
                    Polyline polylineShape = shape as Polyline;
                    if (polylineShape.Closed)
                    {
                        hatchShape.AddPolygon(polylineShape.Vertices);
                    }
                    else
                    {
                        hatchShape.AddPolyline(polylineShape.Vertices);
                    }
                    break;
                default:
                    break;
            }
        }

        private static void AddTextBox(VectorImage vectorImage, HorizontalText textBoxShape, DistanceUnit currentUnit)
        {
            TextShape textShape = new TextShape();

            textShape.Location = textBoxShape.Location.Clone();
            textShape.Angle = textBoxShape.Angle;
            textShape.ItalicAngle = textBoxShape.ItalicAngle;

            textShape.ScaleX = textBoxShape.ScaleX;
            textShape.ScaleY = textBoxShape.ScaleY;
            textShape.TextBoxHeight = textBoxShape.TextBoxHeight;
            textShape.TextBoxWidth = textBoxShape.TextBoxWidth;
            textShape.LineSpace = textBoxShape.LineSpace;
            textShape.LineSpaceStyle = textBoxShape.LineSpaceStyle;
            textShape.HorizontalAlign = textBoxShape.HorizontalAlign;
            textShape.VerticalAlign = textBoxShape.VerticalAlign;
            textShape.WordWrap = textBoxShape.WordWrap;
            textShape.Kerning = textBoxShape.Kerning;

            textShape.AddText(textBoxShape.Text, textBoxShape.FontFaceName, textBoxShape.FontStyle, textBoxShape.TextHeight, 0);
            vectorImage.AddTextShape(textShape);
        }

        private static void AddLinearBarcode(VectorImage vectorImage, LinearBarcode linearBarcode, DistanceUnit currentUnit)
        {
            LinearBarcodeShape barcode = new LinearBarcodeShape();

            barcode.Text = linearBarcode.Text;
            barcode.BarcodeType = linearBarcode.BarcodeType;

            barcode.Height = linearBarcode.Height;
            barcode.InvertImage = linearBarcode.InvertImage;
            barcode.QuietZone = linearBarcode.QuietZone;
            barcode.Angle = linearBarcode.Angle;
            barcode.PrintRatio = linearBarcode.PrintRatio;
            barcode.Width = linearBarcode.Width;
            barcode.FlipHorizontally = linearBarcode.FlipHorizontally;
            barcode.FlipVertically = linearBarcode.FlipVertically;
            barcode.MarkingOrder = linearBarcode.MarkingOrder;
            barcode.Angle = linearBarcode.Angle;

            barcode.HatchPattern = linearBarcode.HatchPattern;

            barcode.Location = linearBarcode.Location.Clone();
            vectorImage.AddBarcodeShape(barcode);
        }

        private static void AddDataMatrixBarcode(VectorImage vectorImage, DataMatrixBarcode dataMatrixBarcode, DistanceUnit currentUnit)
        {
            DataMatrixBarcodeShape barcode = new DataMatrixBarcodeShape();

            barcode.Text = dataMatrixBarcode.Text;

            barcode.DataMatrixFormat = dataMatrixBarcode.DataMatrixFormat;
            barcode.DataMatrixSize = dataMatrixBarcode.DataMatrixSize;

            barcode.Height = dataMatrixBarcode.Height;
            barcode.InvertImage = dataMatrixBarcode.InvertImage;
            barcode.QuietZone = dataMatrixBarcode.QuietZone;
            barcode.Height = dataMatrixBarcode.Height;
            barcode.AutoExpand = dataMatrixBarcode.AutoExpand;

            barcode.FlipHorizontally = dataMatrixBarcode.FlipHorizontally;
            barcode.FlipVertically = dataMatrixBarcode.FlipVertically;
            barcode.MarkingOrder = dataMatrixBarcode.MarkingOrder;

            barcode.Angle = dataMatrixBarcode.Angle;

            barcode.HatchPattern = dataMatrixBarcode.HatchPattern;
            barcode.Location = dataMatrixBarcode.Location.Clone();
            vectorImage.AddBarcodeShape(barcode);
        }

        private static void AddQRCodeBarcode(VectorImage vectorImage, QRCodeBarcode qrCodeBarcode, DistanceUnit currentUnit)
        {
            QRCodeBarcodeShape barcode = new QRCodeBarcodeShape();

            barcode.Text = qrCodeBarcode.Text;

            barcode.ErrorCorrectionLevel = qrCodeBarcode.ErrorCorrectionLevel;
            barcode.CodeSize = qrCodeBarcode.CodeSize;
            barcode.EncodingMode = qrCodeBarcode.EncodingMode;
            barcode.MaskPattern = qrCodeBarcode.MaskPattern;

            barcode.Height = qrCodeBarcode.Height;
            barcode.InvertImage = qrCodeBarcode.InvertImage;
            barcode.QuietZone = qrCodeBarcode.QuietZone;
            barcode.Height = qrCodeBarcode.Height;
            barcode.AutoExpand = qrCodeBarcode.AutoExpand;

            barcode.FlipHorizontally = qrCodeBarcode.FlipHorizontally;
            barcode.FlipVertically = qrCodeBarcode.FlipVertically;
            barcode.MarkingOrder = qrCodeBarcode.MarkingOrder;

            barcode.Angle = qrCodeBarcode.Angle;

            barcode.HatchPattern = qrCodeBarcode.HatchPattern;
            barcode.Location = qrCodeBarcode.Location.Clone();
            vectorImage.AddBarcodeShape(barcode);
        }

        //update markable shape list
        private static List<ShapeBase> GenerateMarkingShape(ShapeBase shape)
        {
            List<ShapeBase> markingShapeList = new List<ShapeBase>();
            bool cutterCompensationResultGenerated = false;
            if (shape.ShapeType == ShapeType.Polyline)
            {
                Polyline polyline = (Polyline)shape;
                IList<ShapeBase> createdShapeList = polyline.GenerateCutterCompensationResult();
                if (createdShapeList != null)
                {
                    cutterCompensationResultGenerated = true;
                    markingShapeList.AddRange(createdShapeList);
                }
            }
            else if (shape.ShapeType == ShapeType.Circle)
            {
                Circle circle = (Circle)shape;
                Circle createdCircle = circle.GenerateCutterCompensationResult();
                if (createdCircle != null)
                {
                    cutterCompensationResultGenerated = true;
                    markingShapeList.Add(createdCircle);
                }
            }

            if (!cutterCompensationResultGenerated)
            {
                markingShapeList.Add(shape);
            }
            return markingShapeList;
        }

        #endregion

        #region Public methods
        private ScanningHelper()
        {
        }

        //Add shapeDocument to VectorImage
        public static void LoadDocument(ShapeDocument shapeDocument, VectorImage vectorImage, DistanceUnit currentUnit)
        {
            IList<Layer> layerList = shapeDocument.LayerList;
            DistanceUnit currentUnitType = shapeDocument.DistanceUnit;

            List<ShapeBase> markableShapeList = new List<ShapeBase>();
            for (int layerIndex = 0; layerIndex < layerList.Count; layerIndex++)
            {
                IList<ShapeBase> shapeList = layerList[layerIndex].ShapeList;
                for (int shapeIndex = 0; shapeIndex < shapeList.Count; shapeIndex++)
                {
                    ShapeBase shape = shapeList[shapeIndex];
                    if (shape.Markable)
                    {

                        List<ShapeBase> markingShapeList = GenerateMarkingShape(shape);
                        foreach (ShapeBase markingShape in markingShapeList)
                        {
                            AddShapeToVectorImage(vectorImage, markingShape, currentUnit);

                        }
                    }
                }
            }
        }



        #endregion
    }
}
