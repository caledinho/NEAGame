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
                const double friction = 0.985; //energy loss to the cloth
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
                int topWall = borderSize;
                int bottomWall = tableHeight - borderSize;

                //write code to detect collision with left and right walls
                if (PositionX < leftWall)
                {
                    VelocityX = -VelocityX * 0.9; // ball hits the cushion, the horizontal velocity is reversed and a slight energy loss is applied
                    PositionX = leftWall; //prevents the ball from sticking out the cushion
                }
                else if ((PositionX + ballRadius*2) > rightWall)
                {
                    VelocityX = -VelocityX * 0.9; //reverses velocity and applies energy loss
                    PositionX = rightWall - (ballRadius*2); //stops ball sticking out
                }
                //write code to detect collision with top and bottom walls
                if (PositionY < topWall)
                {
                    VelocityY = -VelocityY * 0.9; Reverses the Y direction velocity and applies energy loss;
                    PositionY = topWall; //makes sure the ball doesnt stick out
                }
                else if ((PositionY + ballRadius*2) > bottomWall)
                {
                    VelocityY= -VelocityY * 0.9; //reverses Y direction velocity and applies energy loss
                    PositionY = bottomWall - (ballRadius*2); //ensures the ball doesnt stick out
                }
            }
            public void CheckCollision(Ball otherBall)
            {
                //use pythagoras to check distance between other ball
                double dx = (this.PositonX + ballRadius) - (otherBall.PositionX + ballRadius); //gets the x-distance
                double dy = (this.PositionY + ballRadius) - (otherBall.PositionY + ballRadius); //gets the y-distance
                double distance = Math.Sqrt( (Math.Pow(dx,2) + Math.Pow(dy,2) ); //finds total distance

                //check if balls are colliding
                if (distance < (ballRadius * 2))
                {
                    /finds the exact distance needed to fix the overlap
                    double overlap = (ballRadius * 2) - distance; //check the overlap of the balls
                    double overlapX = (dx / distance) * (overlap / 2); 
                    double overlapY = (dx / distance) * (overlap / 2);
                    
                    //adds or subtracts necessary distance to fix the overlap
                    this.PositionX += overlapX;
                    this.PositionY += overlapY;
                    otherBall.PositionX -= overlapX;
                    otherBall.PositionY -= overlapY;

                    //conserve momentum using The Law of Conservation of Momentum
                    double tempVelocityX = this.VelocityX; //stores the object balls current velocity
                    double tempVelocityY = this.VelocityY;

                    //Ball A takes Ball B's Velocity and Ball B takes Ball A's
                    
                    this.VelocityX = otherBall.VelocityX;
                    this.VelocityY = otherBall.VelocityY;
                    otherBall.VelocityX = tempVelocityX;
                    otherBall.VelocityY = tempVelocityY;
                    
                }
            }
            public bool isPotted(bool foul)
            {
                
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
