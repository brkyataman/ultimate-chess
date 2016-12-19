using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using ChessAPI.Engine;

namespace ChessAPI.Controllers
{
    public class MoveController : ApiController
    {

        [HttpPost]
        [Route("move")]
        public IEnumerable<Move> GetMove([FromBody] dynamic value)
        {
            Game game = Game.Instance;
            //int new_move_id = JsonConvert.DeserializeObject<Move>(value.ToString());
            int new_move_id = Convert.ToInt32(value);
            game.GenerateMove(new_move_id);

            List<Move> ai_playable_moves = game.GetPlayableMoves();
            int move_id_ai = game.GetMoveIdOfAI();
            game.GenerateMove(move_id_ai);

            List<Move> response = new List<Move>();
            response.Add(ai_playable_moves[move_id_ai]);

            return response;
        }

        [HttpGet]
        [Route("playablemoves")]
        public IEnumerable<Move> GetPlayableMoves()
        {
            Game game = Game.Instance;
            List<Move> response = game.GetPlayableMoves();
            
            return response;
        }

        [HttpGet]
        [Route("startgame")]
        public bool StartGame()
        {
            Game game = Game.Instance;
            //game = new Game();
            game.InitiliazeGame();
            return true;
        }
        
    }
}
