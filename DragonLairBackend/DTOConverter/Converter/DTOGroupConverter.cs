using System;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter.Converter
{
    public class DTOGroupConverter : DTOAbstract<Group, DTOGroup>
    {
        public DTOGroupConverter()
        {
        }


        public override DTOGroup Convert(Group t)
        {
            return new DTOGroup() { Id = t.Id, Name = t.Name };
        }
    }
}