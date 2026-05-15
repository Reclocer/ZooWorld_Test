using System;

namespace SUBS.Core.NextVar
{
    [Serializable]
    public class NextFloat : NextVar<float>
    {
        public NextFloat()
        {
            val = 0f;
        }

        public NextFloat(float value)
        {
            val = value;
        }

        public static implicit operator float(NextFloat operand)
        {
            return operand == null ? 0f : (float)operand.val;
        }

        public override string ToString()
        {
            return val.ToString();
        }
    }
}
