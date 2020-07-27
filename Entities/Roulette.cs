using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Roulette : Game
    {
        public string Id { get; set; }
        public Status Status { get; set; }

        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Status = Status.Closed;
        }

        public override void Open()
        {
            this.Status = Status.Open;
        }

        public override bool Bet(Bet bet)
        {
            if (this.Status != Status.Open)
                return false;
            return true;
        }

        public override void Closed()
        {
            this.Status = Status.Closed;
        }
    }
}
