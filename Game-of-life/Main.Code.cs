using System.Drawing;
using System;
using System.Windows.Forms;

namespace Gameoflife {
    public partial class Main {

        // Vaiables
        public int res;
        public int rows;
        public int cols;
		public int offset;
		public int offsetX;
		public int offsetY;
		public Box[,] boxx;
        public int generation = 0;
		public int OldPopulationCount = 0;
		Growth growth;
        Color setColor = Color.White;

		Random rand = new Random();


        /// <summary>
        /// Initialize the specified obj.
        /// </summary>
        /// <returns>The initialize.</returns>
        /// <param name="obj">Object.</param>
        void Initialize(Control obj){
            res = 10;
            cols = obj.ClientSize.Width / res;
            rows = obj.ClientSize.Height / res;
            offset = 1;
            offsetX = 3;
            offsetY = 40-6 + 22;
            boxx = new Box[rows, cols];

			Generate(boxx);
        }


        /// <summary>
        /// Generate the specified box.
        /// </summary>
        /// <returns>The generate.</returns>
        /// <param name="box">Box.</param>
        Box[,] Generate(Box[,] box) {
			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < cols; j++) {
					var x = (j * res) + offsetX;
					var y = i * res;
					var s = res - offset;

					box[i,j] = new Box(x,y,s) {
						ID = $"Box {i},{j}"
					};
				}
			}
            return box;
        }


        /// <summary>
        /// Draw the specified gr.
        /// </summary>
        /// <returns>The draw.</returns>
        /// <param name="gr">Gr.</param>
        void Draw(Graphics gr) {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                   gr.FillRectangle(
                        new SolidBrush(setColor),
                        boxx[i,j].X,
                        boxx[i,j].Y,
                        boxx[i,j].Size,
                        boxx[i,j].Size
                    );
                }
            }
        }


        /// <summary>
        /// Draws the only true.
        /// </summary>
        /// <param name="gr">Gr.</param>
        void DrawOnlyTrue(Graphics gr) {
			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < cols; j++) {
                    if (!boxx[i,j].Status) continue;
                    gr.FillRectangle(
                        new SolidBrush(setColor),
                         boxx[i,j].X,
                         boxx[i,j].Y,
                         boxx[i,j].Size,
                         boxx[i,j].Size
                     );
				}
			}
        }


        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="gr">Graphics.</param>
        /// <param name="obj">Object.</param>
        void DrawGrid(Graphics gr, Control obj) {
            Pen pen = new Pen(Color.Gray);
            for (int i = 0; i < rows; i++) {
                var y = i * res - offset;

                gr.DrawLine(
                    pen,
                    new Point(offsetX,y),
                    new Point(obj.ClientRectangle.Width - offsetX, y)
                );
            }

			for (int j = 0; j < cols; j++) {
                var x = j * res - offset;
				gr.DrawLine(
					pen,
					new Point(x + offsetX,0),
                    new Point(x + offsetX,obj.ClientRectangle.Height - offsetY)
				);
			}
        }

        /// <summary>
        /// Draws the border.
        /// </summary>
        /// <param name="gr">Gr.</param>
        void DrawBorder(Graphics gr, Control obj) {
			Pen pen = new Pen(Color.Gray);
            Rectangle border = new Rectangle(
                offsetX - 1,
                0,
                obj.ClientRectangle.Width - offsetX - 2,
                obj.ClientRectangle.Height - offsetY);
            gr.DrawRectangle(
                pen,
                border
            );
        }

        /// <summary>
        /// Updates the draw.
        /// </summary>
        void UpdateDraw() {
            Box[,] boxes = new Box[rows,cols];
            Generate(boxes);
			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < cols; j++) {
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
            generation++;
        }


        /// <summary>
        /// Counts the neighbors.
        /// </summary>
        /// <returns>The neighbors.</returns>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="boxes">Boxes.</param>
        int CountNeighbors(int x, int y, Box[,] boxes) {
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


        /// <summary>
        /// Checks the identifier.
        /// </summary>
        /// <param name="gr">Gr.</param>
        /// <param name="obj">Object.</param>
        void CheckId(Graphics gr, Control obj) {
            var coordinates = obj.PointToClient(MousePosition);
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


        /// <summary>
        /// Turn the specified obj.
        /// </summary>
        /// <returns>The turn.</returns>
        /// <param name="obj">Object: Mouse position relative to</param>
        void Turn(Control obj) {
            var coordinates = obj.PointToClient(MousePosition);
            var i = coordinates.Y / res;
            var j = (coordinates.X - offsetX)/ res;

            if (i < boxx.GetLength(0) && j < boxx.GetLength(1) && i >= 0 && j >= 0) {
                if (!boxx[i,j].Status) {
                    boxx[i,j].Status = true;
                } else {
                    boxx[i,j].Status = false;
                }
            }
        }

        /// <summary>
        /// Randomize this instance.
        /// </summary>
        void Randomize() {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    boxx[i,j].Status = TrueFalse();
                }
            }
        }

        /// <summary>
        /// Counts the population.
        /// </summary>
        /// <returns>The population.</returns>
        int CountPopulation() {
			var count = 0;
			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < cols; j++) {
					if (!boxx[i,j].Status) continue;
					count++;
				}
			}
            return count;
        }

        /// <summary>
        /// Checks the population.
        /// </summary>
        /// <returns><c>true</c>, if population was > 0 , <c>false</c> otherwise.</returns>
        bool CheckPopulation() {
            return (CountPopulation() > 0);
        }

        /// <summary>
        /// Clear this instance.
        /// </summary>
        void Clear() {
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    boxx[i,j].Status = false;
                }
            }                    
        }

        /// <summary>
        /// Checks the growth.
        /// </summary>
		void CheckGrowth() {
            int CurrentPopulationCount = CountPopulation();

			if (CurrentPopulationCount > OldPopulationCount) {
                growth = Growth.INCREASING;
			} else if (CurrentPopulationCount < OldPopulationCount) {
                growth = Growth.DECREASING;
            } else {
				growth = Growth.STAGNANT;
			}

            OldPopulationCount = CurrentPopulationCount;
		}


        /// <summary>
        /// Trues the false.
        /// </summary>
        /// <returns><c>true</c>, if false was trued, <c>false</c> otherwise.</returns>
        bool TrueFalse() {
            return (rand.NextDouble() >= 0.5);
        }

        enum Growth {
            INCREASING,
            STAGNANT,
            DECREASING
        }
    }
}
