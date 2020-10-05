using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorkspaceManager.Core.Domain
{
    [Serializable]
    public class Profile
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlArray("Programs")]
        [XmlArrayItem("Program")]
        public Program[] Programs { get; set; }
    }
}
