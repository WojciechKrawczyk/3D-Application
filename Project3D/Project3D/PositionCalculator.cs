using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3D
{
    public class PositionCalculator
    {
        public Calculator XCalculator;
        public Calculator YCalculator;
        public Calculator ZCalculator;
    }

    public class Calculator
    {
        private bool active = false;
        private float min;
        private float max;
        private float current;
        private float step;

        public Calculator(float min, float max, float current, float step)
        {
            this.min = min;
            this.max = max;
            this.current = current;
            this.step = step;
            this.active = true;
        }

        public Calculator() { }

        public float GetNext()
        {
            if (!active)
                return 0;

            current += step;
            if (current >= max || current <= min)
            {
                step = -step;
            }
            return current;
        }
    }
}
