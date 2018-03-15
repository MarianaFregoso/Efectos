using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Efectos
{
    class Delay : ISampleProvider
    {
        internal float read;
        private ISampleProvider fuente;

        public int offsetTiempoMS;
        List<float> muestras = new List<float>();
        public Delay(ISampleProvider fuente)
        {
            this.fuente = fuente;
            offsetTiempoMS = 600;
            //50ms - 5000ms
        }

        public WaveFormat WaveFormat {
            get {
                return fuente.WaveFormat;
            }
        }
          
        //offset es el numero de muestras leidas
        public int Read(float[] buffer, int offset, int count)
        {

          

            var read = fuente.Read(buffer, offset, count);
            float tiempoTranscurrido = (float) muestras.Count / (float)fuente.WaveFormat.SampleRate;
            int muestrastrasncurridas = muestras.Count;
            float tiempoTranscurridoMS = tiempoTranscurrido * 1000;
            int numMuestrasOffsetTiempo = (int) (((float)offsetTiempoMS / 1000.0f)
                * (float)fuente.WaveFormat.SampleRate);
            
            //añadir muestras a nuestro buffer
            for (int i = 0; i < read; i++)
            {
                muestras.Add(buffer[i]);
            }


            //mofificar muestras
            if (tiempoTranscurridoMS > offsetTiempoMS)
            {
                for (int i = 0; i < read; i++)
                {
                    buffer[i] =

                       buffer[i] += muestras[muestrastrasncurridas +
                         i - numMuestrasOffsetTiempo];
                }
            }

            return read;
        }
    }
}
