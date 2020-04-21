using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjectTransferService
{
   public class APIParam
    {
        public int FieldId { get; set; }
        public int Option { get; set; }
        public object FieldValue { get; set; }

        public APIParam()
        {
            FieldValue = new object();
        }
        
    }
}
