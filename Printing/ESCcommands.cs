using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Printing
{
    class ESC
    {
 
            public readonly string GotoStoCutPosition = "\x1c\x28\x4c\x02\x00\x42\x30";
            public readonly string GotoStartPosition = "\x1c\x28\x4c\x02\x00\x43\x32";
            public readonly string InisialisePrinter = "\x1B\x40";


     }
}
