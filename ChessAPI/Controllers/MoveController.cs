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
            Move new_move = JsonConvert.DeserializeObject<Move>(value.ToString());

            new_move.to_x = 99;
            List<Move> response = new List<Move>();
            response.Add(new_move);

            return response;
        }

        
    }
}
