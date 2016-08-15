using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.Domain.Entities;
using GameStore.Domain.Abstract;

namespace GameStore.Domain.Concrete
{
    public class EFGameRepository:IGameRepository
    {
        EFDBContext context = new EFDBContext();
        
        public IEnumerable<Game> Games
        {
            get { return context.Games; }
        }
    }
}
