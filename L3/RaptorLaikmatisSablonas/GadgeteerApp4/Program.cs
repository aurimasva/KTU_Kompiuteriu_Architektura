using System;
using System.Collections;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Presentation.Shapes;
using Microsoft.SPOT.Touch;

using Gadgeteer.Networking;
using GT = Gadgeteer;
using GTM = Gadgeteer.Modules;
using Gadgeteer.Modules.GHIElectronics;

namespace GadgeteerApp4
{
    public partial class Program
    {
        #region Programos sablonas
        #region kintamuju aprasymas
        private Boolean timerPaleistas = false;
        private GT.Timer timer = new GT.Timer(1000);
        private int minutes = 0;
        private int sekundes = 0;
        private Font font = Resources.GetFont(Resources.FontResources.consolas_72);
        private Font font2 = Resources.GetFont(Resources.FontResources.consolas_24);
        private Font font3 = Resources.GetFont(Resources.FontResources.Arial_18);
        private Window window;
        private Canvas canvas;
        private Text antraste;
        private Text laikas;
        private Image paleistiStabdyti;
        private Image atkurti;
        #endregion
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/


            // Use Debug.Print to show messages in Visual Studio's "Output" window during debugging.
            Debug.Print("Program Started");
            timer.Tick += new GT.Timer.TickEventHandler(timer_Tick);
            SetupWindow();
        }
        void timer_Tick(GT.Timer timer)
        {

            sekundes += 1;
            string skaitiklisSekundes = "";
            string skaitiklisMinutes = "";
            string skaitiklisLaikas = "";
            if (sekundes >= 60)
            {
                minutes += 1;
                sekundes = 0;
            }
            if (sekundes <= 9)
                skaitiklisSekundes = "0" + sekundes.ToString();
            else
                skaitiklisSekundes = sekundes.ToString();
            if (minutes <= 9)
                skaitiklisMinutes = "0" + minutes.ToString();
            else
                skaitiklisMinutes = minutes.ToString();
            skaitiklisLaikas = skaitiklisMinutes + ":" + skaitiklisSekundes;

            laikas.TextContent = skaitiklisLaikas;
        }
        void SetupWindow()
        {
            #region Lango elementu konfiguravimas
            window = displayT43.WPFWindow;
            canvas = new Canvas();
            window.Child = canvas;
            paleistiStabdyti = new Image(Resources.GetBitmap(Resources.BitmapResources.Paleisti_Stabdyti));
            atkurti = new Image(Resources.GetBitmap(Resources.BitmapResources.Atkurti));
            canvas.Children.Add(paleistiStabdyti);
            canvas.Children.Add(atkurti);
            Canvas.SetLeft(paleistiStabdyti, 10);
            Canvas.SetTop(paleistiStabdyti, 230);
            Canvas.SetLeft(atkurti, 230);
            Canvas.SetTop(atkurti, 230);
            antraste = new Text(font2, "Laikmatis");
            antraste.ForeColor = GT.Color.Black;
            laikas = new Text(font, "00:00");
            laikas.ForeColor = GT.Color.Black;
            canvas.Children.Add(antraste);
            canvas.Children.Add(laikas);
            Canvas.SetLeft(antraste, 140);
            Canvas.SetTop(antraste, 0);
            Canvas.SetLeft(laikas, 100);
            Canvas.SetTop(laikas, 120);
            #endregion
        #endregion
            //Veiksmas 1
            atkurti.TouchDown += new Microsoft.SPOT.Input.TouchEventHandler(atkurti_TouchDown);
            //Veiksmas 2
            paleistiStabdyti.TouchDown += new Microsoft.SPOT.Input.TouchEventHandler(paleistiStabdyti_TouchDown);
            //Veiksmas 3
            led7C.SetColor(LED7C.Color.Red);
            //led7R.TurnAllLedsOff();

        }

        void paleistiStabdyti_TouchDown(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            if (timerPaleistas)
            {
                this.timerPaleistas = false;
                timer.Stop();
                led7C.SetColor(LED7C.Color.Red);
                //led7R.TurnAllLedsOff();
            }
            else
            {
                this.timerPaleistas = true;
                timer.Start();
                led7C.SetColor(LED7C.Color.Green);
                //led7R.TurnAllLedsOn();
            }
        }

        void atkurti_TouchDown(object sender, Microsoft.SPOT.Input.TouchEventArgs e)
        {
            sekundes = 0;
            minutes = 0;
            laikas.TextContent = "00:00";
            if (timerPaleistas)
            {
                led7C.SetColor(LED7C.Color.Green);
                //led7R.TurnAllLedsOn();
            }
            else
            {
                led7C.SetColor(LED7C.Color.Red);
               // led7R.TurnAllLedsOff();
            }
        }
    }
}
