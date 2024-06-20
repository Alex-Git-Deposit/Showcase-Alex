using System;
using System.Numerics;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections.Generic;
using System.Drawing;
// using static Snake.Main_Window;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using static System.Windows.Forms.DataFormats;
using System.Runtime.InteropServices;
using System.Media;
using WMPLib;
using System.Text;
namespace Snake
{
    public partial class Main_Window : Form
    {
        /*public static string current_sound { get; set; }*/
        string enteredText = " ";
        string HS_Name = "";
        int HS_Value = 0;
        int HS_T_Name = 6;
        int HS_S_Name = 2;
        int HS_T_Value = 7;
        int HS_S_Value = 2;
        int size_x = 20;
        int size_y = 20;
        int tempo_x = 500;
        int maxlivetime = 4;
        string StoreStep = "Right";
        string Directorystring = "C:/Meins/Coding/C#/playground/Snake/SnakeHighscore.xlsx";
        /*
        public static string sound_01 = "C:/Meins/Coding/C#/playground/Snake/Music_01.mp3";
        public static string sound_02 = "C:/Meins/Coding/C#/playground/Snake/Music_02.mp3";
        public static string sound_03 = "C:/Meins/Coding/C#/playground/Snake/Music_03.mp3";
        string sound_game_over = "C:/Meins/Coding/C#/playground/Snake/Music_Game_Over.mp3";
        string sound_getting_apple = "C:/Meins/Coding/C#/playground/Snake/Music_Getting_Apple.mp3";
        string sound_opening_theme = "C:/Meins/Coding/C#/playground/Snake/Music_Opening_Theme.mp3";
        string sound_start_button = "C:/Meins/Coding/C#/playground/Snake/Music_Start_Button.mp3";
        public bool isLooping { get; set; } = false;
        */

        /* Musik komplett auskommentiert für GitHub (damit jemand der es testet nicht direkt von Musik überwältig wird. 
         * Funktioniert bisher gut, braucht noch feinarbeit  */


        public KastenFactory? KastenArray;
        public System.Threading.Timer? GameTimer = null;
        int score = 0;
        private List<Box> boxes;
        private int boxSize = 25;
        static List<WindowsMediaPlayer> allPlayers = new List<WindowsMediaPlayer>();

        public Main_Window()
        {
            boxes = new List<Box>();
            InitializeComponent();
            /*
            PlayMP3(sound_opening_theme);
            Sound_Window soundWindow = new Sound_Window(this);
            current_sound = sound_01;
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // muss existieren
        }

        private void fontDialog1_Apply(object sender, EventArgs e)
        {
            // muss existieren
        }
        public class Box
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public int Value { get; set; }

            public Box(int x, int y, int width, int height, int value)
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
                        g.FillRectangle(new SolidBrush(Color.White), rect);
                        break;
                    case 4:
                        g.FillRectangle(new SolidBrush(Color.Red), rect);
                        break;
                    case 5:
                        g.FillRectangle(new SolidBrush(Color.Blue), rect);
                        // Game Over: Blue Screen of Death
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

        private void Feld_comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? selectedOption = "";

            if (FieldSizeComboBox.SelectedItem != null)
                selectedOption = FieldSizeComboBox.SelectedItem.ToString();

            switch (selectedOption)
            {
                case "20x20":
                    size_x = 20;
                    size_y = 20;
                    boxSize = 25;
                    HS_S_Name = 2;
                    HS_S_Value = 2;
                    break;
                case "40x40":
                    size_x = 40;
                    size_y = 40;
                    boxSize = 12;
                    HS_S_Name = 3;
                    HS_S_Value = 3;
                    break;
                case "60x60":
                    size_x = 60;
                    size_y = 60;
                    boxSize = 8;
                    HS_S_Name = 4;
                    HS_S_Value = 4;
                    break;
                case "80x80":
                    size_x = 80;
                    size_y = 80;
                    boxSize = 6;
                    HS_S_Name = 5;
                    HS_S_Value = 5;
                    break;
                case "100x100":
                    size_x = 100;
                    size_y = 100;
                    boxSize = 5;
                    HS_S_Name = 6;
                    HS_S_Value = 6;
                    break;
                default:
                    size_x = 20;
                    size_y = 20;
                    boxSize = 25;
                    HS_S_Name = 2;
                    HS_S_Value = 2;
                    break;
            }
            flowLayoutPanel1.Size = new Size(size_x * boxSize, size_y * boxSize);
        }
        private void Tempo_comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? selectedOption = "";
            if (Speed.SelectedItem != null)
                selectedOption = Speed.SelectedItem.ToString();

            switch (selectedOption)
            {
                case "Tempo x 0":
                    tempo_x = 0;
                    HS_T_Name = 2;
                    HS_T_Value = 3;
                    break;
                case "Tempo x 1":
                    tempo_x = 1000;
                    HS_T_Name = 4;
                    HS_T_Value = 5;
                    break;
                case "Tempo x 2":
                    tempo_x = 500;
                    HS_T_Name = 6;
                    HS_T_Value = 7;
                    break;
                case "Tempo x 4":
                    tempo_x = 250;
                    HS_T_Name = 8;
                    HS_T_Value = 9;
                    break;
                case "Tempo x 8":
                    tempo_x = 125;
                    HS_T_Name = 10;
                    HS_T_Value = 11;
                    break;
                case "Tempo x 10":
                    tempo_x = 100;
                    HS_T_Name = 12;
                    HS_T_Value = 13;
                    break;
                default:
                    tempo_x = 500;
                    HS_T_Name = 6;
                    HS_T_Value = 7;
                    break;
            }
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
                    Console.WriteLine("Pause aktiv");
                    break;
                case "Continue":
                    Pause_Button.Text = "Pause";
                    Current_Score.Focus();
                    NextStepTimer();
                    Console.WriteLine("Pause endet");
                    break;
            }
        }

        private void End_button_Click(object sender, EventArgs e)
        {
            this.Close(); // Beenden
        }
        /*
        private async void Start_button_Click(object sender, EventArgs e)
        {
            await StartGame(current_sound);
        }
        private async Task StartGame(string currentSound)
        {
        */
        private void Start_button_Click(object sender, EventArgs e)
        { 
            // Start!

            /*
            StopAllMP3();
            PlayMP3(sound_start_button);
            await Task.Delay(2000);
            await Task.Delay(2000).ContinueWith(_ => PlayMP3Loop(current_sound));
            */
            KastenArray = new KastenFactory(size_x, size_y);
            score = 0;
            Current_Score.Text = $"Score:  {score}";

            FieldSizeComboBox.Enabled = false;
            Speed.Enabled = false;
            Score_Board.Enabled = false;

            string pathToExcelFile = Directorystring;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook SnakeHighscore = xlApp.Workbooks.Open(pathToExcelFile);
            Excel._Worksheet xlWorksheet = SnakeHighscore.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            HS_Name = "";
            HS_Value = 0;

            //      für Excel              Nummer, Buchstabe (als Nummer)
            //                              Tempo, Size
            HS_Name = xlWorksheet.Cells[HS_T_Name, HS_S_Name].Value;
            HS_Value = int.Parse(xlWorksheet.Cells[HS_T_Value, HS_S_Value].Value.ToString());

            Highscore_Display.Text = $"Highscore: {HS_Name} -> {HS_Value}";

            SnakeHighscore.Close();
            xlApp.Quit();

            enteredText = "";
            StoreStep = "Right";
            // y  x
            maxlivetime = 4;
            KastenArray.kasten[5, 5].Kvalue = 1;
            KastenArray.kasten[5, 4].Kvalue = 2;
            KastenArray.kasten[5, 4].livetime = 4;
            KastenArray.kasten[5, 3].Kvalue = 2;
            KastenArray.kasten[5, 3].livetime = 3;
            KastenArray.kasten[5, 2].Kvalue = 2;
            KastenArray.kasten[5, 2].livetime = 2;
            KastenArray.kasten[5, 1].Kvalue = 2;
            KastenArray.kasten[5, 1].livetime = 1;
            KastenArray.kasten[5, 15].Kvalue = 4;
            Console.WriteLine("Next Step");

            boxes.Clear();
            int x, y;

            for (int i = 0; i < size_y; i++)
            {
                for (int j = 0; j < size_x; j++)
                {
                    x = j * boxSize;
                    y = i * boxSize;
                    boxes.Add(new Box(x, y, boxSize, boxSize, KastenArray.kasten[i, j].Kvalue));

                    //Console.WriteLine($"X: {i} Y: {j} k_value: {KastenArray.kasten[i, j].Kvalue}");
                    Console.Write($"{KastenArray.kasten[i, j].Kvalue}");
                }
                Console.WriteLine();  
            }
            Console.WriteLine("Next Step");
            flowLayoutPanel1.Invalidate();
            Current_Score.Focus();
            NextStepTimer();

            /*
            if (!isLooping)
            {
                // Wenn der Loop nicht läuft, starte ihn
                await ToggleLooping();
            }
            */
        }

        private void Highscores_button_Click(object sender, EventArgs e)
        {
            // Highscores anzeigen
            //string HS = string.Format("Highscore Dokument einfügen bitte", e.ToString());
            //MessageBox.Show(HS);

            // Excel-Dokument einlesen
            // Tempo in Nummer // Feldgröße in Buchstabe

            {
                string pathToExcelFile = Directorystring;
                Excel.Application xlApp = new Excel.Application();
                Excel.Workbook SnakeHighscore = xlApp.Workbooks.Open(pathToExcelFile);
                Excel._Worksheet xlWorksheet = SnakeHighscore.Sheets[1];
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
                SnakeHighscore.Close();
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }
        public void Highscore_newrecord()
        {
            string pathToExcelFile = Directorystring;
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook SnakeHighscore = xlApp.Workbooks.Open(pathToExcelFile);
            Excel._Worksheet xlWorksheet = SnakeHighscore.Sheets[1];
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
                Excel.Range cell = SnakeHighscore.Worksheets[1].Cells[HS_T_Name, HS_S_Name];
                cell.Value = enteredText;
                Excel.Range icell = SnakeHighscore.Worksheets[1].Cells[HS_T_Value, HS_S_Value];
                icell.Value = score;
                SnakeHighscore.Save();
                Highscore_Display.Invoke((System.Windows.Forms.MethodInvoker)delegate { Highscore_Display.Text = $"Highscore: {enteredText} -> {score}"; });
                Marshal.ReleaseComObject(cell);
                Marshal.ReleaseComObject(icell);

            }
            SnakeHighscore.Close();
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void Main_Window_KeyDown(object sender, KeyEventArgs e)
        {
            // string s = string.Format("{0} Taste gedrückt", e.ToString());
            // MessageBox.Show(s);
            // Test
        }

        private void Current_Score_KeyPress(object sender, KeyPressEventArgs e)
        {
            // string s = string.Format("{0} Taste gedrückt", e.KeyChar);
            // MessageBox.Show(s);
            // Test
        }

        private void Current_Score_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    //current_y --;
                    StoreStep = "Up";
                    break;
                case Keys.Down:
                    //current_y ++;
                    StoreStep = "Down";
                    break;
                case Keys.Left:
                    //current_x --;
                    StoreStep = "Left";
                    break;
                case Keys.Right:
                    //current_x ++;
                    StoreStep = "Right";
                    break;
                default:
                    break;
            }
            // if (tempo_x == 0)
            NextStepAction(new object());
        }

        private void Current_Score_Leave(object sender, EventArgs e)
        {
            //this.flowLayoutPanel1.Focus();
        }

        private void flowLayoutPanel1_Leave(object sender, EventArgs e)
        {
            //this.flowLayoutPanel1.Focus();
        }

        // Wert 0 = Freies Feld
        // Wert 1 = Kopf der Schlange
        // Wert 2 = Körper der Schlange
        // Wert 4 = Apfel
        // Wert 5 = BlueScreen of Death
        // bei Kollision "Game Over" screen 
        // Scoreboard

        public class Kasten
        {
            public int livetime;
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
                        //Console.WriteLine($"X: {i} Y: {j} k_value: {this.kasten[i, j].Kvalue}");
                        Console.Write($"{this.kasten[i, j].Kvalue}");
                    }
                    Console.WriteLine();
                }
            }
        }
        public class Headposition
        {
            public int current_x { get; set; }
            public int current_y { get; set; }

            public Headposition(Kasten[,] k_array)
            {
                TrackerHead(k_array);
            }
            public void TrackerHead(Kasten[,]? k_array)
            {
                if (k_array != null)
                {
                    for (int y = 0; y < k_array.GetLength(0); y++)
                    {
                        for (int x = 0; x < k_array.GetLength(1); x++)
                        {
                            if (k_array[y, x].Kvalue == 1)
                            {
                                current_x = x;
                                current_y = y;
                                return;
                            }
                        }
                    }
                }
            }
        }
        public void GameOver()
        {
            if (GameTimer != null || tempo_x == 0)
            {
                if (KastenArray != null)
                {
                    /*
                    StopAllMP3();
                    isLooping = false;
                    PlayMP3(sound_game_over);
                    */
                    int x = 0;
                    int y = 0;

                    for (; y < size_y;)
                    {
                        KastenArray.kasten[y, x].Kvalue = 5;
                        x++;
                        if (x >= size_x)
                        {
                            x = 0;
                            y++;
                        }
                    }
                    foreach (Box b in boxes)
                    {
                        b.Value = KastenArray.kasten[b.Y / boxSize, b.X / boxSize].Kvalue;
                    }
                    flowLayoutPanel1.Invalidate();
                }
                if (GameTimer != null)
                {
                    GameTimer.Dispose();
                    GameTimer = null;
                }
            }
            Enable_Button();
            Highscore_newrecord();
            Console.WriteLine("GAME OVER!");
        }
        public void Enable_Button()
        {
            if (FieldSizeComboBox.InvokeRequired == true)
            {
                FieldSizeComboBox.Invoke((System.Windows.Forms.MethodInvoker)delegate { Enable_Button(); });
                return;
            }
            FieldSizeComboBox.Enabled = true;
            Speed.Enabled = true;
            Score_Board.Enabled = true;
        }
        public void NextStepTimer()
        {
            if (tempo_x != 0)
            {
                if (GameTimer != null)
                {
                    GameTimer.Dispose();
                    GameTimer = null;
                }
                GameTimer = new System.Threading.Timer(NextStepAction, null, tempo_x, tempo_x); // 1000 milliseconds = 1 seconds
                                                                                                // Console.WriteLine("Timer gestartet");
                                                                                                // Console.ReadLine(); // Keep the program running
            }
        }
        public void CollisiontypeAndMove(Kasten C, Headposition H)
        {
            switch (C.Kvalue)
            {
                case 0:
                case 4:
                    MoveSpace(C, H);
                    break;
                case 2:
                    GameOver();
                    break;
                default:
                    break;
            }
        }
        public void NextStepAction(object? state)
        {
            if (KastenArray == null)
                return;

            Headposition hp = new Headposition(KastenArray.kasten);

            int y = hp.current_y;
            int x = hp.current_x;

            switch (StoreStep)
            {
                case "Up":
                    if (y - 1 >= 0)
                        CollisiontypeAndMove(KastenArray.kasten[y - 1, x], hp);
                    else
                        GameOver();
                    break;
                case "Down":
                    if (y + 1 < size_y)
                        CollisiontypeAndMove(KastenArray.kasten[y + 1, x], hp);
                    else
                        GameOver();
                    break;
                case "Left":
                    if (x - 1 >= 0)
                        CollisiontypeAndMove(KastenArray.kasten[y, x - 1], hp);
                    else
                        GameOver();
                    break;
                case "Right":
                    if (x + 1 < size_x)
                        CollisiontypeAndMove(KastenArray.kasten[y, x + 1], hp);
                    else
                        GameOver();
                    break;
                default:
                    GameOver();
                    break;
            }
            if (KastenArray != null)
            {
                for (int i = 0; i < size_y; i++)
                {
                    for (int j = 0; j < size_x; j++)
                    {
                        Console.Write($"{KastenArray.kasten[i, j].Kvalue}");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("Next Step");
            }
        }
        public void MoveSpace(Kasten S, Headposition P)
        {

            if (KastenArray != null)
            {
                bool DecrementIt = true;
                int s2 = S.Kvalue;


                S.Kvalue = KastenArray.kasten[P.current_y, P.current_x].Kvalue;
                KastenArray.kasten[P.current_y, P.current_x].Kvalue = 2;
                KastenArray.kasten[P.current_y, P.current_x].livetime = maxlivetime;

                if (s2 == 4)  // Ein Apfel erwidscht - Schlange wächst
                {
                    score++;
                    Current_Score.Invoke((System.Windows.Forms.MethodInvoker)delegate { Current_Score.Text = "Score: " + score.ToString(); });
                    maxlivetime++;
                    DecrementIt = false;
                    NewApple();
                }

                int x = 0;
                int y = 0;

                for (; y < size_y;)
                {
                    if (KastenArray.kasten[y, x].Kvalue == 2)
                    {
                        if (DecrementIt)
                        {
                            KastenArray.kasten[y, x].livetime--;
                        }
                        if (KastenArray.kasten[y, x].livetime <= 0)
                        {
                            KastenArray.kasten[y, x].Kvalue = 0;
                        }
                    }
                    x++;
                    if (x >= size_x)
                    {
                        x = 0;
                        y++;
                    }
                }

                foreach (Box b in boxes)
                {
                    b.Value = KastenArray.kasten[b.Y / boxSize, b.X / boxSize].Kvalue;
                }
                flowLayoutPanel1.Invalidate();
            }
        }
        public void NewApple()
        {
            if (KastenArray != null)
            {
                int max = 100; // Notbremse damit es sich im Worst Case nicht aufhängt
                do
                {
                    Random rnd = new Random();
                    int v = rnd.Next(size_x * size_y);
                    int x = v % size_x;
                    int y = v / size_y;

                    Kasten K = KastenArray.kasten[y, x];
                    if (K.Kvalue == 0)
                    {
                        K.Kvalue = 4;
                        break;
                    }

                } while (--max > 0);
                /*
                PlayMP3(sound_getting_apple);
                */
            }
        }
        /*
        private void Sound_Pick_Click(object sender, EventArgs e)
        {
            Sound_Window sw = new Sound_Window(this); // übergeben sie 'this', um die mainwindow-referenz zu übergeben
            sw.ShowDialog();
        }
        public async Task ToggleLooping()
        {
            if (!isLooping)
            {
                isLooping = true;
                while (isLooping)
                {
                    PlayMP3Loop(current_sound);
                    await Task.Delay(2000); // Anpassen der Wartezeit zwischen den Schleifendurchläufen
                }
            }
        }
        public static void PlayMP3(string mp3FilePath)
        {
            WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
            wplayer.URL = mp3FilePath;
            wplayer.controls.play();
            allPlayers.Add(wplayer);
        }

        public static void PlayMP3Loop(string mp3FilePath)
        {
            WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
            wplayer.URL = mp3FilePath;
            wplayer.settings.setMode("loop", true);
            wplayer.controls.play();
            allPlayers.Add(wplayer);
        }

        public static void PauseMP3()
        {
            foreach (var wplayer in allPlayers.ToList())
            {
                wplayer.controls.pause();
            }
        }

        public static void StopAllMP3()
        {
            foreach (var wplayer in allPlayers.ToList())
            {
                wplayer.controls.stop();
            }
            allPlayers.Clear(); // Lösche die Liste, um sicherzustellen, dass keine doppelten Einträge vorhanden sind
        }

        public double GetMP3Duration(string filePath)
        {
            try
            {
                WindowsMediaPlayer wplayer = new WindowsMediaPlayer();
                wplayer.URL = filePath;
                while (wplayer.currentMedia == null) { System.Threading.Thread.Sleep(100); } // Warten bis media geladen ist
                return wplayer.currentMedia.duration;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while getting MP3 duration: {ex.Message}");
                return 0;
            }
        }
        */
    }
}