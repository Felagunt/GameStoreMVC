using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities;

namespace GameStore.Domain.Abstract
{

    public interface IGameRepository
    {
        IEnumerable<Game> Games { get; }//чтобы позволить вызывающему коду получать последовательность объектов Game, ничего не сообщая о том, как или где хранятся или извлекаются данные
    }
}
