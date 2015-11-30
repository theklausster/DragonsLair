using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DTOConverter.DTOModel
{
    [DataContract(IsReference = true)]
    public class DTOGenre
    {

        public DTOGenre()
        {
        }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public List<DTOGame> DtoGames { get; set; }
    }
    }