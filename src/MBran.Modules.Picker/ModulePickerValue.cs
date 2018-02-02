using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace MBran.Modules.Picker
{
    public class ModulePickerValue
    {
        public Type ModuleType { get; set; }
        public IEnumerable<IPublishedContent> Sources { get; set; }
    }
}
