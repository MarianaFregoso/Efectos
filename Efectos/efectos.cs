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

        private float factor;
        public float Factor
        {
            get
            {
                return factor;
            }
            set
            {
                if (value > 1)
                    factor = 1;
                else if (value < 0)
                    factor = 0;
                else
                    factor = value;
             }
        }
        public efectos (ISampleProvider fuente)
        {
            this.fuente = fuente;
            Factor = 0.5f;
        }

        public efectos(ISampleProvider fuente, float factor)
        {
            this.fuente = fuente;
            Factor = factor;
            if (factor > 1)
                factor = 1;
            else if (factor < 0)
                factor = 0;
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
