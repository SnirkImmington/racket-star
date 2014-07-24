using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacketSharp
{
    abstract class CallArgument
    {
        public abstract string ToString();
    }

    class IntArgument : CallArgument
    {
        private int value;

        public override string ToString()
        {
            return value.ToString();
        }

        public IntArgument(int input)
        {
            value = input;
        }
    }

    class StringArgument : CallArgument
    {
        private string value;

        public override string ToString()
        {
            return value;
        }

        public StringArgument(string input)
        {
            value = input;
        }
    }

    class DoubleArgument : CallArgument
    {
        private double value;

        public override string ToString()
        {
            return value.ToString();
        }

        public DoubleArgument(double input)
        {
            value = input;
        }
    }

    class ObjectArgument<T> : CallArgument
       // where T : object
    {
        private T value;

        public override string ToString()
        {
            return value.ToString();
        }

        public ObjectArgument(T input)
        {
            value = input;
        }
    }
}
