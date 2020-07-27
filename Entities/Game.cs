using System;

namespace Entities
{
    public abstract class Game
    {
        public abstract void Create();
        public abstract void Open();
        public abstract bool Bet(Bet bet);
        public abstract void Closed();

    }
}
