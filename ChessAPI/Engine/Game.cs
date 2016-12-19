using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ChessAPI.Engine
{
    public class Game
    {
        static public Game _instance = new Game();
        public State s;
        public Tree t;
        public int ply;
        public static Game Instance
        {
            get
            {
                return _instance;
            }
        }
        Game() { }
        public void InitiliazeGame()
        {
            ply = 3;

            s = new State();
            s.InitiliazeBoard();
            s.color = 'w';


            t = new Tree(ply);
            t.BuildTree(s);
        }

        public List<Move> GetPlayableMoves()
        {
            return t.root.state.GetPlayableMoves();
        }

        public bool GenerateMove(int move_id)
        {
            t.ExpandWithNewRoot(move_id);
            return true;
        }

        public int GetMoveIdOfAI()
        {
            return t.GetBestMove();
        }
    }
}