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

        public DTOTournamentType DTOTournamentType { get; set; }
        public ICollection<DTOGroup> DtoGroups { get; set; }
        public DTOGame DtoGame { get; set; }
    }
}