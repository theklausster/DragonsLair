using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOConverter.DTOModel
{
    [DataContract(IsReference = true)]
    public class DTOGame
    {
        public DTOGame()
        {
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public DTOGenre DtoGenre { get; set; }
        [DataMember]
        public  List<DTOTournament> DtoTournaments { get; set; }
    }
}