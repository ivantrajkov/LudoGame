using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LudoGame
{
    public class Piece
    {
        public string color {  get; set; }
        public bool isAtHome {  get; set; }
        public bool isFinished {  get; set; }
        public bool passedHalf { get; set; }=false;

        public Piece(string color)
        {
            this.color = color;
            isFinished = false;
            this.isAtHome = true;
          
        }

        public override string ToString()
        {
            return String.Format("{0}",color);
        }
    }
}
