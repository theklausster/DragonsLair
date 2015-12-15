using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Newtonsoft.Json;

namespace DTOConverter.DTOModel
{
    [DataContract(IsReference = true)]
    public class DTOMatch
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Round { get; set; }
        [DataMember]
        public DTOTeam Winner { get; set; }

        [DataMember]

        public DTOTeam HomeTeam { get; set; }
        [DataMember]
        public DTOTeam AwayTeam { get; set; }
        public List<int> TeamIdvsTeamId { get; set; }
        public DTOTournament DtoTournament { get; set; }
    }
}
