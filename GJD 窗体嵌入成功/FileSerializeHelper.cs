using Cti.Hardware.Extension.Shapes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace GJD
{
    public sealed class FileSerializeHelper
    {
        #region Private Methods
        private FileSerializeHelper()
        {
        }
        #endregion
        #region Public Methods
        public static void OpenDocument(string fileName, ShapeDocument shapeDocument)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            FileAttributes fileAttribute = fileInfo.Attributes & FileAttributes.ReadOnly;
            if (fileAttribute == FileAttributes.ReadOnly)
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(stream);

                ShapeDocument doc = new ShapeDocument();
                doc.Deserialize(reader);
                shapeDocument = doc;

                reader.Close();
            }
            else
            {
                FileStream stream = new FileStream(fileName, FileMode.Open);
                BinaryReader reader = new BinaryReader(stream);
                ShapeDocument doc = new ShapeDocument();
                shapeDocument.Deserialize(reader);
                reader.Close();
            }
        }
        public static void SaveDocument(string fileName, ShapeDocument shapeDocument)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            FileAttributes fileAttribute = fileInfo.Attributes & FileAttributes.Hidden;
            if (fileAttribute == FileAttributes.Hidden)
            {
                FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate);
                BinaryWriter writer = new BinaryWriter(stream);
                shapeDocument.Serialize(writer);
                writer.Close();
                stream.Close();
            }
            else
            {
                FileStream stream = new FileStream(fileName, FileMode.Create);
                BinaryWriter writer = new BinaryWriter(stream);
                shapeDocument.Serialize(writer);
                writer.Close();
                stream.Close();
            }
        }
        #endregion
    }
}
