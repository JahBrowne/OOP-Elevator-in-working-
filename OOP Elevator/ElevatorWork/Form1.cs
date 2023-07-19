using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Data.Odbc;
using System.Data.OleDb;


namespace ElevatorWork
{
    public partial class Form1 : Form
    {
        public bool TopGateOpen; //Opens top floor doors
        public bool TopGateClosed; //Closes top floor doors
        public bool BotGateOpen; //Opens ground doors
        public bool BotGateClosed; // Closes ground floor doors
        public int CurrentFloor = 0; //Current floor is 0
        bool startdown = false;
        bool startup = false;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Enabled = true;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (startup == true)
            {
                pictureBox1.Enabled = true;
                startup = true;
                pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y - 40);
            }
            if (startdown == true)
            {
                pictureBox1.Enabled = true;
                startdown = true;
                pictureBox1.Location = new Point(pictureBox1.Location.X, pictureBox1.Location.Y + 40);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (BotGateOpen == true)
            {
                CloseBotElevator();
                ElevatorUp();
                OpenTopElevator();
                TopFloorDisplay();
            }
            else
            {
                OpenTopElevator();
                TopFloorDisplay();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (TopGateOpen == true)
            {
                CloseTopElevator();
                ElevatorDown();
                OpenBotElevator();
                BotFloorDisplay();
            }
            else
            {
                OpenBotElevator();
                BotFloorDisplay();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Refers to the elevator going to the first floor
                if (BotGateOpen == true) //It checks is the ground floor is open
                {
                    //If opened it would close and move the elevator up and open top doors
                    CloseBotElevator();
                    OpenTopElevator();
                    ElevatorUp();
                    TopFloorDisplay();
                }
                else
                {
                    //If not then it moves the elevator up and opens top doors
                    ElevatorUp();
                    OpenTopElevator();
                    TopFloorDisplay();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Refers to the ground floor
                if (TopGateOpen == true) //If the top doors are open
                {
                    //If they are then it closes the doors and the elevator moves down
                    CloseTopElevator();
                    OpenBotElevator();
                    ElevatorDown();
                    BotFloorDisplay();
                }
                else
                {
                    //If not then the elevator moves down and opens the bottom doors
                    ElevatorDown();
                    OpenBotElevator();
                    BotFloorDisplay();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void OpenTopElevator()
        {
            TopGateOpen = true;
            TopGateClosed = false;
            // If the top gate is closed then the doors move to show it opened
            pictureBox3.Location = new Point(pictureBox3.Location.X - 40, pictureBox3.Location.Y);
            pictureBox2.Location = new Point(pictureBox2.Location.X + 40, pictureBox2.Location.Y);
        }

        public void CloseTopElevator()
        {
            TopGateClosed = true;
            TopGateOpen = false;
            // If the top gate is open then the doors move to show it closed
            pictureBox3.Location = new Point(pictureBox3.Location.X + 40, pictureBox3.Location.Y);
            pictureBox2.Location = new Point(pictureBox2.Location.X - 40, pictureBox2.Location.Y);
        }

        public void OpenBotElevator()
        {
            BotGateOpen = true;
            BotGateClosed = false;
            // If the top gate is closed then the doors move to show it opened
            pictureBox4.Location = new Point(pictureBox4.Location.X + 40, pictureBox4.Location.Y);
            pictureBox5.Location = new Point(pictureBox5.Location.X - 40, pictureBox5.Location.Y);
        }

        public void CloseBotElevator()
        {
            BotGateClosed = true;
            BotGateOpen = false;
            // If the bottom gate is opened then the doors move to show it closed
            pictureBox4.Location = new Point(pictureBox4.Location.X - 40, pictureBox4.Location.Y);
            pictureBox5.Location = new Point(pictureBox5.Location.X + 40, pictureBox5.Location.Y);
        }

        public void ElevatorUp()
        {
            TimerCount.Enabled = true;
            startup = true;
        }

        public void ElevatorDown()
        {
            TimerCount.Enabled = true;
            startdown = true;
        }

        public void TopFloorDisplay()
        {
            CurrentFloor = 1; //Makes the current floor integer 1
            //Displays all labels as the cureent floor
            label1.Text = CurrentFloor.ToString();
            label2.Text = CurrentFloor.ToString();
            label3.Text = CurrentFloor.ToString();
        }

        public void BotFloorDisplay()
        {
            CurrentFloor = 0;//Makes the current floor integer 1
            //Displays all labels as the cureent floor
            label1.Text = CurrentFloor.ToString();
            label2.Text = CurrentFloor.ToString();
            label3.Text = CurrentFloor.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string dbconnection = "Provider=Microsoft.ACE.OLEDB.12.0;" + @"data source = Status.accdb";
            string dbcommand = "SELECT Floor, TimeEle FROM Tabel11";
            OleDbDataAdapter DataAdapterTest = new OleDbDataAdapter(dbcommand, dbconnection);

            DataSet ds = new DataSet();
            DataAdapterTest.Fill(ds);
            DataTable ElevatorData = ds.Tables[0];

            foreach (DataRow row in ElevatorData.Rows)
            {
                ListView.Items.Add(row["Floor"] + "" + row["TimeEle"]);
            }
        }
    }
}
