using System.Windows;

//====================================================
// Описание работы классов и методов исходника на:
// https://www.interestprograms.ru
// Исходные коды программ и игр
//====================================================

namespace Wpf3DRubiksCube
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        int count = 0;
        int k = 1;
        bool clockwise = false;
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Space) return;

            if (rcube.IsCanAnimation == false) return;

            switch (count)
            {
                case 0:
                    rcube.RotateLeftX(clockwise);
                    break;
                case 1:
                    rcube.RotateFrontZ(clockwise);
                    break;
                case 2:
                    rcube.RotateTopY(clockwise);
                    break;


                case 3:
                    rcube.RotateMiddleX(clockwise);
                    break;
                case 4:
                    rcube.RotateMiddleZ(clockwise);
                    break;
                case 5:
                    rcube.RotateMiddleY(clockwise);
                    break;


                case 6:
                    rcube.RotateRightX(clockwise);
                    break;
                case 7:
                    rcube.RotateBackZ(clockwise);
                    break;
                case 8:
                    rcube.RotateBottomY(clockwise);
                    break;
            }


            count += k;

            if (count == 9)
            {
                k = -1;
                clockwise = true;
                count = 8;
            }

            if (count == -1)
            {
                k = 1;
                clockwise = false;
                count = 0;
            }
        }
    }
}
