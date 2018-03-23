using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GJD
{
    public interface ICvMeasure
    {
        void CvMeasureInit();
        void CvPhotograph();
        void CvCharacDataReceive(object a);
        void CvEdgeStrucDataReceive(object a);
        void CvReMeasureResult();
    }
    class CvMeasure : ICvMeasure
    {
        public static int cvMeasureHasinit = 0;
        public static int cvHasMeasure = 0;//0表示没执行指令，1表示指令执行中，2表示指令执行完
        public static int cvHasMeasureResult = 0;
        public static int cvCharacDataHasReceive = 0;
        public static int cvEdgeStrucDataHasReceive = 0;
        Ilaser ilaser = new Laser();
        public void CvReMeasureResult()
        {
            ilaser.delay(1000);
            cvHasMeasureResult = 2;
        }
        public void CvMeasureInit()
        {
            ilaser.delay(1000);
            cvMeasureHasinit = 2;
        }
        public void CvPhotograph()
        {
            System.Threading.Thread.Sleep(1000);
            cvHasMeasure = 2;
        }
        public void CvCharacDataReceive(object a)
        {
            System.Threading.Thread.Sleep(1000);
            cvCharacDataHasReceive = 2;
        }
        public void CvEdgeStrucDataReceive(object a)
        {
            System.Threading.Thread.Sleep(1000);
            cvEdgeStrucDataHasReceive = 2;
        }

    }
}
