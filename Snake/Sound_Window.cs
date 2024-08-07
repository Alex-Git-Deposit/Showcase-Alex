using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class Sound_Window : Form
    {
        /*
        private Main_Window? mainWindow;
        private Queue<string> soundQueue = new Queue<string>();
        private bool isClosing = false;
        private bool isPlaying = false; // Variable, um den Abspielstatus zu verfolgen
        public List<string> soundFiles = new List<string>(); // Liste der Sounddateien

        public Sound_Window()
        {
            InitializeComponent();
        }

        public Sound_Window(Main_Window mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            soundFiles.Add(Main_Window.sound_01);
            soundFiles.Add(Main_Window.sound_02);
            soundFiles.Add(Main_Window.sound_03);
        }

        private void PlaySound1Button_Click(object sender, EventArgs e)
        {
            StopCurrentSound();
            Main_Window.current_sound = Main_Window.sound_01;
            Main_Window.PlayMP3Loop(Main_Window.sound_01);
        }

        private void PlaySound2Button_Click(object sender, EventArgs e)
        {
            StopCurrentSound();
            Main_Window.current_sound = Main_Window.sound_02;
            Main_Window.PlayMP3Loop(Main_Window.sound_02);
        }

        private void PlaySound3Button_Click(object sender, EventArgs e)
        {
            StopCurrentSound();
            Main_Window.current_sound = Main_Window.sound_03;
            Main_Window.PlayMP3Loop(Main_Window.sound_03);
        }

        private void OnOff_Switch_Click(object sender, EventArgs e)
        {
            switch (OnOff_Switch.Text)
            {
                case "Turn Sound ON":
                    OnOff_Switch.Text = "Turn Sound OFF";
                    
        .WriteLine("Sound aktiv");
                    break;
                case "Turn Sound OFF":
                    OnOff_Switch.Text = "Turn Sound ON";
                    Console.WriteLine("Sound endet");
                    break;
            }
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            Main_Window.PauseMP3();
        }

        public async void Loop_All_Click(object sender, EventArgs e)
        {
            if (mainWindow != null && !mainWindow.isLooping)
            {
                if (!isPlaying)
                {
                    isPlaying = true;
                    Loop_All.Enabled = false; // Deaktiviere den Button, um mehrfache Ausführungen zu vermeiden
                    mainWindow.isLooping = true;
                    await Task.Run(async () =>
                    {
                        try
                        {
                            while (isPlaying)
                            {
                                if (soundQueue.Count == 0)
                                {
                                    InitializeSoundQueue();
                                }

                                if (soundQueue.Count > 0)
                                {
                                    var soundFile = soundQueue.Dequeue();
                                    Main_Window.current_sound = soundFile;
                                    await PlaySoundAsync(soundFile);
                                    soundQueue.Enqueue(soundFile);
                                }
                            }
                            PrintQueueContents();
                        }
                        finally
                        {
                            if (!isClosing)
                            {
                                Loop_All.Enabled = true; // Aktiviere den Button wieder, nachdem die Schleife beendet wurde
                            }
                            mainWindow.isLooping = false;
                        }
                    });
                }
            }
        }

        private void InitializeSoundQueue()
        {
            soundQueue.Clear();
            foreach (var soundFile in soundFiles)
            {
                soundQueue.Enqueue(soundFile);
            }
        }

        private async Task PlaySoundAsync(string soundFile)
        {
            if (mainWindow != null)
            {
                Main_Window.PlayMP3Loop(soundFile);
                double duration = mainWindow.GetMP3Duration(soundFile);
                await Task.Delay(TimeSpan.FromSeconds(duration)); // Wait until the sound is played
                Main_Window.StopAllMP3(); // Stop the current sound before playing the next
            }
        }

        private void Sound_01_Click(object sender, EventArgs e)
        {
            StopCurrentSound();
            Main_Window.current_sound = Main_Window.sound_01;
        }

        private void Sound_02_Click(object sender, EventArgs e)
        {
            StopCurrentSound();
            Main_Window.current_sound = Main_Window.sound_02;
        }

        private void Sound_03_Click(object sender, EventArgs e)
        {
            StopCurrentSound();
            Main_Window.current_sound = Main_Window.sound_03;
        }

        private void Sound_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
            isClosing = true;
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (isPlaying)
            {
                e.Cancel = true; // Abbrechen des Schließens des Formulars
                isClosing = false;
                Hide(); // Das Formular ausblenden, anstatt es zu schließen
            }
            else
            {
                isClosing = true;
            }
        }

        private void PrintQueueContents()
        {
            Console.WriteLine("Inhalt der Queue:");
            foreach (var item in soundQueue)
            {
                Console.WriteLine(item);
            }
        }

        private void StopCurrentSound()
        {
            isPlaying = false;
            Main_Window.StopAllMP3();
        }
        */
    }
}