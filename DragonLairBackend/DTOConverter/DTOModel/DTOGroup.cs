using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOConverter.DTOModel
{
    [DataContract(IsReference = true)]
    public class DTOGroup
    {
        public DTOGroup()
        {
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public  DTOTournament DtoTournament { get; set; }
        [DataMember]
        public  List<DTOTeam> DtoTeams { get; set; }
    }
}