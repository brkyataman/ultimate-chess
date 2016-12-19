﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAPI.Engine
{
    /*
     Holds state of game with board and turn data.
     */
    public class State
    {
        private Square[,] board;

        public Information info;
        public char color { get; set; }
        public char game_state { get; set; } //opening, mid, end
        public int turn { get; set; }
        //public int king_black_x { get; set; }
        //public int king_black_y { get; set; }
        //public int king_white_x { get; set; }
        //public int king_white_y { get; set; }
        //public int numberOfPieces_white { get; set; }
        //public int numberOfPieces_black { get; set; }

        private double toRadian;

        public Square[,] getBoard()
        {
            return this.board;
        }

        public State()
        {
            this.board = new Square[8, 8];
            toRadian = (Math.PI / 180);
        }

        public State GetCopy(){
            State new_state = new State();
            new_state.board = new Square[8, 8];
            new_state.turn = this.turn;
            new_state.color = this.color;
            new_state.game_state = this.game_state;
            new_state.info = this.info.GetCopy();
            //new_state.king_black_x = this.king_black_x;
            //new_state.king_black_y = this.king_black_y;
            //new_state.king_white_x = this.king_white_x;
            //new_state.king_white_y = this.king_white_y;
            //new_state.numberOfPieces_black = this.numberOfPieces_black;
            //new_state.numberOfPieces_white = this.numberOfPieces_white;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    new_state.board[i, j] = new Square();
                    new_state.board[i, j].isOccupied = this.board[i, j].isOccupied;
                    if (this.board[i, j].occupiedBy != null)
                    {
                        new_state.board[i, j].occupiedBy = this.board[i, j].occupiedBy.GetCopy();
                    }
                    
                }
            }
            return new_state;
        }

        public void setColor(char _color)
        {
            this.color = _color;
        }
        public char getColor()
        {
            return this.color;
        }
        public void InitiliazeBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = new Square();
                }
            }
            for (int j = 0; j < 8; j++)
            {
                this.board[1, j].occupiedBy = new Pawn(1, j, 'w', true);
                this.board[1, j].isOccupied = true;
                this.board[6, j].occupiedBy = new Pawn(6, j, 'b', true);
                this.board[6, j].isOccupied = true;
            }
            char colorOfPieces = 'w';
            for (int i = 0; i < 2; i++)
            {
                this.board[0 + 7 * i, 0].occupiedBy = new Rook(0 + 7 * i, 0, colorOfPieces, true);
                this.board[0 + 7 * i, 0].isOccupied = true;
                this.board[0 + 7 * i, 1].occupiedBy = new Knight(0 + 7 * i, 1, colorOfPieces, true);
                this.board[0 + 7 * i, 1].isOccupied = true;
                this.board[0 + 7 * i, 2].occupiedBy = new Bishop(0 + 7 * i, 2, colorOfPieces, true);
                this.board[0 + 7 * i, 2].isOccupied = true;
                this.board[0 + 7 * i, 3].occupiedBy = new Queen(0 + 7 * i, 3, colorOfPieces, true);
                this.board[0 + 7 * i, 3].isOccupied = true;
                this.board[0 + 7 * i, 4].occupiedBy = new King(0 + 7 * i, 4, colorOfPieces, true);
                this.board[0 + 7 * i, 4].isOccupied = true;
                this.board[0 + 7 * i, 5].occupiedBy = new Bishop(0 + 7 * i, 5, colorOfPieces, true);
                this.board[0 + 7 * i, 5].isOccupied = true;
                this.board[0 + 7 * i, 6].occupiedBy = new Knight(0 + 7 * i, 6, colorOfPieces, true);
                this.board[0 + 7 * i, 6].isOccupied = true;
                this.board[0 + 7 * i, 7].occupiedBy = new Rook(0 + 7 * i, 7, colorOfPieces, true);
                this.board[0 + 7 * i, 7].isOccupied = true;

                colorOfPieces = 'b';
            }

            this.turn = 0;
            this.game_state = 'o';
            //numberOfPieces_black = 16;
            //numberOfPieces_white = 16;
            info = new Information();
            //info.GatherInformation(this.board);
        }
        ////Generates a move with given params, eg. from e2 to e3
        //public bool GenerateMove(int from_x, int from_y, int to_x, int to_y)
        //{

        //    board[to_x, to_y].occupiedBy = board[from_x, from_y].occupiedBy;
        //    board[to_x, to_y].occupiedBy.x = to_x;
        //    board[to_x, to_y].occupiedBy.y = to_y;
        //    board[to_x, to_y].occupiedBy.initialPos = false;
        //    board[from_x, from_y].occupiedBy = null;
        //    board[from_x, from_y].isOccupied = false;
        //    board[to_x, to_y].isOccupied = true;
        //    return true;
        //}

        public bool GenerateMove(Move move)
        {
            if (move.msg == 'C')
            {
                string key = "";
                char target_c = color == 'w' ? 'b' : 'w';
                key += target_c.ToString() + "_" + board[move.to_x,move.to_y].occupiedBy.GetType().Name.ToLower();
                info.info[key] -= 1; //B_KİNG ????

                //TODO: burayı sil debug için burası
                //System.Console.WriteLine("THIS IS A CAPTUE!");
            }

            board[move.to_x, move.to_y].occupiedBy = board[move.from_x, move.from_y].occupiedBy;
            board[move.to_x, move.to_y].occupiedBy.UpdateData(move.to_x, move.to_y);
            board[move.to_x, move.to_y].isOccupied = true;

            board[move.from_x, move.from_y].occupiedBy = null;
            board[move.from_x, move.from_y].isOccupied = false;

            if (move.msg == 'R')
            {

                if (move.to_y > 4) //Right castling //daha önce 4 değil 0'dı fakat hata almıştım.
                {
                    board[move.to_x, move.to_y - 1].occupiedBy = board[move.to_x, move.to_y + 1].occupiedBy;
                    board[move.to_x, move.to_y - 1].occupiedBy.UpdateData(move.to_x, move.to_y - 1);
                    board[move.to_x, move.to_y - 1].isOccupied = true;

                    board[move.to_x, move.to_y + 1].occupiedBy = null;
                    board[move.to_x, move.to_y + 1].isOccupied = false;
                }
                else //Left castling
                {
                    board[move.to_x, move.to_y + 1].occupiedBy = board[move.to_x, move.to_y - 2].occupiedBy;
                    board[move.to_x, move.to_y + 1].occupiedBy.UpdateData(move.to_x, move.to_y + 1);
                    board[move.to_x, move.to_y + 1].isOccupied = true;

                    board[move.to_x, move.to_y - 2].occupiedBy = null;
                    board[move.to_x, move.to_y - 2].isOccupied = false;
                }
            }

            
            //Change game state opening to mid-game. If both queens moved.
            if (this.game_state == 'o' && 
                (!board[0,3].isOccupied || board[0,3].isOccupied && !board[0,3].occupiedBy.initialPos) && 
                (!board[7,3].isOccupied || board[7,3].isOccupied && !board[7,3].occupiedBy.initialPos)) {
                this.game_state = 'm';
            }
            //Change game state mid to end-game. If both have no queens or queen + a minor piece.
            if(this.game_state=='m'&& (this.info.info["w_queen"] == 0 || (info.info["w_rook"] == 0 &&info.info["w_knight"] + info.info["w_bishop"] == 1))
                && (this.info.info["b_queen"] == 0 || (info.info["b_rook"] == 0 && info.info["b_knight"] + info.info["b_bishop"] == 1)))
            {
                this.game_state = 'e';
            }

            if (board[move.to_x, move.to_y].occupiedBy.GetType().Equals(typeof(King)))
            {
                if (this.color == 'w')
                {
                    info.info["w_king_x"] = move.to_x;
                    info.info["w_king_y"] = move.to_y;
                    //king_white_x = move.to_x;
                    //king_white_y = move.to_y;
                }
                else
                {

                    info.info["b_king_x"] = move.to_x;
                    info.info["b_king_y"] = move.to_y;
                    //king_black_x = move.to_x;
                    //king_black_y = move.to_y;
                }
            }
            return true;
        }
        //Turns playable moves of current board
        public List<Move> GetPlayableMoves()
        {

            int k = 1;
            int numbOfPieces = info.info["b_piece_num"];
            if (this.color == 'w')
            {
                k = 0;
                numbOfPieces = info.info["w_piece_num"];
            }


            //If color is White then it will start searching for 'x' from 0 to 7
            //If Black then start searching for 'x' from 7 to 0 
            int i = 0;
            i = i + 7 * k;
            List<Move> playableMoves = new List<Move>();
            while (true)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (numbOfPieces == 0)
                    {
                        return playableMoves;
                    }
                    if (board[i, j].isOccupied && board[i, j].occupiedBy.color == this.color)
                    {
                        foreach (var move in board[i, j].occupiedBy.PlayableMoves(board))
                        {
                            State temp = GetCopy();
                            temp.GenerateMove(move);
                            if(!temp.IsKingChecked())
                                playableMoves.Add(move);
                        }
                        numbOfPieces--;
                    }
                }
                i = i + (1 - 2 * k);
                if (i == -1 || i == 8) { break; }
            }
            return playableMoves;
        }

        //Evaluates board
        public int Evaluate(char _target_color)
        {
            //Piece number eval.
            double result =
                Weights.PAWN_VAL * (info.info["w_pawn"] - info.info["b_pawn"]) +
                Weights.ROOK_VAL * (info.info["w_rook"] - info.info["b_rook"]) +
                Weights.BISHOP_VAL * (info.info["w_bishop"] - info.info["b_bishop"]) +
                Weights.KNIGHT_VAL * (info.info["w_knight"] - info.info["b_knight"]) +
                Weights.QUEEN_VAL * (info.info["w_queen"] - info.info["b_queen"]);
            result = _target_color == 'w' ? result : result * -1;

            //Piece position eval.
            int k = 0; //w
            int t = -1;
            if (_target_color == 'b')
            {
                k = 1;
                t = 1;
            }
            int piece_num = info.info[_target_color + "_piece_num"];
            for (int i = 0; i < 8; i++ )
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[t * (7 * k - i), j].isOccupied && board[t * (7 * k - i), j].occupiedBy.color == _target_color)
                    {
                        switch (board[t * (7 * k - i), j].occupiedBy.GetType().Name)
                        {
                            case "Pawn":
                                result += Weights.PAWN_TABLE[i, j];
                                break;
                            case "Rook":
                                result += Weights.ROOK_TABLE[i, j];
                                break;
                            case "Knight":
                                result += Weights.KNIGHT_TABLE[i, j];
                                break;
                            case "Bishop":
                                result += Weights.BISHOP_TABLE[i, j];
                                break;
                            case "Queen":
                                result += Weights.QUEEN_TABLE[i, j];
                                break;
                            case "King":
                                result += Weights.KING_TABLE[i, j];
                                break;
                        }

                        piece_num--;
                    }
                    if (piece_num == 0)
                        break;
                }
                if (piece_num == 0)
                    break;
            }
            return Convert.ToInt32(result);
        }

        public bool IsKingChecked()
        {
            int king_x = info.info[color.ToString() + "_king_x"];
            int king_y = info.info[color.ToString() + "_king_y"];
            //int king_x = king_white_x;
            //int king_y = king_white_y;
            //if (color == 'B')
            //{
            //    king_x = king_black_x;
            //    king_y = king_white_y;
            //}
            for (double k = 0.0; k < 360; k += 90.0)
            {
                //Queen or Rook [(1,0),(0,1),(-1,0),(0,-1)]
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Sin(k * toRadian)),
                    Convert.ToInt32(Math.Cos(k * toRadian)), typeof(Queen), typeof(Rook), true))
                {
                    return true;
                }
                //Queen or Bishop [(1,1),(1,-1),(-1,1),(-1,-1)]
                if (IsThisPiece(king_x, king_y, (k < 91.0 ? 1 : -1),
                    Convert.ToInt32(Math.Tan((45 + k) * toRadian)), typeof(Queen), typeof(Bishop), true))
                {
                    return true;
                }

                //Knight [(2,1),(2,-1),(-2,1),(-2,1)]
                if (IsThisPiece(king_x, king_y, (k < 91.0 ? 2 : -2),
                    Convert.ToInt32(Math.Tan((45 + k) * toRadian)), typeof(Knight), _loop: false))
                {
                    return true;
                }

                //Knight [(1,2),(1,-2),(-1,2),(-1,-2)]
                if (IsThisPiece(king_x, king_y, (k < 91.0 ? 1 : -1),
                    Convert.ToInt32(Math.Tan((45 + k) * toRadian)) * 2, typeof(Knight), _loop: false))
                {
                    return true;
                }

                //Pawn [(-1,1),(-1,-1) if opponent is white else (1,1),(1,-1)]
                if (IsThisPiece(king_x, king_y, k < 91.0 ? -1 : -2,
                    Convert.ToInt32(Math.Tan((360 - k + 45) * toRadian)), typeof(Pawn), _loop: false))
                {
                    return true;
                }
                //King-Direct [(1,0),(0,1),(-1,0),(0,-1)]
                if (IsThisPiece(king_x, king_y, Convert.ToInt32(Math.Sin(k * toRadian)),
                    Convert.ToInt32(Math.Cos(k * toRadian)), typeof(King), _loop: false))
                {
                    return true;
                }
                //King-Diagonal [(1,1),(1,-1),(-1,1),(-1,-1)]
                if (IsThisPiece(king_x, king_y, (k < 91.0 ? 1 : -1),
                    Convert.ToInt32(Math.Tan((45 + k) * toRadian)), typeof(King), _loop: false))
                {
                    return true;
                }

            }
            return false;
        }

        //Queen-Rook and Queen-Bishop checks same squares, so there is two types.
        private bool IsThisPiece(int king_x, int king_y, int _i, int _j, Type _type1, Type _type2 = null, bool _loop = false)
        {

            if (_type1.Equals(typeof(Pawn)) && color == 'w')
            {
                _i = _i * -1;
            }
            int inc_i = _i;
            int inc_j = _j;
            while (isValid(king_x + _i, king_y + _j))
            {
                if (board[king_x + _i, king_y + _j].isOccupied)
                {
                    if (board[king_x + _i, king_y + _j].occupiedBy.color != color &&
                        (board[king_x + _i, king_y + _j].occupiedBy.GetType().Equals(_type1)
                        || board[king_x + _i, king_y + _j].occupiedBy.GetType().Equals(_type2)))
                    {
                        //TODO: refactor..
                        if (_type1.Equals(typeof(Pawn)) &&
                            Math.Abs(_i) == 2 &&
                            !((Pawn)board[king_x + _i, king_y + _j].occupiedBy).initialPos)
                        {
                            return false;
                        }
                        //System.Console.WriteLine("King is check by a " + (board[king_x + _i, king_y + _j].occupiedBy.GetType().ToString()));
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (!_loop) { break; }
                _i += inc_i;
                _j += inc_j;
            }
            return false;
        }

        private bool isValid(int _x, int _y)
        {
            if (_x > 7 || _x < 0 || _y > 7 || _y < 0)
            {
                return false;
            }
            return true;
        }
    }
}
