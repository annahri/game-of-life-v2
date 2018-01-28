using System;
using System.Drawing;

namespace Gameoflife {
    public class Box {
        float x;
        float y;
        float size;
        bool status;

        public float X { 
            get { return x; } 
        }
        public float Y {
            get { return y; } 
        }
        public float Size {
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

        public Box(float x, float y, float size) {
            this.x = x;
            this.y = y;
            this.size = size;
        }
    }
}
