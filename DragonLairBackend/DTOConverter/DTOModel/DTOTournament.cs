using System;
using System.Runtime.Serialization;

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


    }
}