using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Entities;


namespace DTOConverter.DTOModel
{
    [DataContract(IsReference = true)]
    public class DTOPlayer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public ICollection<DTOTeam> Teams { get; set; }
    }
}
