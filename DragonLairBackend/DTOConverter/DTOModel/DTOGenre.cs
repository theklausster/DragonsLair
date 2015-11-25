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
    }
    }