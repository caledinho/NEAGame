using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NEA_Game
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
            this.Paint += Form1_Paint; //adds the code to form
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics; //creates the graphics to paint using code
            
            int border = 40; //sets how much bigger the border/cushion is bigger the the table
            int buttonBorderX = 5; //length of the border
            int buttonBorderY = 3; //width of button broder

            int baulkLineWidth = 1; //sets the width of the baulk line
            int baulkLineLength = this.ClientSize.Height - 2 * border;
            double baulkLineY = border + baulkLineLength * 0.25;
            int BaulkEndX = 0;
            int BaulkX = border;
            
            Rectangle tablerect = new Rectangle(border, border, this.ClientSize.Width - 2 * border, this.ClientSize.Height - 2 * border); //creates a rectangle with these dimensions
            //Rectangle ButtonRectangle = new Rectangle(buttonBorderX, buttonBorderY, this.ClientSize.Width, this.ClientSize.Height + 2* border);
            Rectangle baulkRect = new Rectangle(border,border,baulkLineWidth,baulkLineLength);
            
            using (Brush cushion = new SolidBrush(Color.White)) //creates a brush for the cushions of the table
                g.FillRectangle(cushion, ClientRectangle); //draws the rectangle for the cushion in the cushion colour
            
            using (Brush felt = new SolidBrush(Color.Purple)) //creates a brush colour for the actual table
                g.FillRectangle(felt, tablerect); //
            
            //using (Brush buttonBrush = new SolidBrush(Color.LightGray))
                //g.FillRectangle(buttonBrush, ButtonRectangle);
            
            int pocketSize = 35; //sets the size of the pocket to 35
            int adjustment = 3; //adjusts the pockets to the correct position
            int ballSize = 25; //size of the balls

            //Creating the different brushes for the other ball colours

            Brush pocketBrush = Brushes.Black; 
            Brush redBall = Brushes.Red; 
            Brush yellowBall = Brushes.Yellow; 
            Brush whiteBall = Brushes.WhiteSmoke;
            Brush blackBall = Brushes.Black;

            //drawing the circles for the pockets
            
            g.FillEllipse(pocketBrush, border - pocketSize / 2 - adjustment, border - pocketSize / 2 - adjustment, pocketSize, pocketSize); //top left pocket
            g.FillEllipse(pocketBrush, border - pocketSize / 2 - adjustment, this.ClientSize.Height - border - pocketSize / 2 + adjustment, pocketSize, pocketSize);//bottom left pocket
            g.FillEllipse(pocketBrush, (this.ClientSize.Width / 2) - pocketSize / 2, border - pocketSize / 2 - adjustment, pocketSize, pocketSize);//top middle pocket
            g.FillEllipse(pocketBrush, (this.ClientSize.Width / 2) - pocketSize / 2, this.ClientSize.Height - border - pocketSize / 2 + adjustment, pocketSize, pocketSize);//bottom middle pocket
            g.FillEllipse(pocketBrush, this.ClientSize.Width - border - pocketSize / 2 + adjustment, border - pocketSize / 2 - adjustment, pocketSize, pocketSize);//top right pocket
            g.FillEllipse(pocketBrush, this.ClientSize.Width - border - pocketSize / 2 + adjustment, this.ClientSize.Height - border - pocketSize / 2 + adjustment, pocketSize, pocketSize);//bottom right pocket

            //drawing the different balls

            g.FillEllipse(redBall,(this.ClientSize.Width/2) - border, (this.ClientSize.Height/2) - border, ballSize, ballSize); 
            g.FillEllipse(yellowBall, (this.ClientSize.Width / 2) - border + 40, (this.ClientSize.Height / 2) - border, ballSize, ballSize);
            g.FillEllipse(whiteBall, (this.ClientSize.Width / 2) - border + 80, (this.ClientSize.Height / 2) - border, ballSize, ballSize);
            g.FillEllipse(blackBall, (this.ClientSize.Width / 2) - border + -40, (this.ClientSize.Height / 2) - border, ballSize, ballSize);
        }
        

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        public class Ball
        {
            public int PositionX { get; set; } public int PositionY { get; set; }
            public double VelocityX {  get; set; } public double VelocityY { get; set; }
            public double AngularVelocity { get; set; }
            private const int ballRadius = 12; //half of 25 pixel ball size

            public void UpdateMovement() 
            { 
                PositionX = PositionX + VelocityX; //calculates the new X-Value
                PositionY = PositionY + VelocityY; //calculates the new Y-Value
                double const friction = 0.985; //energy loss to the cloth
                VelocityX = VelocityX * friction; //updates the new X-Velocity
                VelocityY = VelocityY * friction; //updates the new Y-Velocity
                //Create zero velocity to stop infinite tiny movements
                if (Maths.Abs(VelocityX) < 0.1) 
                {
                    VelocityX = 0; //if velocity is less than 0.1 sets the speed to 0
                }    
                if (Maths.Abs(VelocityY) < 0.1)
                {
                    VelocityY = 0; //if velocity is less than 0.1 sets the speed to 0
                }
            }
            public void HandleCushionHit(int tableWidth, int tableHeight,int borderSize)
            {
                int leftWall = borderSize;
                int rightWall = tableWidth - borderSize;
                int topWall = 
            }
            public void CheckCollision(){}
            public bool isPotted(bool foul)
            {
                return foul=true;
            }
        }
        public class CueBall : Ball
        {

        }
        public class ObjectBall : Ball
        {

        }
        public class BlackBall : ObjectBall
        {

        }
        public class RedBall : ObjectBall
        {

        }
        public class YellowBall : ObjectBall
        {

        }

        public class GameState
        {
            public string Name { get; set; }
            public int Score { get; set; }
            public string AiDifficulty {  get; set; }
            public string CurrentPlayer { get; set; }
            public void ProcessShot() { }
            public bool CheckForFoul() { return false; }
            public void ManageTurn() { }
            public bool CheckWinLoss() { return false; }
        }
        public class GameManager
        {
            public void StartGame() { }
            public void GameLoop() { }
            public void EndShotTurn() { }
            
        }
        public void UpdateMovement() { }
        public void HandleColision() { }
        public void HandleCushionHit() { }
        public void ManageTurn() { }
        public void CheckEndGame() { }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Record(string username, int score)
        {
            
        }        
    }
}
