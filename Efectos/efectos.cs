using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Efectos
{
    class efectos : ISampleProvider
    {

        private ISampleProvider fuente;
        public efectos (ISampleProvider fuente)
        {
            this.fuente = fuente;
        }
        public WaveFormat WaveFormat => throw new NotImplementedException();

        public int Read(float[] buffer, int offset, int count)
        {
            var read = fuente.Read(buffer, offset, count);

            for (int i=0; i < read; i++)
            {
                //efecto
                buffer[offset + i] *= 0.5f;
            }

            return read;
        }
    }
}
