using System;
using System.Linq;
using indianpoker.model;

namespace indianpoker
{
    class Program
    {
        static void Main(string[] args)
        {
            var argsList = args.ToList();
            int pidx = argsList.IndexOf("-p");
            int pnum = pidx != -1 ? int.Parse(argsList[pidx + 1]) : new Random().Next(1, 10);
            if (pidx != -1) argsList = argsList.Where((a, i) => i != pidx && i != pidx + 1).ToList();

            int cidx = argsList.IndexOf("-c");
            int cnum = cidx != -1 ? int.Parse(argsList[cidx + 1]) : new Random().Next(pnum+1, 11);
            if (cidx != -1) argsList = argsList.Where((a, i) => i != cidx && i != cidx + 1).ToList();

            int[] cards = argsList.Select(i => int.Parse(i)).ToArray();
            Game game = new Game(pnum, cnum, cards);
            game.Play();
        }
    }
}
