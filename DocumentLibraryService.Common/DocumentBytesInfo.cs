using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentLibraryService.Common
{
    public  class DocumentBytesInfo
    {
        public string AbsoluteFilePath { get; set; }
        public byte[] Bytes { get; set; }
    }
}
