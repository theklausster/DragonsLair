using System;
using System.Collections.Generic;
using DTOConverter.DTOModel;
using Entities;

namespace DTOConverter.Converter
{
    public class DTOGenreConverter : DTOAbstract<Genre, DTOGenre>
    {
        public DTOGenreConverter()
        {
        }

        public override DTOGenre Convert(Genre t)
        {
            if (t == null) throw  new ArgumentException("Genre is null");
            List<DTOGame> dtoGames = new List<DTOGame>();
            if(t.Games == null) return new DTOGenre() { Id = t.Id, Name = t.Name };
            foreach (var game in t.Games)
            {
                dtoGames.Add(new DTOGame() {Id = game.Id, Name = game.Name});
            }
            return new DTOGenre() { Id = t.Id, Name = t.Name, DtoGames = dtoGames};
        }
    }
}