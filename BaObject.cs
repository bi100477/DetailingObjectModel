using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetailingObjectModel
{
    public abstract class BaObject
    {
        public Guid Identifier { get; set; }

        protected BaObject()
        {
            Identifier = new Guid();
        }

        public abstract BaObType baObType();

        public abstract void Write(StreamWriter sw);

        public abstract void Read(StreamReader sr);
    }
}
