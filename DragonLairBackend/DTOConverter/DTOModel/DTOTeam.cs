using System.Collections.Generic;
using System.Runtime.Serialization;
using Entities;

namespace DTOConverter.DTOModel
{

    [DataContract(IsReference = true)]
    public class DTOTeam
    {
        
        public DTOTeam()
        {
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Draw { get; set; }
        [DataMember]
        public int Loss { get; set; }
        [DataMember]
        public int Win { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public ICollection<DTOPlayer> Players { get; set; } 

    }
}