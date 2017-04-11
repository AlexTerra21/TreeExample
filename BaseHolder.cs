using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeExample
{
    abstract class BaseHolder
    {
        public BaseHolder() { }

        
    }

    class IntHolder : BaseHolder
    {
        Int32 IntValue { get; set; }

        public IntHolder() {}
        public IntHolder(Int32 val) 
        {
            IntValue = val;
        }

        public override string ToString()
        {
            return IntValue.ToString();
        }
    }

    class StringHolder : BaseHolder
    {
        String StringValue { get; set; }

        public StringHolder() { }
        public StringHolder(String val)
        {
            StringValue = val;
        }

        public override string ToString()
        {
            return StringValue.ToString();
        }
    }

    class DoubleHolder : BaseHolder
    {
        Double DoubleValue { get; set; }

        public DoubleHolder() { }
        public DoubleHolder(Double val)
        {
            DoubleValue = val;
        }

        public override string ToString()
        {
            return DoubleValue.ToString();
        }
    }

}
