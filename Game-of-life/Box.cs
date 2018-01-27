using System;
using System.Drawing;

namespace Gameoflife {
    public class Box {
        int x;
        int y;
        int size;
        bool status;

        public int X { 
            get { return x; } 
        }
        public int Y {
            get { return y; } 
        }
        public int Size {
            get { return size; }
        }
        public bool Status { 
            get { return status; }
            set { status = value; } 
        }
        public string ID {
            get;
            set;
        }


        public Box(int x, int y, int size) {
            this.x = x;
            this.y = y;
            this.size = size;
        }
    }
}
