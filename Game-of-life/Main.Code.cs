using System.Drawing;
using System;

namespace Gameoflife {
    public partial class Main {

        // Vaiables
        public int res;
        public int rows;
        public int cols;
        public int offset;
        public int[,] grid;
        public Box[,] boxx;

        Random rand = new Random();

        private void Initialize(){
            res = 10;
            cols = (this.Width - 1) / res;
            rows = (this.Height - 1) / res;
            offset = 0;
            grid = new int[rows,cols];
            boxx = new Box[rows, cols];

			for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    var x = j * res;
                    var y = i * res;
                    var s = res - offset;

                    boxx[i,j] = new Box(x,y,s) {
                        ID = $"Box {i},{j}"
                    };
                }
            }
        }

        private void Draw(Graphics gr) {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                   gr.FillRectangle(
                        (boxx[i,j].Status) ? Brushes.DarkGreen : Brushes.White,
                        boxx[i,j].X,
                        boxx[i,j].Y,
                        boxx[i,j].Size,
                        boxx[i,j].Size
                    );
                }
            }
        }

        private void UpdateDraw() {
            Box[,] boxes = new Box[rows,cols];
			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < cols; j++) {
					var x = j * res;
					var y = i * res;
					var s = res - offset;
                    boxes[i,j] = new Box(x,y,s) { ID = $"Box {i}, {j}" };

                    var state = boxx[i,j].Status;
                    var neighbors = CountNeighbors(i,j, boxx);

                    if (!state && neighbors == 3) {
                        boxes[i,j].Status = true;
                    } else if (state && (neighbors < 2 || neighbors > 3)) {
                        boxes[i,j].Status = false;
                    } else {
                        boxes[i,j].Status = state;
                    }
				}
			}
            boxx = boxes;
        }

        private int CountNeighbors(int x, int y, Box[,] boxes) {
            var sum = 0;

            #region first method
            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    try {
                        if (boxes[x+i,y+j].Status) {
                            sum++;
                        }
                    } catch {
                        continue;
                    }
                }
            }
			#endregion

            if (boxes[x,y].Status) {
                sum -= 1;
            }

            return sum;
        }

        private void CheckId(Graphics gr) {
            var coordinates = this.PointToClient(MousePosition);
            var i = coordinates.Y / res;
            var j = coordinates.X / res;

            if (i < boxx.GetLength(0) && j < boxx.GetLength(1) && i >= 0 && j >= 0) {
                gr.DrawString(
                    boxx[i,j].ID,
                new Font("Arial", 10),
                Brushes.Red,
                coordinates.X,
                coordinates.Y - 10);
            }
        }

        private void Turn() {
            var coordinates = this.PointToClient(MousePosition);
            var i = coordinates.Y / res;
            var j = coordinates.X / res;

            if (i < boxx.GetLength(0) && j < boxx.GetLength(1) && i >= 0 && j >= 0) {
                if (!boxx[i,j].Status) {
                    boxx[i,j].Status = true;
                } else {
                    boxx[i,j].Status = false;
                }
            }
        }

        private void Randomize() {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    boxx[i,j].Status = TrueFalse();
                }
            }
        }

        private void Clear() {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    boxx[i,j].Status = false;
                }
            }                    
        }

        private bool TrueFalse() {
            return (rand.NextDouble() >= 0.5);
        }
    }
}
