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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AIMenu AIGame = new AIMenu();
            AIGame.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Game OfflineGame = new Game(); //if the user clicks offline button, send them to them game
            OfflineGame.Show(); //shows the Game UI
            this.Hide(); //hides the Main Menu UI
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0); //Exits the game
        }
    }
}
