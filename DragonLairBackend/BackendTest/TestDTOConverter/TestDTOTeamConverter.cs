﻿using DTOConverter.Converter;
using DTOConverter.DTOModel;
using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTest.TestDTOConverter
{
    [TestFixture]
    class TestDTOTeamConverter
    {

        [Test]
        public void Test_if_Team_can_be_converted_to_DTO_with_Players()
        {
            DTOTeam dtoTeam = new DTOTeam();
            Team team = new Team() { Id = 1, Draw = 0, Loss = 0, Win = 1, Name = "Team Awesome", Players = new List<Player>()
                {
                    new Player() { Id = 1, Name = "Ole" },
                    new Player() { Id = 2, Name = "Ulla" }
                },
            };
            DTOTeamConverter dtoTeamConverter = new DTOTeamConverter();
            dtoTeam = dtoTeamConverter.Convert(team);
            Assert.AreEqual(team.Id, dtoTeam.Id);
            Assert.NotNull(dtoTeam.DtoPlayers); 
        }



        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_argument_exception_if_players_is_null()
        {
           DTOTeamConverter dtoTeamConverter = new DTOTeamConverter();
            Team team = new Team() { Id = 1, Draw = 0, Loss = 0, Win = 1, Name = "Team Awesome" };
            DTOTeam dtoTeam = dtoTeamConverter.Convert(team);
        }


        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_if_argurment_exception_is_thrown_if_team_is_null()
        {
            Team team = null;
            DTOTeamConverter dtoTeamConverter = new DTOTeamConverter();
            dtoTeamConverter.Convert(team);
        }

    }
}
