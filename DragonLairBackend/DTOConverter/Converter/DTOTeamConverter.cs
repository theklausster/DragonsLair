using System;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter.Converter
{
    public class DTOTeamConverter : DTOAbstract<Team, DTOTeam>
    {
        public DTOTeamConverter()
        {
        }

        public override DTOTeam Convert(Team t)
        {

            return new DTOTeam() { Id = t.Id, Draw = t.Draw, Loss = t.Loss, Name = t.Name, Win = t.Win };
        }
    }
}