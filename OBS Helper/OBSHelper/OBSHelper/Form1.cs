using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OBSHelper
{
    public partial class Form1 : Form
    {
        #region Form Members

        private const string URLBase = "C:/Users/Nathan/Documents/WSU Stream/OBS Helper/";
        private const string URLInfoLeft = "Info_Left.txt";
        private const string URLInfoRight = "Info_Right.txt";
        private const string URLNameLeft = "Name_Left.txt";
        private const string URLNameRight = "Name_Right.txt";
        private const string URLCamLeft = "Camera_Left.txt";
        private const string URLCamRight = "Camera_Right.txt";
        private const string URLScoreLeft = "Score_Left.txt";
        private const string URLScoreRight = "Score_Right.txt";
        private const string URLCommentatorLeft = "CommentatorName_Left.txt";
        private const string URLCommentatorRight = "CommentatorName_Right.txt";
        private const string URLPort1 = "Port_1.txt";
        private const string URLPort2 = "Port_2.txt";
        private const string URLPort3 = "Port_3.txt";
        private const string URLPort4 = "Port_4.txt";
        private const string URLTournamentName = "Tournament_Name.txt";
        private const string URLTournamentProgress = "Tournament_Progress.txt";
        private Timer InfoLeftTimer = new Timer();
        private Timer InfoRightTimer = new Timer();
        private Timer NameLeftTimer = new Timer();
        private Timer NameRightTimer = new Timer();
        private Timer CamLeftTimer = new Timer();
        private Timer CamRightTimer = new Timer();
        private Timer ScoreLeftTimer = new Timer();
        private Timer ScoreRightTimer = new Timer();
        private Timer CommentatorLeftTimer = new Timer();
        private Timer CommentatorRightTimer = new Timer();
        private Timer Port1Timer = new Timer();
        private Timer Port2Timer = new Timer();
        private Timer Port3Timer = new Timer();
        private Timer Port4Timer = new Timer();
        private Timer TournamentNameTimer = new Timer();
        private Timer TournamentProgressTimer = new Timer();
        private uint InfoLeftSaveWait = 0;
        private uint InfoRightSaveWait = 0;
        private uint NameLeftSaveWait = 0;
        private uint NameRightSaveWait = 0;
        private uint CamLeftSaveWait = 0;
        private uint CamRightSaveWait = 0;
        private uint ScoreLeftSaveWait = 0;
        private uint ScoreRightSaveWait = 0;
        private uint CommentatorLeftSaveWait = 0;
        private uint CommentatorRightSaveWait = 0;
        private uint Port1SaveWait = 0;
        private uint Port2SaveWait = 0;
        private uint Port3SaveWait = 0;
        private uint Port4SaveWait  = 0;
        private uint TournamentNameSaveWait = 0;
        private uint TournamentProgressSaveWait = 0;
        private uint SaveWaitThresh = 2;
        SoundManager soundManager;

        #endregion

        #region Construction

        public Form1()
        {
            InitializeComponent();
            LoadValues();
            InfoLeftTimer.Tick += InfoLeftTimerTick;
            InfoRightTimer.Tick += InfoRightTimerTick;
            NameLeftTimer.Tick += NameLeftTimerTick;
            NameRightTimer.Tick += NameRightTimerTick;
            CamLeftTimer.Tick += CamLeftTimerTick;
            CamRightTimer.Tick += CamRightTimerTick;
            ScoreLeftTimer.Tick += ScoreLeftTimerTick;
            ScoreRightTimer.Tick += ScoreRightTimerTick;
            CommentatorLeftTimer.Tick += CommentatorLeftTimerTick;
            CommentatorRightTimer.Tick += CommentatorRightTimerTick;
            Port1Timer.Tick += Port1TimerTick;
            Port2Timer.Tick += Port2TimerTick;
            Port3Timer.Tick += Port3TimerTick;
            Port4Timer.Tick += Port4TimerTick;
            TournamentNameTimer.Tick += TournamentNameTimerTick;
            TournamentProgressTimer.Tick += TournamentProgressTimerTick;
            soundManager = new SoundManager();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(-7, 0); // Fits left side of screen on test machine 
        }

        #endregion

        #region Timer Ticks

        private void TournamentProgressTimerTick(object sender, EventArgs e)
        {
            TournamentProgressSaveWait++;
            if (TournamentProgressSaveWait > SaveWaitThresh)
            {
                TournamentProgressTimer.Stop();
                TournamentProgressSaveWait = 0;
                WriteTournamentProgress();
            }
        }

        private void TournamentNameTimerTick(object sender, EventArgs e)
        {
            TournamentNameSaveWait++;
            if (TournamentNameSaveWait > SaveWaitThresh)
            {
                TournamentNameTimer.Stop();
                TournamentNameSaveWait = 0;
                WriteTournamentName();
            }
        }

        private void ScoreRightTimerTick(object sender, EventArgs e)
        {
            ScoreRightSaveWait++;
            if (ScoreRightSaveWait > SaveWaitThresh)
            {
                ScoreRightTimer.Stop();
                ScoreRightSaveWait = 0;
                WriteSCoreRight();
            }
        }

        private void CamRightTimerTick(object sender, EventArgs e)
        {
            CamRightSaveWait++;
            if (CamRightSaveWait > SaveWaitThresh)
            {
                CamRightTimer.Stop();
                CamRightSaveWait = 0;
                //WriteCamRight(); // Not being used with current stream layout
            }
        }

        private void ScoreLeftTimerTick(object sender, EventArgs e)
        {
            ScoreLeftSaveWait++;
            if (ScoreLeftSaveWait > SaveWaitThresh)
            {
                ScoreLeftTimer.Stop();
                ScoreLeftSaveWait = 0;
                WriteScoreLeft();
            }
        }

        private void CamLeftTimerTick(object sender, EventArgs e)
        {
            CamLeftSaveWait++;
            if (CamLeftSaveWait > SaveWaitThresh)
            {
                CamLeftTimer.Stop();
                CamLeftSaveWait = 0;
                //WriteCamLeft(); // Not being used with current stream layout
            }
        }

        private void NameRightTimerTick(object sender, EventArgs e)
        {
            NameRightSaveWait++;
            if (NameRightSaveWait > SaveWaitThresh)
            {
                NameRightTimer.Stop();
                NameRightSaveWait = 0;
                WriteNameRight();
            }
        }

        private void NameLeftTimerTick(object sender, EventArgs e)
        {
            NameLeftSaveWait++;
            if (NameLeftSaveWait > SaveWaitThresh)
            {
                NameLeftTimer.Stop();
                NameLeftSaveWait = 0;
                WriteNameLeft();
            }
        }

        private void InfoRightTimerTick(object sender, EventArgs e)
        {
            InfoRightSaveWait++;
            if (InfoRightSaveWait > SaveWaitThresh)
            {
                InfoRightTimer.Stop();
                InfoRightSaveWait = 0;
                WriteInfoRight();
            }
        }

        private void InfoLeftTimerTick(object sender, EventArgs e)
        {
            InfoLeftSaveWait++;
            if (InfoLeftSaveWait > SaveWaitThresh)
            {
                InfoLeftTimer.Stop();
                InfoLeftSaveWait = 0;
                WriteInfoLeft();
            }
        }

        private void Port1TimerTick(object sender, EventArgs e)
        {
            Port1SaveWait++;
            if (Port1SaveWait > SaveWaitThresh)
            {
                Port1Timer.Stop();
                Port1SaveWait = 0;
                WritePort1();
                if (checkBox11.Checked) updateNameLeft();
                if (checkBox21.Checked) updateNameRight();
            }
        }

        private void Port2TimerTick(object sender, EventArgs e)
        {
            Port2SaveWait++;
            if (Port2SaveWait > SaveWaitThresh)
            {
                Port2Timer.Stop();
                Port2SaveWait = 0;
                WritePort2();
                if (checkBox12.Checked) updateNameLeft();
                if (checkBox22.Checked) updateNameRight();
            }
        }

        private void Port3TimerTick(object sender, EventArgs e)
        {
            Port3SaveWait++;
            if (Port3SaveWait > SaveWaitThresh)
            {
                Port3Timer.Stop();
                Port3SaveWait = 0;
                WritePort3();
                if (checkBox13.Checked) updateNameLeft();
                if (checkBox23.Checked) updateNameRight();
            }
        }

        private void Port4TimerTick(object sender, EventArgs e)
        {
            Port4SaveWait++;
            if (Port4SaveWait > SaveWaitThresh)
            {
                Port4Timer.Stop();
                Port4SaveWait = 0;
                WritePort4();
                if (checkBox14.Checked) updateNameLeft();
                if (checkBox24.Checked) updateNameRight();
            }
        }

        private void CommentatorRightTimerTick(object sender, EventArgs e)
        {
            CommentatorRightSaveWait++;
            if (CommentatorRightSaveWait > SaveWaitThresh)
            {
                CommentatorRightTimer.Stop();
                CommentatorRightSaveWait = 0;
                WriteCommentatorRight();
            }
        }

        private void CommentatorLeftTimerTick(object sender, EventArgs e)
        {
            CommentatorLeftSaveWait++;
            if (CommentatorLeftSaveWait > SaveWaitThresh)
            {
                CommentatorLeftTimer.Stop();
                CommentatorLeftSaveWait = 0;
                WriteCommentatorLeft();
            }
        }

        #endregion

        #region Button Clicks

        private void LeftDecButton_Click(object sender, EventArgs e)
        {
            if (ScoreLeft.Value == 0) return;
            ScoreLeft.Value--;
        }

        private void LeftIncButton_Click(object sender, EventArgs e)
        {
            if (ScoreLeft.Value == 100) return;
            ScoreLeft.Value++;
        }

        private void RightDecButton_Click(object sender, EventArgs e)
        {
            if (ScoreRight.Value == 0) return;
            ScoreRight.Value--;
        }

        private void RightIncButton_Click(object sender, EventArgs e)
        {
            if (ScoreRight.Value == 100) return;
            ScoreRight.Value++;
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            ScoreLeft.Value = 0;
            ScoreRight.Value = 0;
            NameLeft.Text = "";
            NameRight.Text = "";
            InfoLeft.Text = "";
            InfoRight.Text = "";
            port1.Text = "";
            port2.Text = "";
        }

        private void SwapPlayerButton_Click(object sender, EventArgs e)
        {
            string left = NameLeft.Text;
            NameLeft.Text = NameRight.Text;
            NameRight.Text = left;
        }

        private void SwapCameraButton_Click(object sender, EventArgs e)
        {
            string left = port1.Text;
            port1.Text = port2.Text;
            port2.Text = left;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InfoLeft.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InfoRight.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkBox11.Checked = false;
            checkBox12.Checked = false;
            checkBox13.Checked = false;
            checkBox14.Checked = false;
            updateNameLeft();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            checkBox21.Checked = false;
            checkBox22.Checked = false;
            checkBox23.Checked = false;
            checkBox24.Checked = false;
            updateNameRight();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            port1.Text = "";
            checkBox11.Checked = false;
            checkBox21.Checked = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            port2.Text = "";
            checkBox12.Checked = false;
            checkBox22.Checked = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tournamentName.Text = "";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tournamentProgress.Text = "";
        }

        private void swapInfoButton_Click(object sender, EventArgs e)
        {
            string temp = InfoLeft.Text;

            InfoLeft.Text = InfoRight.Text;
            InfoRight.Text = temp;

            return;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            CommentatorLeft.Text = "";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CommentatorRight.Text = "";
        }

        private void swapCommentatorButton_Click(object sender, EventArgs e)
        {
            string temp = CommentatorLeft.Text;

            CommentatorLeft.Text = CommentatorRight.Text;
            CommentatorRight.Text = temp;

            return;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            port3.Text = "";
            checkBox13.Checked = false;
            checkBox23.Checked = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            port4.Text = "";
            checkBox14.Checked = false;
            checkBox24.Checked = false;
        }

        #endregion

        #region Text/Score Changed 

        private void InfoLeft_TextChanged(object sender, EventArgs e)
        {
            InfoLeftTimer.Stop();
            InfoLeftSaveWait = 0;
            InfoLeftTimer.Start();
        }

        private void InfoRight_TextChanged(object sender, EventArgs e)
        {
            InfoRightTimer.Stop();
            InfoRightSaveWait = 0;
            InfoRightTimer.Start();
        }

        private void NameLeft_TextChanged(object sender, EventArgs e)
        {
            NameLeftTimer.Stop();
            NameLeftSaveWait = 0;
            NameLeftTimer.Start();
        }

        private void NameRight_TextChanged(object sender, EventArgs e)
        {
            NameRightTimer.Stop();
            NameRightSaveWait = 0;
            NameRightTimer.Start();
        }

        private void CamLeft_TextChanged(object sender, EventArgs e)
        {
            CamLeftTimer.Stop();
            CamLeftSaveWait = 0;
            CamLeftTimer.Start();
        }

        private void CamRight_TextChanged(object sender, EventArgs e)
        {
            CamRightTimer.Stop();
            CamRightSaveWait = 0;
            CamRightTimer.Start();
        }

        private void ScoreLeft_ValueChanged(object sender, EventArgs e)
        {
            ScoreLeftTimer.Stop();
            ScoreLeftSaveWait = 0;
            ScoreLeftTimer.Start();
        }

        private void ScoreRight_ValueChanged(object sender, EventArgs e)
        {
            ScoreRightTimer.Stop();
            ScoreRightSaveWait = 0;
            ScoreRightTimer.Start();
        }

        private void tournamentName_TextChanged(object sender, EventArgs e)
        {
            TournamentNameTimer.Stop();
            TournamentNameSaveWait = 0;
            TournamentNameTimer.Start();
        }

        private void tournamentProgress_TextChanged(object sender, EventArgs e)
        {
            TournamentProgressTimer.Stop();
            TournamentProgressSaveWait = 0;
            TournamentProgressTimer.Start();
        }

        private void CommentatorLeft_TextChanged(object sender, EventArgs e)
        {
            CommentatorLeftTimer.Stop();
            CommentatorLeftSaveWait = 0;
            CommentatorLeftTimer.Start();
        }

        private void CommentatorRight_TextChanged(object sender, EventArgs e)
        {
            CommentatorRightTimer.Stop();
            CommentatorRightSaveWait = 0;
            CommentatorRightTimer.Start();
        }

        private void port1_TextChanged(object sender, EventArgs e)
        {
            Port1Timer.Stop();
            Port1SaveWait = 0;
            Port1Timer.Start();
        }

        private void port2_TextChanged(object sender, EventArgs e)
        {
            Port2Timer.Stop();
            Port2SaveWait = 0;
            Port2Timer.Start();
        }

        private void port3_TextChanged(object sender, EventArgs e)
        {
            Port3Timer.Stop();
            Port3SaveWait = 0;
            Port3Timer.Start();
        }

        private void port4_TextChanged(object sender, EventArgs e)
        {
            Port4Timer.Stop();
            Port4SaveWait = 0;
            Port4Timer.Start();
        }

        #endregion

        #region File Writers

        private void WriteInfoLeft()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLInfoLeft);
                if (InfoLeft.Text == "") InfoLeft.Text = " ";
                writer.WriteLine(InfoLeft.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WriteInfoRight()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLInfoRight);
                if (InfoRight.Text == "") InfoRight.Text = " ";
                writer.WriteLine(InfoRight.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WriteNameLeft()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLNameLeft);
                if (NameLeft.Text == "") NameLeft.Text = " ";
                writer.WriteLine(NameLeft.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WriteNameRight()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLNameRight);
                if (NameRight.Text == "") NameRight.Text = " ";
                writer.WriteLine(NameRight.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WriteCommentatorLeft()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLCommentatorLeft);
                if (CommentatorLeft.Text == "") CommentatorLeft.Text = " ";
                writer.WriteLine(CommentatorLeft.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WriteCommentatorRight()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLCommentatorRight);
                if (CommentatorRight.Text == "") CommentatorRight.Text = " ";
                writer.WriteLine(CommentatorRight.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WritePort1()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLPort1);
                if (port1.Text == "") port1.Text = " ";
                writer.WriteLine(port1.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WritePort2()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLPort2);
                if (port2.Text == "") port2.Text = " ";
                writer.WriteLine(port2.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WritePort3()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLPort3);
                if (port3.Text == "") port3.Text = " ";
                writer.WriteLine(port3.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WritePort4()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLPort4);
                if (port4.Text == "") port4.Text = " ";
                writer.WriteLine(port4.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WriteTournamentName()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLTournamentName);
                if (tournamentName.Text == "") tournamentName.Text = " ";
                writer.WriteLine(tournamentName.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WriteTournamentProgress()
        {
            try
            {
                StreamWriter writer = new StreamWriter(URLBase + URLTournamentProgress);
                if (tournamentProgress.Text == "") tournamentProgress.Text = " ";
                writer.WriteLine(tournamentProgress.Text);
                writer.Close();
            }
            catch
            {
                return;
            }
        }

        private void WriteScoreLeft()
        {
            StreamWriter writer = new StreamWriter(URLBase + URLScoreLeft);
            writer.WriteLine(ScoreLeft.Value.ToString());
            writer.Close();
        }

        private void WriteSCoreRight()
        {
            StreamWriter writer = new StreamWriter(URLBase + URLScoreRight);
            writer.WriteLine(ScoreRight.Value.ToString());
            writer.Close();
        }

        #endregion

        #region Sound Effect Buttons

        private void soundButton_horn_Click(object sender, EventArgs e)
        {
            soundManager.PlayAirHorn();
        }

        private void soundButton_circus_Click(object sender, EventArgs e)
        {
            soundManager.PlayCircus();
        }

        private void soundButton_trombone_Click(object sender, EventArgs e)
        {
            soundManager.PlayTrombone();
        }

        private void soundButton_applause_Click(object sender, EventArgs e)
        {
            soundManager.PlayApplause();
        }

        private void soundButton_boo_Click(object sender, EventArgs e)
        {
            soundManager.PlayBoo();
        }

        private void soundButton_snore_Click(object sender, EventArgs e)
        {
            soundManager.PlaySnore();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            soundManager.PlayGorilla();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            soundManager.PlayChimp();
        }

        #endregion

        #region Logic Functions

        private void LoadValues()
        {
            string value;
            try
            {
                StreamReader InfoLeftReader = new StreamReader(URLBase + URLInfoLeft);
                value = InfoLeftReader.ReadLine();
                InfoLeft.Text = value;
                InfoLeftReader.Close();

                StreamReader InfoRightReader = new StreamReader(URLBase + URLInfoRight);
                value = InfoRightReader.ReadLine();
                InfoRight.Text = value;
                InfoRightReader.Close();

                StreamReader NameLeftReader = new StreamReader(URLBase + URLNameLeft);
                value = NameLeftReader.ReadLine();
                NameLeft.Text = value;
                NameLeftReader.Close();

                StreamReader NameRightReader = new StreamReader(URLBase + URLNameRight);
                value = NameRightReader.ReadLine();
                NameRight.Text = value;
                NameRightReader.Close();

                //StreamReader CamLeftReader = new StreamReader(URLBase + URLCamLeft);
                //value = CamLeftReader.ReadLine();
                //port1.Text = value;
                //CamLeftReader.Close();

                //StreamReader CamRightReader = new StreamReader(URLBase + URLCamRight);
                //value = CamRightReader.ReadLine();
                //port2.Text = value;
                //CamRightReader.Close();

                StreamReader ScoreLeftReader = new StreamReader(URLBase + URLScoreLeft);
                value = ScoreLeftReader.ReadLine();
                ScoreLeft.Value = Convert.ToInt32(value);
                ScoreLeftReader.Close();

                StreamReader ScoreRightReader = new StreamReader(URLBase + URLScoreRight);
                value = ScoreRightReader.ReadLine();
                ScoreRight.Value = Convert.ToInt32(value);
                ScoreRightReader.Close();

                StreamReader CommentatorLeftReader = new StreamReader(URLBase + URLCommentatorLeft);
                value = CommentatorLeftReader.ReadLine();
                CommentatorLeft.Text = value;
                CommentatorLeftReader.Close();

                StreamReader CommentatorRightReader = new StreamReader(URLBase + URLCommentatorRight);
                value = CommentatorRightReader.ReadLine();
                CommentatorRight.Text = value;
                CommentatorRightReader.Close();

                StreamReader Port1Reader = new StreamReader(URLBase + URLPort1);
                value = Port1Reader.ReadLine();
                port1.Text = value;
                Port1Reader.Close();

                StreamReader Port2Reader = new StreamReader(URLBase + URLPort2);
                value = Port2Reader.ReadLine();
                port2.Text = value;
                Port2Reader.Close();

                StreamReader Port3Reader = new StreamReader(URLBase + URLPort3);
                value = Port3Reader.ReadLine();
                port3.Text = value;
                Port3Reader.Close();

                StreamReader Port4Reader = new StreamReader(URLBase + URLPort4);
                value = Port4Reader.ReadLine();
                port4.Text = value;
                Port4Reader.Close();

                StreamReader TournamentNameReader = new StreamReader(URLBase + URLTournamentName);
                value = TournamentNameReader.ReadLine();
                tournamentName.Text = value;
                TournamentNameReader.Close();

                StreamReader TournamentProgressReader = new StreamReader(URLBase + URLTournamentProgress);
                value = TournamentProgressReader.ReadLine();
                tournamentProgress.Text = value;
                TournamentProgressReader.Close();
            }
            catch
            {
                return;
            }
        }

        private void updateNameLeft()
        {
            List<string> PortNames = new List<string>();

            if (checkBox11.Checked && port1.Text != "") PortNames.Add(port1.Text);
            if (checkBox12.Checked && port2.Text != "") PortNames.Add(port2.Text);
            if (checkBox13.Checked && port3.Text != "") PortNames.Add(port3.Text);
            if (checkBox14.Checked && port4.Text != "") PortNames.Add(port4.Text);

            if (PortNames.Count == 0)
            {
                NameLeft.Text = "";
                return;
            }

            if (PortNames.Count == 1)
            {
                NameLeft.Text = PortNames[0];
                return;
            }

            string nameLeft = "";

            foreach (string name in PortNames)
            {
                nameLeft += (name + " + ");
            }

            nameLeft = nameLeft.Substring(0, nameLeft.Length - 3);
            NameLeft.Text = nameLeft;
        }

        private void updateNameRight()
        {
            List<string> PortNames = new List<string>();

            if (checkBox21.Checked && port1.Text != "") PortNames.Add(port1.Text);
            if (checkBox22.Checked && port2.Text != "") PortNames.Add(port2.Text);
            if (checkBox23.Checked && port3.Text != "") PortNames.Add(port3.Text);
            if (checkBox24.Checked && port4.Text != "") PortNames.Add(port4.Text);

            if (PortNames.Count == 0)
            {
                NameRight.Text = "";
                return;
            }

            if (PortNames.Count == 1)
            {
                NameRight.Text = PortNames[0];
                return;
            }

            string nameRight = "";

            foreach (string name in PortNames)
            {
                nameRight += (name + " + ");
            }

            nameRight = nameRight.Substring(0, nameRight.Length - 3);
            NameRight.Text = nameRight;
        }

        #endregion

        #region Port Checkboxes

        private void checkBox11_CheckedChanged(object sender, EventArgs e)
        {
            updateNameLeft();
        }

        private void checkBox12_CheckedChanged(object sender, EventArgs e)
        {
            updateNameLeft();
        }

        private void checkBox13_CheckedChanged(object sender, EventArgs e)
        {
            updateNameLeft();
        }

        private void checkBox14_CheckedChanged(object sender, EventArgs e)
        {
            updateNameLeft();
        }

        private void checkBox21_CheckedChanged(object sender, EventArgs e)
        {
            updateNameRight();
        }

        private void checkBox22_CheckedChanged(object sender, EventArgs e)
        {
            updateNameRight();
        }

        private void checkBox23_CheckedChanged(object sender, EventArgs e)
        {
            updateNameRight();
        }

        private void checkBox24_CheckedChanged(object sender, EventArgs e)
        {
            updateNameRight();
        }

        #endregion
    }
}
