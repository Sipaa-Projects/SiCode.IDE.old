using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiCode.IDE
{
    public class BraceFoldingStrategy : BaseFoldingStrategy
    {
        public BraceFoldingStrategy() : base('{','}') { }
    }
}
