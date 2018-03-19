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
    }
    class CvMeasure : ICvMeasure
    {
        public static bool cvMeasureHasinit = false;
        public static bool cvHasMeasure = false;

        public static bool cvCharacDataHasReceive = false;
        public static bool cvEdgeStrucDataHasReceive = false;
        Ilaser ilaser = new Laser();
        public void CvMeasureInit()
        {
            ilaser.delay(1000);
            cvMeasureHasinit = true;
        }
        public void CvPhotograph()
        {
            System.Threading.Thread.Sleep(1000);
            cvHasMeasure = true;
        }
        public void CvCharacDataReceive(object a)
        {
            System.Threading.Thread.Sleep(1000);
            cvCharacDataHasReceive = true;
        }
        public void CvEdgeStrucDataReceive(object a)
        {
            System.Threading.Thread.Sleep(1000);
            cvEdgeStrucDataHasReceive = true;
        }

    }
}
