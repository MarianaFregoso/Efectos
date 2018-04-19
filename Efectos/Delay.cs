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

        private int tamañobuffer;
        private int cantidadmuestrasborradas = 0;
        private int cantidadmuestrastranscurridas = 0;
        public Delay(ISampleProvider fuente)
        {
            this.fuente = fuente;
            offsetTiempoMS = 600;
           tamañobuffer = 20 * fuente.WaveFormat.SampleRate;
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
            float tiempoTranscurrido = (float) cantidadmuestrastranscurridas / (float)fuente.WaveFormat.SampleRate;
           
            float tiempoTranscurridoMS = tiempoTranscurrido * 1000;
            int numMuestrasOffsetTiempo = (int) (((float)offsetTiempoMS / 1000.0f)
                * (float)fuente.WaveFormat.SampleRate);
            
            //añadir muestras a nuestro buffer
            for (int i = 0; i < read; i++)
            {
                muestras.Add(buffer[i]);
            }

            //quitar elementos de nuestro buffer si se paso del maximo
            if(muestras.Count > tamañobuffer)
                { 
                //ya se paso
                int diferencia = muestras.Count - tamañobuffer;
                muestras.RemoveRange(0,diferencia);
                cantidadmuestrasborradas += diferencia;
                }

            //mofificar muestras
            if (tiempoTranscurridoMS > offsetTiempoMS)
            {
                for (int i = 0; i < read; i++)
                {
                    buffer[i] +=

                       buffer[i] += 
                       muestras[(cantidadmuestrastranscurridas - cantidadmuestrasborradas) +
                         i - numMuestrasOffsetTiempo];
                }
            }

            cantidadmuestrastranscurridas += read;

            return read;
        }
    }
}
