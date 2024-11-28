using System;
using System.Numerics;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Generic;
using System.Drawing;
// using static Tetris.Main_Window;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using static System.Windows.Forms.DataFormats;
using static Tetris.Main_Window;
using Microsoft.VisualBasic.Devices;
using System.Threading.Tasks.Dataflow;
using System.Runtime.InteropServices;

namespace Tetris
{
    public partial class Main_Window : Form
    {
        string enteredText = " ";
        string HS_Name = "";
        int HS_Value = 0;
        int HS_T_Name = 1;
        int HS_S_Name = 2;
        int HS_T_Value = 2;
        int HS_S_Value = 2;
        int HS_T_Level = 3;
        int HS_S_Level = 2;
        int speed_increase = 20;
        int speed_level = 10; //  in welchen lvl abständen die geschwindigkeit erhöht wird
        int current_level = 0;
        int size_x = 10;
        int size_y = 28;
        int tempo_x = 500;
        int start_tempo;
        string Activeblock = "bt_1";
        string Nextblock = "Empty";
        string Storedblock = "Empty";
        string shortstore_1 = "Empty";
        string shortstore_2 = "Empty";
        string Moveblock = "";
        int blockstate = 1; // max 4
        public KastenFactory? Spielfeld;
        public KastenFactory? KastenNextBlock;
        public KastenFactory? KastenStoreBlock;
        public System.Threading.Timer? GameTimer = null;
        int score = 0;
        private List<Box> boxes;
        private List<Box> boxesNB;
        private List<Box> boxesSB;
        private int boxSize = 18;
        int storeblock_x = 5;
        int storeblock_y = 5;
        int nextblock_x = 5;
        int nextblock_y = 5;
        int boxnextstore = 20;
        public Block_Core? Fokusblock;
        public Block_Core? Addedblock_1;
        public Block_Core? Addedblock_2;
        public Block_Core? Addedblock_3;
        private Random RndGenerator;
        int RndKv = 1;
        bool bottom = false;
        bool GO = false;


        public Main_Window()
        {
            boxes = new List<Box>();
            boxesNB = new List<Box>();
            boxesSB = new List<Box>();
            InitializeComponent();
            flowLayoutPanel1.Size = new Size(size_x * boxSize, size_y * boxSize);
            flow_Next.Size = new Size(nextblock_x * boxnextstore, nextblock_y * boxnextstore);
            flow_store.Size = new Size(storeblock_x * boxnextstore, storeblock_y * boxnextstore);
            RndGenerator = new Random();
            start_tempo = tempo_x;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {

        }
        public class Box
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int Value { get; set; }

            public Box(int y, int x, int width, int height, int value)
            {
                X = x;
                Y = y;
                Width = width;
                Height = height;
                Value = value;
            }

            public void Draw(Graphics g)
            {
                // SolidBrush blackBrush = new SolidBrush(Color.Black);
                // SolidBrush whiteBrush = new SolidBrush(Color.White);
                // SolidBrush greenBrush = new SolidBrush(Color.Green);
                // SolidBrush redBrush = new SolidBrush(Color.Red);

                System.Drawing.Rectangle rect = new System.Drawing.Rectangle(X, Y, Width, Height);

                switch (Value)
                {
                    case 0:
                        g.FillRectangle(new SolidBrush(Color.Black), rect);
                        break;
                    case 1:
                        g.FillRectangle(new SolidBrush(Color.Green), rect);
                        break;
                    case 2:
                        g.FillRectangle(new SolidBrush(Color.Violet), rect);
                        break;
                    case 3:
                        g.FillRectangle(new SolidBrush(Color.Red), rect);
                        break;
                    case 4:
                        g.FillRectangle(new SolidBrush(Color.Blue), rect);
                        break;
                    case 5:
                        g.FillRectangle(new SolidBrush(Color.Yellow), rect);
                        break;
                    case 6:
                        g.FillRectangle(new SolidBrush(Color.DarkRed), rect);
                        break;
                    case 7:
                        g.FillRectangle(new SolidBrush(Color.Orange), rect);
                        break;
                    case 8:
                        g.FillRectangle(new SolidBrush(Color.Azure), rect);
                        break;
                    case 9:
                        g.FillRectangle(new SolidBrush(Color.Crimson), rect);
                        break;
                    case 10:
                        g.FillRectangle(new SolidBrush(Color.Gray), rect);
                        break;
                    case 11:
                        g.FillRectangle(new SolidBrush(Color.Gold), rect);
                        break;
                    case 12:
                        g.FillRectangle(new SolidBrush(Color.BlueViolet), rect);
                        break;
                    case 13:
                        g.FillRectangle(new SolidBrush(Color.Brown), rect);
                        break;
                    case 14:
                        g.FillRectangle(new SolidBrush(Color.Aqua), rect);
                        break;
                    case 15:
                        g.FillRectangle(new SolidBrush(Color.DarkBlue), rect);
                        break;
                    case 16:
                        g.FillRectangle(new SolidBrush(Color.DarkOrange), rect);
                        break;
                    case 17:
                        g.FillRectangle(new SolidBrush(Color.DarkMagenta), rect);
                        break;
                    case 18:
                        g.FillRectangle(new SolidBrush(Color.DarkViolet), rect);
                        break;
                    case 19:
                        g.FillRectangle(new SolidBrush(Color.Goldenrod), rect);
                        break;
                    case 20:
                        g.FillRectangle(new SolidBrush(Color.LawnGreen), rect);
                        break;
                }
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            if (boxes != null)
            {
                Graphics g = e.Graphics;

                foreach (Box box in boxes)
                {
                    box.Draw(g);
                }
            }
        }
        private void flow_Next_Paint(object sender, PaintEventArgs e)
        {
            if (boxesNB != null)
            {
                Graphics g = e.Graphics;

                foreach (Box box in boxesNB)
                {
                    box.Draw(g);
                }
            }
        }
        private void flow_store_Paint(object sender, PaintEventArgs e)
        {
            if (boxesSB != null)
            {
                Graphics g = e.Graphics;

                foreach (Box box in boxesSB)
                {
                    box.Draw(g);
                }
            }
        }

        private void Feld_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? selectedOption = "";

            if (SpeedIncreaseComboBox.SelectedItem != null)
                selectedOption = SpeedIncreaseComboBox.SelectedItem.ToString();

            switch (selectedOption)
            {
                case "Fixed":
                    HS_S_Name = 2;
                    HS_S_Value = 2;
                    HS_S_Level = 2;
                    speed_increase = 0;
                    break;
                case "Slow":
                    HS_S_Name = 2;
                    HS_S_Value = 2;
                    HS_S_Level = 2;
                    speed_increase = 20;
                    speed_level = 10;
                    break;
                case "Medium":
                    HS_S_Name = 2;
                    HS_S_Value = 2;
                    HS_S_Level = 2;
                    speed_increase = 20;
                    speed_level = 5;
                    break;
                case "High":
                    HS_S_Name = 2;
                    HS_S_Value = 2;
                    HS_S_Level = 2;
                    speed_increase = 25;
                    speed_level = 3;
                    break;
                case "Max":
                    HS_S_Name = 2;
                    HS_S_Value = 2;
                    HS_S_Level = 2;
                    speed_increase = 25;
                    speed_level = 1;
                    break;
                default:
                    HS_S_Name = 2;
                    HS_S_Value = 2;
                    HS_S_Level = 2;
                    speed_increase = 20;
                    speed_level = 10;
                    break;
            }
        }
        private void Tempo_comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? selectedOption = "";
            if (Speed_Start.SelectedItem != null)
                selectedOption = Speed_Start.SelectedItem.ToString();

            switch (selectedOption)
            {
                case "Tempo x 0":
                    tempo_x = 0;
                    HS_T_Name = 1;
                    HS_T_Value = 2;
                    HS_T_Level = 3;
                    break;
                case "Tempo x 1":
                    tempo_x = 1000;
                    HS_T_Name = 1;
                    HS_T_Value = 2;
                    HS_T_Level = 3;
                    break;
                case "Tempo x 2":
                    tempo_x = 500;
                    HS_T_Name = 1;
                    HS_T_Value = 2;
                    HS_T_Level = 3;
                    break;
                case "Tempo x 4":
                    tempo_x = 250;
                    HS_T_Name = 1;
                    HS_T_Value = 2;
                    HS_T_Level = 3;
                    break;
                case "Tempo x 8":
                    tempo_x = 125;
                    HS_T_Name = 1;
                    HS_T_Value = 2;
                    HS_T_Level = 3;
                    break;
                case "Tempo x 10":
                    tempo_x = 100;
                    HS_T_Name = 1;
                    HS_T_Value = 2;
                    HS_T_Level = 3;
                    break;
                default:
                    tempo_x = 500;
                    HS_T_Name = 1;
                    HS_T_Value = 2;
                    HS_T_Level = 3;
                    break;
            }
            start_tempo = tempo_x;
        }
        private void Pause_button_Click(object sender, EventArgs e)
        {
            // Spiel Pausieren
            switch (Pause_Button.Text)
            {
                case "Pause":
                    if (GameTimer != null)
                    {
                        GameTimer.Dispose();
                        GameTimer = null;
                    }
                    Pause_Button.Text = "Continue";
                    Current_Score.Focus();
                    // Console.WriteLine("Pause aktiv");
                    break;
                case "Continue":
                    Pause_Button.Text = "Pause";
                    Current_Score.Focus();
                    coloring_update();
                    TetrisTimer();
                    // Console.WriteLine("Pause endet");
                    break;
            }
        }

        private void End_button_Click(object sender, EventArgs e)
        {
            this.Close(); // Beenden
        }

        private void Start_button_Click(object sender, EventArgs e)
        {
            // Start!

            Speed_Start.Enabled = false;
            SpeedIncreaseComboBox.Enabled = false;
            Score_Board.Enabled = false;
            GO = false;

            Spielfeld = new KastenFactory(size_y, size_x);
            KastenNextBlock = new KastenFactory(nextblock_y, nextblock_x);
            KastenStoreBlock = new KastenFactory(storeblock_y, storeblock_x);

            tempo_x = start_tempo;
            blockstate = 1;
            score = 0;
            current_level = 0;
            Current_Score.Text = $"Score: {score}";
            HS_Name = "";
            HS_Value = 0;
            enteredText = "";
            Moveblock = "";
            Nextblock = "bt_1";
            Storedblock = "Empty";
            shortstore_1 = "Empty";
            shortstore_2 = "Empty";
            Pause_Button.Text = "Pause";
            /*
            string pathToExcelFile = "C:/Meins/Coding/C#/playground/Tetris/TetrisHighscore.xlsx";
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook TetrisHighscore = xlApp.Workbooks.Open(pathToExcelFile);
            Excel._Worksheet xlWorksheet = TetrisHighscore.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //      für Excel              Nummer, Buchstabe (als Nummer)
            //                              Tempo, Size
            HS_Name = xlWorksheet.Cells[HS_T_Name, HS_S_Name].Value;
            HS_Value = int.Parse(xlWorksheet.Cells[HS_T_Value, HS_S_Value].Value.ToString());

            Highscore_Display.Text = $"Highscore: {HS_Name} -> {HS_Value}";
            Level_Display.Text = $"Level: {current_level}";

            TetrisHighscore.Close();
            xlApp.Quit();
            */
            boxes.Clear();
            boxesNB.Clear();
            boxesSB.Clear();

            int c, v;
            for (int n = 0; n < nextblock_y; n++) // NextBlock
            {
                for (int m = 0; m < nextblock_x; m++)
                {
                    c = m * boxnextstore;
                    v = n * boxnextstore;
                    boxesNB?.Add(new Box(c, v, boxnextstore, boxnextstore, KastenNextBlock.kasten[n, m].Kvalue));
                }
            }
            int z, u;
            for (int s = 0; s < storeblock_y; s++) // StoreBlock
            {
                for (int d = 0; d < storeblock_x; d++)
                {
                    z = d * boxnextstore;
                    u = s * boxnextstore;
                    boxesSB?.Add(new Box(z, u, boxnextstore, boxnextstore, KastenStoreBlock.kasten[s, d].Kvalue));
                }
            }
            int x, y;
            for (int i = 0; i < size_y; i++) // Spielfeld
            {
                for (int j = 0; j < size_x; j++)
                {
                    x = j * boxSize;
                    y = i * boxSize;
                    boxes?.Add(new Box(y, x, boxSize, boxSize, Spielfeld.kasten[i, j].Kvalue));

                    // Console.WriteLine($"X: {i} Y: {j} k_value: {KastenArray.kasten[i, j].Kvalue}");
                    // Console.Write($"{KastenArray.kasten[i, j].Kvalue}");
                }
                // Console.WriteLine();
            }
            int a = 0;
            int b = 0;
            for (; a < size_y;)
            {
                Spielfeld.kasten[a, b].Kvalue = 0;
                b++;
                if (b >= size_x)
                {
                    b = 0;
                    a++;
                }
            }
            // y  x
            //KastenNextBlock.kasten[35, 1].Kvalue = 1;
            //
            // Console.WriteLine("Next Step");

            Insert_Nextblock();
            coloring_update();
            TetrisTimer();
            Current_Score.Focus();
        }

        private void Highscores_button_Click(object sender, EventArgs e)
        {/*
                string pathToExcelFile = "C:/Meins/Coding/C#/playground/Tetris/TetrisHighscore.xlsx";
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook TetrisHighscore = xlApp.Workbooks.Open(pathToExcelFile);
                Excel._Worksheet xlWorksheet = TetrisHighscore.Sheets[1];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                string messageBoxContent = ""; // Add Excel data to this string

                for (int i = 1; i <= rowCount; i++)
                {
                    for (int j = 1; j <= colCount; j++)
                    {
                        object cellValue = xlRange.Cells[i, j].Value2;
                        if (cellValue == null)
                            messageBoxContent += " \t";
                        else
                            messageBoxContent += cellValue.ToString() + "\t";
                    }
                    messageBoxContent += "\n";
                }

                MessageBox.Show(messageBoxContent, "Highscores");
                TetrisHighscore.Close();
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();*/
        }
        public void Highscore_newrecord()
        {/*
            string pathToExcelFile = "C:/Meins/Coding/C#/playground/Tetris/TetrisHighscore.xlsx";
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook TetrisHighscore = xlApp.Workbooks.Open(pathToExcelFile);
            Excel._Worksheet xlWorksheet = TetrisHighscore.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            if (score > HS_Value)
            {
                NameInputDlg dlg = new NameInputDlg();
                dlg.ShowDialog();
                if (dlg.NameHS().Length > 3)
                {
                    enteredText = dlg.NameHS().Substring(0, 3);
                }
                else
                {
                    enteredText = dlg.NameHS();
                }
                Excel.Range ncell = TetrisHighscore.Worksheets[1].Cells[HS_T_Name, HS_S_Name];
                ncell.Value = enteredText;
                Excel.Range scell = TetrisHighscore.Worksheets[1].Cells[HS_T_Value, HS_S_Value];
                scell.Value = score;
                Excel.Range lcell = TetrisHighscore.Worksheets[1].Cells[HS_T_Level, HS_S_Level];
                lcell.Value = current_level;
                TetrisHighscore.Save();
                Highscore_Display.Invoke((System.Windows.Forms.MethodInvoker)delegate { Highscore_Display.Text = $"Highscore: {enteredText} -> {score}"; });
                Marshal.ReleaseComObject(ncell);
                Marshal.ReleaseComObject(scell);
                Marshal.ReleaseComObject(lcell);
            }
            TetrisHighscore.Close();
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            GC.Collect();
            GC.WaitForPendingFinalizers();*/
        }
        private void Current_Score_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    // rotieren
                    Blockstate_change();
                    break;
                case Keys.Down:
                    //current_y ++;
                    Moveblock = "Down";
                    Player_based_Move();
                    break;
                case Keys.Left:
                    //current_x --;
                    Moveblock = "Left";
                    Player_based_Move();
                    break;
                case Keys.Right:
                    //current_x ++;
                    Moveblock = "Right";
                    Player_based_Move();
                    break;
                case Keys.NumPad1:
                    // store block
                    Block_storing();
                    // gelagerten Block verwenden
                    break;
                case Keys.NumPad0:
                    // y -> max
                    Complete_Down();
                    // nach unten komplett
                    break;
                default:
                    break;
            }
        }
        // Wert 0 = Freies Feld
        // Wert 1+ = Belegtes Feld
        public void Blcoktype_choose()
        {
            Random rndblock = new Random();
            int block_chosen = rndblock.Next(1, 9);
            switch (block_chosen)
            {
                case 1:
                    Nextblock = "bt_1";
                    // bt_1 // 4 in Reihe
                    break;
                case 2:
                    Nextblock = "bt_2";
                    // bt_2 // 011/010/010
                    break;
                case 3:
                    Nextblock = "bt_3";
                    // bt_3 // 010/010/011
                    break;
                case 4:
                    Nextblock = "bt_4";
                    // bt_4 // Quadrat
                    break;
                case 5:
                    Nextblock = "bt_5";
                    // bt_5 // 010/011/001
                    break;
                case 6:
                    Nextblock = "bt_6";
                    // bt_6 // 010/011/010
                    break;
                case 7:
                    Nextblock = "bt_7";
                    // bt_7 // 001/011/010
                    break;
                case 8:
                    Nextblock = "bt_1";
                    // bt_1 // 4 in Reihe
                    break;
                default:
                    Nextblock = "bt_1";
                    // bt_1
                    break;
            }
        }
        public void Complete_Down()
        {
            Moveblock = "Down";
            bottom = false;
            while (!bottom)
            {
                Player_based_Move();
            }
        }
        public void Blockstate_change()
        {
            if (Fokusblock == null || Spielfeld == null
                || Addedblock_1 == null || Addedblock_2 == null
                || Addedblock_3 == null)
                return;
            int y_0 = Fokusblock.current_y;
            int x_0 = Fokusblock.current_x;
            int y_1 = Addedblock_1.current_y;
            int x_1 = Addedblock_1.current_x;
            int y_2 = Addedblock_2.current_y;
            int x_2 = Addedblock_2.current_x;
            int y_3 = Addedblock_3.current_y;
            int x_3 = Addedblock_3.current_x;
            Spielfeld.kasten[y_0, x_0].Kvalue = 0;
            Spielfeld.kasten[y_1, x_1].Kvalue = 0;
            Spielfeld.kasten[y_2, x_2].Kvalue = 0;
            Spielfeld.kasten[y_3, x_3].Kvalue = 0;
            if (blockstate < 4)
            {
                blockstate = blockstate + 1;
            }
            else
            {
                blockstate = 1;
            }
            switch (Activeblock)
            {
                case "bt_1":
                    // bt_1 // 4 in Reihe
                    if (blockstate == 1)
                    {
                        Fokusblock.current_y = Fokusblock.current_y;
                        Fokusblock.current_x = Fokusblock.current_x + 1;
                        Addedblock_1.current_y = Addedblock_1.current_y - 1;
                        Addedblock_1.current_x = Addedblock_1.current_x + 2;
                        Addedblock_2.current_y = Addedblock_2.current_y + 1;
                        Addedblock_2.current_x = Addedblock_2.current_x;
                        Addedblock_3.current_y = Addedblock_3.current_y + 2;
                        Addedblock_3.current_x = Addedblock_3.current_x - 1;
                    }
                    if (blockstate == 2)
                    {
                        Fokusblock.current_y = Fokusblock.current_y + 1;
                        Fokusblock.current_x = Fokusblock.current_x;
                        Addedblock_1.current_y = Addedblock_1.current_y + 2;
                        Addedblock_1.current_x = Addedblock_1.current_x + 1;
                        Addedblock_2.current_y = Addedblock_2.current_y;
                        Addedblock_2.current_x = Addedblock_2.current_x - 1;
                        Addedblock_3.current_y = Addedblock_3.current_y - 1;
                        Addedblock_3.current_x = Addedblock_3.current_x - 2;
                    }
                    if (blockstate == 3)
                    {
                        Fokusblock.current_y = Fokusblock.current_y;
                        Fokusblock.current_x = Fokusblock.current_x - 1;
                        Addedblock_1.current_y = Addedblock_1.current_y + 1;
                        Addedblock_1.current_x = Addedblock_1.current_x - 2;
                        Addedblock_2.current_y = Addedblock_2.current_y - 1;
                        Addedblock_2.current_x = Addedblock_2.current_x;
                        Addedblock_3.current_y = Addedblock_3.current_y - 2;
                        Addedblock_3.current_x = Addedblock_3.current_x + 1;
                    }
                    if (blockstate == 4)
                    {
                        Fokusblock.current_y = Fokusblock.current_y - 1;
                        Fokusblock.current_x = Fokusblock.current_x;
                        Addedblock_1.current_y = Addedblock_1.current_y - 2;
                        Addedblock_1.current_x = Addedblock_1.current_x - 1;
                        Addedblock_2.current_y = Addedblock_2.current_y;
                        Addedblock_2.current_x = Addedblock_2.current_x + 1;
                        Addedblock_3.current_y = Addedblock_3.current_y + 1;
                        Addedblock_3.current_x = Addedblock_3.current_x + 2;
                    }
                    break;
                case "bt_4":
                    // braucht keine Aktion
                    // bt_4 // Quadrat
                    break;
                case "bt_2":
                    // bt_2 // 011/010/010
                case "bt_3":
                    // bt_3 // 010/010/011
                case "bt_5":
                    // bt_5 // 010/011/001
                case "bt_6":
                    // bt_6 // 010/011/010
                case "bt_7":
                    // bt_7 // 001/011/010
                    {
                        int BlockToMoveY = 0;
                        int BlockToMoveX = 0;
                        for (int loop = 1; loop <= 3; loop++)
                        {
                            if (loop == 1)
                            {
                                BlockToMoveY = Addedblock_1.current_y;
                                BlockToMoveX = Addedblock_1.current_x;
                            }
                            else if (loop == 2)
                            {
                                BlockToMoveY = Addedblock_2.current_y;
                                BlockToMoveX = Addedblock_2.current_x;
                            }
                            else if (loop == 3)
                            {
                                BlockToMoveY = Addedblock_3.current_y;
                                BlockToMoveX = Addedblock_3.current_x;
                            }
                            if (BlockToMoveY == Fokusblock.current_y - 1 && BlockToMoveX == Fokusblock.current_x)
                            {
                                BlockToMoveY = Fokusblock.current_y;
                                BlockToMoveX = Fokusblock.current_x + 1;
                            }
                            else if (BlockToMoveY == Fokusblock.current_y && BlockToMoveX == Fokusblock.current_x + 1)
                            {
                                BlockToMoveY = Fokusblock.current_y + 1;
                                BlockToMoveX = Fokusblock.current_x;
                            }
                            else if (BlockToMoveY == Fokusblock.current_y + 1 && BlockToMoveX == Fokusblock.current_x)
                            {
                                BlockToMoveY = Fokusblock.current_y;
                                BlockToMoveX = Fokusblock.current_x - 1;
                            }
                            else if (BlockToMoveY == Fokusblock.current_y && BlockToMoveX == Fokusblock.current_x - 1)
                            {
                                BlockToMoveY = Fokusblock.current_y - 1;
                                BlockToMoveX = Fokusblock.current_x;
                            }
                            else if (BlockToMoveY == Fokusblock.current_y - 1 && BlockToMoveX == Fokusblock.current_x + 1)
                            {
                                BlockToMoveY = Fokusblock.current_y + 1;
                                BlockToMoveX = Fokusblock.current_x + 1;
                            }
                            else if (BlockToMoveY == Fokusblock.current_y + 1 && BlockToMoveX == Fokusblock.current_x + 1)
                            {
                                BlockToMoveY = Fokusblock.current_y + 1;
                                BlockToMoveX = Fokusblock.current_x - 1;
                            }
                            else if (BlockToMoveY == Fokusblock.current_y + 1 && BlockToMoveX == Fokusblock.current_x - 1)
                            {
                                BlockToMoveY = Fokusblock.current_y - 1;
                                BlockToMoveX = Fokusblock.current_x - 1;
                            }
                            else if (BlockToMoveY == Fokusblock.current_y - 1 && BlockToMoveX == Fokusblock.current_x - 1)
                            {
                                BlockToMoveY = Fokusblock.current_y - 1;
                                BlockToMoveX = Fokusblock.current_x + 1;
                            }
                            if (loop == 1)
                            {
                                Addedblock_1.current_y = BlockToMoveY;
                                Addedblock_1.current_x = BlockToMoveX;
                            }
                            else if (loop == 2)
                            {
                                Addedblock_2.current_y = BlockToMoveY;
                                Addedblock_2.current_x = BlockToMoveX;
                            }
                            else if (loop == 3)
                            {
                                Addedblock_3.current_y = BlockToMoveY;
                                Addedblock_3.current_x = BlockToMoveX;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
            while (Fokusblock.current_x < 0 || Addedblock_1.current_x < 0 || Addedblock_2.current_x < 0 || Addedblock_3.current_x < 0)
            {
                Fokusblock.current_x = Fokusblock.current_x + 1;
                Addedblock_1.current_x = Addedblock_1.current_x + 1;
                Addedblock_2.current_x = Addedblock_2.current_x + 1;
                Addedblock_3.current_x = Addedblock_3.current_x + 1;
            }
            while (Fokusblock.current_x >= size_x || Addedblock_1.current_x >= size_x || Addedblock_2.current_x >= size_x || Addedblock_3.current_x >= size_x)
            {
                Fokusblock.current_x = Fokusblock.current_x - 1;
                Addedblock_1.current_x = Addedblock_1.current_x - 1;
                Addedblock_2.current_x = Addedblock_2.current_x - 1;
                Addedblock_3.current_x = Addedblock_3.current_x - 1;
            }
            while (Fokusblock.current_y >= size_y || Addedblock_1.current_y >= size_y || Addedblock_2.current_y >= size_y || Addedblock_3.current_y >= size_y)
            {
                Fokusblock.current_y = Fokusblock.current_y - 1;
                Addedblock_1.current_y = Addedblock_1.current_y - 1;
                Addedblock_2.current_y = Addedblock_2.current_y - 1;
                Addedblock_3.current_y = Addedblock_3.current_y - 1;
            }
            if (Spielfeld.kasten[Fokusblock.current_y, Fokusblock.current_x].Kvalue != 0
                || Spielfeld.kasten[Addedblock_1.current_y, Addedblock_1.current_x].Kvalue != 0
                || Spielfeld.kasten[Addedblock_2.current_y, Addedblock_2.current_x].Kvalue != 0
                || Spielfeld.kasten[Addedblock_3.current_y, Addedblock_3.current_x].Kvalue != 0)
            {
                blockstate = blockstate - 1;
                Fokusblock.current_y = y_0;
                Fokusblock.current_x = x_0;
                Addedblock_1.current_y = y_1;
                Addedblock_1.current_x = x_1;
                Addedblock_2.current_y = y_2;
                Addedblock_2.current_x = x_2;
                Addedblock_3.current_y = y_3;
                Addedblock_3.current_x = x_3;
                Spielfeld.kasten[Fokusblock.current_y, Fokusblock.current_x].Kvalue = RndKv;
                Spielfeld.kasten[Addedblock_1.current_y, Addedblock_1.current_x].Kvalue = RndKv;
                Spielfeld.kasten[Addedblock_2.current_y, Addedblock_2.current_x].Kvalue = RndKv;
                Spielfeld.kasten[Addedblock_3.current_y, Addedblock_3.current_x].Kvalue = RndKv;
            }
            else
            {
                Spielfeld.kasten[Fokusblock.current_y, Fokusblock.current_x].Kvalue = RndKv;
                Spielfeld.kasten[Addedblock_1.current_y, Addedblock_1.current_x].Kvalue = RndKv;
                Spielfeld.kasten[Addedblock_2.current_y, Addedblock_2.current_x].Kvalue = RndKv;
                Spielfeld.kasten[Addedblock_3.current_y, Addedblock_3.current_x].Kvalue = RndKv;
            }
            coloring_update();
        }
        public void Block_storing()
        {
            if (Fokusblock == null || Spielfeld == null
                || Addedblock_1 == null || Addedblock_2 == null
                || Addedblock_3 == null || KastenNextBlock == null
                || KastenStoreBlock == null)
                return;
            if (Storedblock == "Empty")
            {
                Storedblock = Nextblock;
                Blcoktype_choose();
            }
            else
            {
                shortstore_1 = Storedblock;
                shortstore_2 = Nextblock;
                Storedblock = shortstore_2;
                Nextblock = shortstore_1;
            }
            int y = 0;
            int x = 0;
            int i = 0;
            int j = 0;
            for (; y < storeblock_y;)
            {
                KastenStoreBlock.kasten[y, x].Kvalue = 0;
                x++;
                if (x >= storeblock_x)
                {
                    x = 0;
                    y++;
                }
            }
            for (; i < nextblock_y;)
            {
                KastenNextBlock.kasten[i, j].Kvalue = 0;
                j++;
                if (j >= nextblock_x)
                {
                    j = 0;
                    i++;
                }
            }
            switch (Storedblock)
            {
                case "bt_1":
                    KastenStoreBlock.kasten[0, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[3, 1].Kvalue = RndKv;
                    // bt_1 // 4 in Reihe
                    break;
                case "bt_2":
                    KastenStoreBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[1, 2].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[3, 1].Kvalue = RndKv;
                    // bt_2 // 011/010/010
                    break;
                case "bt_3":
                    KastenStoreBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[3, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[3, 2].Kvalue = RndKv;
                    // bt_3 // 010/010/011
                    break;
                case "bt_4":
                    KastenStoreBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[1, 2].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 2].Kvalue = RndKv;
                    // bt_4 // Quadrat
                    break;
                case "bt_5":
                    KastenStoreBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 2].Kvalue = RndKv;
                    KastenStoreBlock.kasten[3, 2].Kvalue = RndKv;
                    // bt_5 // 010/011/001
                    break;
                case "bt_6":
                    KastenStoreBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 2].Kvalue = RndKv;
                    KastenStoreBlock.kasten[3, 1].Kvalue = RndKv;
                    // bt_6 // 010/011/010
                    break;
                case "bt_7":
                    KastenStoreBlock.kasten[1, 2].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenStoreBlock.kasten[2, 2].Kvalue = RndKv;
                    KastenStoreBlock.kasten[3, 1].Kvalue = RndKv;
                    // bt_7 // 001/011/010
                    break;
                default:
                    // bt_1
                    break;
            }
            switch (Nextblock)
            {
                case "bt_1":
                    KastenNextBlock.kasten[0, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[3, 1].Kvalue = RndKv;
                    // bt_1 // 4 in Reihe
                    break;
                case "bt_2":
                    KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[1, 2].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[3, 1].Kvalue = RndKv;
                    // bt_2 // 011/010/010
                    break;
                case "bt_3":
                    KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[3, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[3, 2].Kvalue = RndKv;
                    // bt_3 // 010/010/011
                    break;
                case "bt_4":
                    KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[1, 2].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 2].Kvalue = RndKv;
                    // bt_4 // Quadrat
                    break;
                case "bt_5":
                    KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 2].Kvalue = RndKv;
                    KastenNextBlock.kasten[3, 2].Kvalue = RndKv;
                    // bt_5 // 010/011/001
                    break;
                case "bt_6":
                    KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 2].Kvalue = RndKv;
                    KastenNextBlock.kasten[3, 1].Kvalue = RndKv;
                    // bt_6 // 010/011/010
                    break;
                case "bt_7":
                    KastenNextBlock.kasten[1, 2].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                    KastenNextBlock.kasten[2, 2].Kvalue = RndKv;
                    KastenNextBlock.kasten[3, 1].Kvalue = RndKv;
                    // bt_7 // 001/011/010
                    break;
                default:
                    break;
            } 
            coloring_update();
        }
        public class Kasten
        {
            public int Kvalue { get; set; } = 0;
        }
        public class KastenFactory
        {
            public Kasten[,] kasten { get; set; }

            public KastenFactory(int fx, int fy)
            {
                kasten = new Kasten[fx, fy];
                for (int i = 0; i < fx; i++)
                {
                    for (int j = 0; j < fy; j++)
                    {
                        kasten[i, j] = new Kasten();
                        // Console.WriteLine($"X: {i} Y: {j} k_value: {this.kasten[i, j].Kvalue}");
                        // Console.Write($"{this.kasten[i, j].Kvalue}");
                    }
                    // Console.WriteLine();
                }
            }
        }
        public class Block_Core
        {
            public int current_x { get; set; }
            public int current_y { get; set; }

            public Block_Core(Kasten[,] k_array)
            {
                current_y = k_array.GetLength(0);
                current_x = k_array.GetLength(1);
            }
        }
        public void TetrisTimer()
        {
            if (tempo_x != 0)
            {
                if (current_level % speed_level == 0)
                {
                    tempo_x = tempo_x - speed_increase;
                    if (tempo_x < 100)
                    {
                        tempo_x = 100;
                    }
                }
                if (GameTimer != null)
                {
                    GameTimer.Dispose();
                    GameTimer = null;
                }
                GameTimer = new System.Threading.Timer(Timer_based_Move, null, tempo_x, tempo_x); // 1000 milliseconds = 1 seconds
                                                                                                  // Console.WriteLine("Timer gestartet");
                                                                                                  // Console.ReadLine(); // Keep the program running
                
            }
        }
        public void Timer_based_Move(object? state)
        {
            if (Fokusblock == null || Spielfeld == null
                || Addedblock_1 == null || Addedblock_2 == null
                || Addedblock_3 == null)
                return;
            int y_0 = Fokusblock.current_y;
            int x_0 = Fokusblock.current_x;
            int y_1 = Addedblock_1.current_y;
            int x_1 = Addedblock_1.current_x;
            int y_2 = Addedblock_2.current_y;
            int x_2 = Addedblock_2.current_x;
            int y_3 = Addedblock_3.current_y;
            int x_3 = Addedblock_3.current_x;
            Spielfeld.kasten[y_0, x_0].Kvalue = 0;
            Spielfeld.kasten[y_1, x_1].Kvalue = 0;
            Spielfeld.kasten[y_2, x_2].Kvalue = 0;
            Spielfeld.kasten[y_3, x_3].Kvalue = 0;
            {
                if (y_0 + 1 < size_y && Spielfeld.kasten[y_0 + 1, x_0].Kvalue != 0
                    || y_1 + 1 < size_y && Spielfeld.kasten[y_1 + 1, x_1].Kvalue != 0
                    || y_2 + 1 < size_y && Spielfeld.kasten[y_2 + 1, x_2].Kvalue != 0
                    || y_3 + 1 < size_y && Spielfeld.kasten[y_3 + 1, x_3].Kvalue != 0)
                {
                    Block_Collision_Detected();
                }
                else if (y_0 + 1 == size_y
                         || y_1 + 1 == size_y
                         || y_2 + 1 == size_y
                         || y_3 + 1 == size_y)
                {
                    Fokusblock.current_y++;
                    Addedblock_1.current_y++;
                    Addedblock_2.current_y++;
                    Addedblock_3.current_y++;
                    Block_Collision_Detected();
                }
                else
                {
                    Fokusblock.current_y++;
                    Addedblock_1.current_y++;
                    Addedblock_2.current_y++;
                    Addedblock_3.current_y++;
                }
            }
            if (Fokusblock == null || Spielfeld == null
                || Addedblock_1 == null || Addedblock_2 == null
                || Addedblock_3 == null)
                return;
            Spielfeld.kasten[Fokusblock.current_y, Fokusblock.current_x].Kvalue = RndKv;
            Spielfeld.kasten[Addedblock_1.current_y, Addedblock_1.current_x].Kvalue = RndKv;
            Spielfeld.kasten[Addedblock_2.current_y, Addedblock_2.current_x].Kvalue = RndKv;
            Spielfeld.kasten[Addedblock_3.current_y, Addedblock_3.current_x].Kvalue = RndKv;
            coloring_update();
        }
        public void Player_based_Move()
        {
            if (Fokusblock == null || Spielfeld == null 
                || Addedblock_1 == null || Addedblock_2 == null
                || Addedblock_3 == null)
                return;
            int y_0 = Fokusblock.current_y;
            int x_0 = Fokusblock.current_x;
            int y_1 = Addedblock_1.current_y;
            int x_1 = Addedblock_1.current_x;
            int y_2 = Addedblock_2.current_y;
            int x_2 = Addedblock_2.current_x;
            int y_3 = Addedblock_3.current_y;
            int x_3 = Addedblock_3.current_x;
            Spielfeld.kasten[y_0, x_0].Kvalue = 0;
            Spielfeld.kasten[y_1, x_1].Kvalue = 0;
            Spielfeld.kasten[y_2, x_2].Kvalue = 0;
            Spielfeld.kasten[y_3, x_3].Kvalue = 0;
            switch (Moveblock)
            {
                case "Down":
                    {
                        if (y_0 + 1 < size_y && Spielfeld.kasten[y_0 + 1, x_0].Kvalue != 0
                            || y_1 + 1 < size_y && Spielfeld.kasten[y_1 + 1, x_1].Kvalue != 0
                            || y_2 + 1 < size_y && Spielfeld.kasten[y_2 + 1, x_2].Kvalue != 0
                            || y_3 + 1 < size_y && Spielfeld.kasten[y_3 + 1, x_3].Kvalue != 0)
                        {
                            Block_Collision_Detected();
                        }
                        else if (y_0 + 1 == size_y
                                 || y_1 + 1 == size_y
                                 || y_2 + 1 == size_y
                                 || y_3 + 1 == size_y)
                        {
                            Fokusblock.current_y++;
                            Addedblock_1.current_y++;
                            Addedblock_2.current_y++;
                            Addedblock_3.current_y++;
                            Block_Collision_Detected();
                        }
                        else
                        {
                            Fokusblock.current_y++;
                            Addedblock_1.current_y++;
                            Addedblock_2.current_y++;
                            Addedblock_3.current_y++;
                        }
                    }
                    break;
                case "Left":
                    {
                        if (x_0 - 1 >= 0
                            && x_1 - 1 >= 0
                            && x_2 - 1 >= 0
                            && x_3 - 1 >= 0)
                        {
                            if (Spielfeld.kasten[y_0, x_0 - 1].Kvalue == 0
                                && Spielfeld.kasten[y_1, x_1 - 1].Kvalue == 0
                                && Spielfeld.kasten[y_2, x_2 - 1].Kvalue == 0
                                && Spielfeld.kasten[y_3, x_3 - 1].Kvalue == 0)
                            {
                                Fokusblock.current_x--;
                                Addedblock_1.current_x--;
                                Addedblock_2.current_x--;
                                Addedblock_3.current_x--;
                            }
                        }
                    }
                    break;
                case "Right":
                    {
                        if (x_0 + 1 < size_x
                            && x_1 + 1 < size_x
                            && x_2 + 1 < size_x
                            && x_3 + 1 < size_x)
                        {
                            if (Spielfeld.kasten[y_0, x_0 + 1].Kvalue == 0
                                && Spielfeld.kasten[y_1, x_1 + 1].Kvalue == 0
                                && Spielfeld.kasten[y_2, x_2 + 1].Kvalue == 0
                                && Spielfeld.kasten[y_3, x_3 + 1].Kvalue == 0)
                            {
                                Fokusblock.current_x++;
                                Addedblock_1.current_x++;
                                Addedblock_2.current_x++;
                                Addedblock_3.current_x++;
                            }
                        }
                    }
                    break;
            }
            if (Fokusblock == null || Spielfeld == null
                || Addedblock_1 == null || Addedblock_2 == null
                || Addedblock_3 == null)
                return;
            Spielfeld.kasten[Fokusblock.current_y, Fokusblock.current_x].Kvalue = RndKv;
            Spielfeld.kasten[Addedblock_1.current_y, Addedblock_1.current_x].Kvalue = RndKv;
            Spielfeld.kasten[Addedblock_2.current_y, Addedblock_2.current_x].Kvalue = RndKv;
            Spielfeld.kasten[Addedblock_3.current_y, Addedblock_3.current_x].Kvalue = RndKv;
            coloring_update();
        }
        public void coloring_update()
        {
            if (Spielfeld != null && KastenNextBlock != null && KastenStoreBlock != null)
            {
                int counter = 0;
                foreach (Box b in boxes)
                {
                    b.Value = Spielfeld.kasten[b.Y / boxSize, b.X / boxSize].Kvalue;
                    if (counter < 40 && !GO)
                    {
                        counter = counter + 1;
                        b.Value = Spielfeld.kasten[b.Y / boxSize, b.X / boxSize].Kvalue = 0;
                    }
                }
                foreach (Box b in boxesNB)
                {
                    b.Value = KastenNextBlock.kasten[b.Y / boxnextstore, b.X / boxnextstore].Kvalue;
                }
                foreach (Box b in boxesSB)
                {
                    b.Value = KastenStoreBlock.kasten[b.Y / boxnextstore, b.X / boxnextstore].Kvalue;
                }
                flowLayoutPanel1.Invalidate();
                flow_Next.Invalidate();
                flow_store.Invalidate();
            }
        }
        public void Block_Collision_Detected()
        {
            if (Fokusblock != null && Spielfeld != null
                && Addedblock_1 != null && Addedblock_2 != null
                && Addedblock_3 != null)
            {
                bottom = true;
                if (Fokusblock.current_y >= size_y
                    || Addedblock_1.current_y >= size_y
                    || Addedblock_2.current_y >= size_y
                    || Addedblock_3.current_y >= size_y)
                {
                    Spielfeld.kasten[Fokusblock.current_y - 1, Fokusblock.current_x].Kvalue = RndKv;
                    Spielfeld.kasten[Addedblock_1.current_y - 1, Addedblock_1.current_x].Kvalue = RndKv;
                    Spielfeld.kasten[Addedblock_2.current_y - 1, Addedblock_2.current_x].Kvalue = RndKv;
                    Spielfeld.kasten[Addedblock_3.current_y - 1, Addedblock_3.current_x].Kvalue = RndKv;
                }
                else
                {
                    Spielfeld.kasten[Fokusblock.current_y, Fokusblock.current_x].Kvalue = RndKv;
                    Spielfeld.kasten[Addedblock_1.current_y, Addedblock_1.current_x].Kvalue = RndKv;
                    Spielfeld.kasten[Addedblock_2.current_y, Addedblock_2.current_x].Kvalue = RndKv;
                    Spielfeld.kasten[Addedblock_3.current_y, Addedblock_3.current_x].Kvalue = RndKv;
                }
                if (Fokusblock.current_y - 1 < 0
                    || Addedblock_1.current_y - 1 < 0
                    || Addedblock_2.current_y - 1 < 0
                    || Addedblock_3.current_y - 1 < 0)
                {
                    Spielfeld.kasten[Fokusblock.current_y + 1, Fokusblock.current_x].Kvalue = RndKv;
                    Spielfeld.kasten[Addedblock_1.current_y + 1, Addedblock_1.current_x].Kvalue = RndKv;
                    Spielfeld.kasten[Addedblock_2.current_y + 1, Addedblock_2.current_x].Kvalue = RndKv;
                    Spielfeld.kasten[Addedblock_3.current_y + 1, Addedblock_3.current_x].Kvalue = RndKv;
                }
                for (int i = size_y - 1; i > 0; i--)
                {
                    for (int j = 4; j > 0; j--)
                    {
                        bool DEL = true;
                        for (int b = 0; b < size_x && DEL; b++)
                        {
                            if (Spielfeld.kasten[i, b].Kvalue == 0)
                            {
                                DEL = false;
                            }
                        }
                        if (DEL)
                        {
                            current_level = current_level + 1;
                            score = (score + (current_level * 2) + 100) - (tempo_x / 10);
                            Current_Score.Invoke((System.Windows.Forms.MethodInvoker)delegate { Current_Score.Text = $"{score}"; });
                            Level_Display.Invoke((System.Windows.Forms.MethodInvoker)delegate { Level_Display.Text = $"Level: {current_level}"; });
                            for (int y = i; y > 0; y--)
                            {
                                for (int x = 0; x < size_x; x++)
                                {
                                    Spielfeld.kasten[y, x].Kvalue = Spielfeld.kasten[y - 1, x].Kvalue;
                                }
                            }
                        }
                    }
                }
            }
            Insert_Nextblock();
        }
        public void Insert_Nextblock()
        {
            if (Spielfeld != null && Nextblock != null && KastenNextBlock != null)
            {
                int r = 0;
                int t = 0;
                for (r = 0; r < 4; r++)
                {
                    for (t = 0; t < size_x; t++)
                    {
                        if (Spielfeld.kasten[r, t].Kvalue != 0)
                        {
                            GameOver();
                            return;
                        }
                    }
                }
                blockstate = 1;
                int X_chosen = RndGenerator.Next(0, size_x - 3);
                RndKv = RndGenerator.Next(1, 21);
                int i = 0;
                int j = 0;
                for (; i < nextblock_y;)
                {
                    KastenNextBlock.kasten[i, j].Kvalue = 0;
                    j++;
                    if (j >= nextblock_x)
                    {
                        j = 0;
                        i++;
                    }
                }
                switch (Nextblock)
                    {
                    case "bt_1":
                        Activeblock = "bt_1";
                        Kasten[,] bt_1_Startposition_0 = new KastenFactory(1, X_chosen).kasten;
                        Kasten[,] bt_1_Startposition_1 = new KastenFactory(0, X_chosen).kasten;
                        Kasten[,] bt_1_Startposition_2 = new KastenFactory(2, X_chosen).kasten;
                        Kasten[,] bt_1_Startposition_3 = new KastenFactory(3, X_chosen).kasten;
                        Fokusblock = new Block_Core(bt_1_Startposition_0);
                        Addedblock_1 = new Block_Core(bt_1_Startposition_1);
                        Addedblock_2 = new Block_Core(bt_1_Startposition_2);
                        Addedblock_3 = new Block_Core(bt_1_Startposition_3);
                        // bt_1 // 4 in Reihe
                        break;
                    case "bt_2":
                        Activeblock = "bt_2";
                        Kasten[,] bt_2_Startposition_0 = new KastenFactory(1, X_chosen).kasten;
                        Kasten[,] bt_2_Startposition_1 = new KastenFactory(0, X_chosen).kasten;
                        Kasten[,] bt_2_Startposition_2 = new KastenFactory(0, X_chosen + 1).kasten;
                        Kasten[,] bt_2_Startposition_3 = new KastenFactory(2, X_chosen).kasten;
                        Fokusblock = new Block_Core(bt_2_Startposition_0);
                        Addedblock_1 = new Block_Core(bt_2_Startposition_1);
                        Addedblock_2 = new Block_Core(bt_2_Startposition_2);
                        Addedblock_3 = new Block_Core(bt_2_Startposition_3);
                        // bt_2 // 011/010/010
                        break;
                    case "bt_3":
                        Activeblock = "bt_3";
                        Kasten[,] bt_3_Startposition_0 = new KastenFactory(1, X_chosen).kasten;
                        Kasten[,] bt_3_Startposition_1 = new KastenFactory(0, X_chosen).kasten;
                        Kasten[,] bt_3_Startposition_2 = new KastenFactory(2, X_chosen).kasten;
                        Kasten[,] bt_3_Startposition_3 = new KastenFactory(2, X_chosen + 1).kasten;
                        Fokusblock = new Block_Core(bt_3_Startposition_0);
                        Addedblock_1 = new Block_Core(bt_3_Startposition_1);
                        Addedblock_2 = new Block_Core(bt_3_Startposition_2);
                        Addedblock_3 = new Block_Core(bt_3_Startposition_3);
                        // bt_3 // 010/010/011
                        break;
                    case "bt_4":
                        Activeblock = "bt_4";
                        Kasten[,] bt_4_Startposition_0 = new KastenFactory(1, X_chosen).kasten;
                        Kasten[,] bt_4_Startposition_1 = new KastenFactory(0, X_chosen).kasten;
                        Kasten[,] bt_4_Startposition_2 = new KastenFactory(1, X_chosen + 1).kasten;
                        Kasten[,] bt_4_Startposition_3 = new KastenFactory(0, X_chosen + 1).kasten;
                        Fokusblock = new Block_Core(bt_4_Startposition_0);
                        Addedblock_1 = new Block_Core(bt_4_Startposition_1);
                        Addedblock_2 = new Block_Core(bt_4_Startposition_2);
                        Addedblock_3 = new Block_Core(bt_4_Startposition_3);
                        // bt_4 // Quadrat
                        break;
                    case "bt_5":
                        Activeblock = "bt_5";
                        Kasten[,] bt_5_Startposition_0 = new KastenFactory(1, X_chosen).kasten;
                        Kasten[,] bt_5_Startposition_1 = new KastenFactory(0, X_chosen).kasten;
                        Kasten[,] bt_5_Startposition_2 = new KastenFactory(1, X_chosen + 1).kasten;
                        Kasten[,] bt_5_Startposition_3 = new KastenFactory(2, X_chosen + 1).kasten;
                        Fokusblock = new Block_Core(bt_5_Startposition_0);
                        Addedblock_1 = new Block_Core(bt_5_Startposition_1);
                        Addedblock_2 = new Block_Core(bt_5_Startposition_2);
                        Addedblock_3 = new Block_Core(bt_5_Startposition_3);
                        // bt_5 // 010/011/001
                        break;
                    case "bt_6":
                        Activeblock = "bt_6";
                        Kasten[,] bt_6_Startposition_0 = new KastenFactory(1, X_chosen).kasten;
                        Kasten[,] bt_6_Startposition_1 = new KastenFactory(0, X_chosen).kasten;
                        Kasten[,] bt_6_Startposition_2 = new KastenFactory(1, X_chosen + 1).kasten;
                        Kasten[,] bt_6_Startposition_3 = new KastenFactory(2, X_chosen).kasten;
                        Fokusblock = new Block_Core(bt_6_Startposition_0);
                        Addedblock_1 = new Block_Core(bt_6_Startposition_1);
                        Addedblock_2 = new Block_Core(bt_6_Startposition_2);
                        Addedblock_3 = new Block_Core(bt_6_Startposition_3);
                        // bt_6 // 010/011/010
                        break;
                    case "bt_7":
                        Activeblock = "bt_7";
                        Kasten[,] bt_7_Startposition_0 = new KastenFactory(1, X_chosen).kasten;
                        Kasten[,] bt_7_Startposition_1 = new KastenFactory(1, X_chosen + 1).kasten;
                        Kasten[,] bt_7_Startposition_2 = new KastenFactory(0, X_chosen + 1).kasten;
                        Kasten[,] bt_7_Startposition_3 = new KastenFactory(2, X_chosen).kasten;
                        Fokusblock = new Block_Core(bt_7_Startposition_0);
                        Addedblock_1 = new Block_Core(bt_7_Startposition_1);
                        Addedblock_2 = new Block_Core(bt_7_Startposition_2);
                        Addedblock_3 = new Block_Core(bt_7_Startposition_3);
                        // bt_7 // 001/011/010
                        break;
                }
                Blcoktype_choose();
                switch (Nextblock)
                {
                    case "bt_1":
                        KastenNextBlock.kasten[0, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[3, 1].Kvalue = RndKv;
                        // bt_1 // 4 in Reihe
                        break;
                    case "bt_2":
                        KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[1, 2].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[3, 1].Kvalue = RndKv;
                        // bt_2 // 011/010/010
                        break;
                    case "bt_3":
                        KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[3, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[3, 2].Kvalue = RndKv;
                        // bt_3 // 010/010/011
                        break;
                    case "bt_4":
                        KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[1, 2].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 2].Kvalue = RndKv;
                        // bt_4 // Quadrat
                        break;
                    case "bt_5":
                        KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 2].Kvalue = RndKv;
                        KastenNextBlock.kasten[3, 2].Kvalue = RndKv;
                        // bt_5 // 010/011/001
                        break;
                    case "bt_6":
                        KastenNextBlock.kasten[1, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 2].Kvalue = RndKv;
                        KastenNextBlock.kasten[3, 1].Kvalue = RndKv;
                        // bt_6 // 010/011/010
                        break;
                    case "bt_7":
                        KastenNextBlock.kasten[1, 2].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 1].Kvalue = RndKv;
                        KastenNextBlock.kasten[2, 2].Kvalue = RndKv;
                        KastenNextBlock.kasten[3, 1].Kvalue = RndKv;
                        // bt_7 // 001/011/010
                        break;
                }
                coloring_update();
            }
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // muss existieren
        }
        public void GameOver()
        {
            if (GameTimer != null && Spielfeld != null
                && Fokusblock != null && Addedblock_1 != null
                && Addedblock_2 != null && Addedblock_3 != null)
            {
                int x = 0;
                int y = 0;
                GO = true;
                for (; y < size_y;)
                {
                    Spielfeld.kasten[y, x].Kvalue = 4;
                    x++;
                    if (x >= size_x)
                    {
                        x = 0;
                        y++;
                    }
                }
                foreach (Box b in boxes)
                {
                    b.Value = Spielfeld.kasten[b.Y / boxSize, b.X / boxSize].Kvalue;
                }
                Spielfeld.kasten[Fokusblock.current_y, Fokusblock.current_x].Kvalue = 4;
                Spielfeld.kasten[Addedblock_1.current_y, Addedblock_1.current_x].Kvalue = 4;
                Spielfeld.kasten[Addedblock_2.current_y, Addedblock_2.current_x].Kvalue = 4;
                Spielfeld.kasten[Addedblock_3.current_y, Addedblock_3.current_x].Kvalue = 4;
                Fokusblock = null;
                Addedblock_1 = null;
                Addedblock_2 = null;
                Addedblock_3 = null;

                if (GameTimer != null)
                {
                    GameTimer.Dispose();
                    GameTimer = null;
                }
            }
            Speed_Start.Enabled = true;
            SpeedIncreaseComboBox.Enabled = true;
            Score_Board.Enabled = true;
            Highscore_newrecord();
            coloring_update();
        }
    }
}
