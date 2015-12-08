using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Entities;

namespace DTOConverter.DTOModel
{
    [DataContract(IsReference = true)]
    public class DTOTournament
    {
       
        public DTOTournament()
        {
        }
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DTOTournamentType DTOTournamentType { get; set; }
        [DataMember]
        public ICollection<DTOGroup> DtoGroups { get; set; }

        [DataMember]
        public DTOGame DtoGame { get; set; }
    }
}