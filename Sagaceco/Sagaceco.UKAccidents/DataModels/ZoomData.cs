using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sagaceco.UKAccidents.DataModels
{
    public class ZoomData
    {
        private SortedList<long,PointData>[]    layers  = new SortedList<long, PointData>[15];

        public ZoomData()
        {
            for(int i = 0; i < layers.Length; i++)
                layers[i] = new SortedList<long, PointData>(10000 * (i+1));
        }

        public void AddAccident(double latitude, double longitude)
        {
            Parallel.For(0, layers.Length, i =>
            {
                int     zoom    = i + 8;
                long    key     = QuadKey.ToQuadKey(latitude, longitude, zoom);

                if(!layers[i].ContainsKey(key))
                    layers[i][key] = new PointData(latitude, longitude, zoom);

                layers[i][key].AccidentCount += 1;
            } );
        }

        public int GetAccidentCountAt(long key, int zoom)
        {
            int index = zoom - 8;

            if(index < 0 || index >= layers.Length)
                return 0;

            if(layers[index].ContainsKey(key))
                return layers[index][key].AccidentCount;
            else
                return 0;
        }
    }
}
