using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace indianpoker.model
{
    class Game
    {
        readonly Player[] players;
        readonly int[] cards;
        readonly int cnum;

        public Game(int pnum, int cnum, int[] cards = null)
        {
            if (cnum < pnum) cnum = pnum;
            this.cnum = cnum;
            if (cards == null || cards.Count() != pnum)
            {
                cards = Enumerable
                    .Range(1, cnum)
                    .OrderBy(i => Guid.NewGuid())
                    .Take(pnum)
                    .ToArray();
            }
            this.cards = cards;
            this.players = Enumerable.Range(0, pnum).Select(i => new Player(i, cnum, cards)).ToArray();
            Console.Write(pnum + " " + cnum + " ");
            Console.Write("[ ");
            cards.ToList().ForEach(i => Console.Write(i+" "));
            Console.Write("]");
            Console.WriteLine();
        }

        public void Play()
        {
            int pnum = players.Count();
            int turn = 0;
            while (true)
            {
                int id = turn % pnum;
                int res = players[id].Predict(turn);
                Console.Write(_announce(id,res));
                if (res != 0) break;
                Console.Write(", ");
                turn++;
            }
        }
        private string _announce(int id, int res)
        {
            string rtn = (char)(id+'A') + "=>";
            if (res == 0) rtn += "?";
            else if (res == 1) rtn += "MIN";
            else if (res == 2) rtn += "MAX";
            else if (res == 3) rtn += "MID";
            return rtn;
        }
    }
}
