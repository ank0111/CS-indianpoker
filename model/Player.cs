using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace indianpoker.model
{
    class Player
    {
        private int id;
        private int cnum;
        private int[] cards;
        private List<int> kouho;

        public Player(int id, int cnum, int[] cards)
        {
            this.id = id;
            this.cnum = cnum;
            this.cards = cards.Clone() as int[];
            this.cards[id] = 0;
            this.kouho = Enumerable.Range(1, cnum).Except(this.cards).ToList();
        }

        public int Predict(int turn)
        {
            if (turn > 0)
            {
                int pnum = cards.Count();
                int[] bcards = cards.Clone() as int[];
                kouho = kouho.Where(i => _isOk(i)).ToList();
                bool _isOk(int i)
                {
                    bcards[id] = i;
                    Player bplayer = new Player((pnum + id - 1) % pnum, cnum, bcards);
                    return bplayer.Predict(turn - 1) == 0;
                }
            }
            bool win = kouho.Skip(1).Zip(kouho.SkipLast(1))
                .Select(i => i.First - i.Second)
                .All(i => i == 1);
            if (win)
            {
                if (kouho.First() == 1) return 1;
                if (kouho.Last() == cnum) return 2;
                return 3;
            }
            return 0;
        }
    }
}
