using Microsoft.Win32;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace asdf
{
    public partial class MainWindow : Window
    {
        private List<Szelveny> szelvenyek = new List<Szelveny>();
        private NapiKenoGUI napiKeno = new NapiKenoGUI();
        private Szelveny kivalasztottSzelveny = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnBetoltes_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.CurrentDirectory;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string[] sorok = File.ReadAllLines(openFileDialog.FileName);
                    szelvenyek.Clear();
                    lbSzelvenyek.Items.Clear();

                    foreach (string sor in sorok)
                    {
                        if (!string.IsNullOrWhiteSpace(sor))
                        {
                            Szelveny sz = new Szelveny(sor.Trim());
                            szelvenyek.Add(sz);
                            lbSzelvenyek.Items.Add(sz);
                        }
                    }

                    MessageBox.Show($"Sikeresen betöltve {szelvenyek.Count} szelvény!", "Információ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hiba a fájl betöltésekor: {ex.Message}", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnSorsolas_Click(object sender, RoutedEventArgs e)
        {
            napiKeno.Sorsolas();

            lbSorsoltSzamok.Items.Clear();
            foreach (int szam in napiKeno.HuzottSzamok)
            {
                lbSorsoltSzamok.Items.Add(szam);
            }

            if (kivalasztottSzelveny != null)
            {
                SzamolNyeremeny();
            }
        }

        private void lbSzelvenyek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbSzelvenyek.SelectedItem is Szelveny kivalasztott)
            {
                kivalasztottSzelveny = kivalasztott;

                tbTetSzorzo.Text = kivalasztott.TetSzorzo.ToString();

                MegjelenitSzelveny(kivalasztott);

                if (napiKeno.HuzottSzamok.Count > 0)
                {
                    SzamolNyeremeny();
                }
                else
                {
                    tbNyeremeny.Text = "-";
                }
            }
        }

        private void MegjelenitSzelveny(Szelveny szelveny)
        {
            ugSzelvenySzamok.Children.Clear();

            for (int i = 1; i <= 80; i++)
            {
                Border border = new Border();
                border.BorderBrush = Brushes.DarkGreen;
                border.BorderThickness = new Thickness(1);
                border.Margin = new Thickness(1);

                TextBlock txt = new TextBlock();
                txt.Text = i.ToString();
                txt.HorizontalAlignment = HorizontalAlignment.Center;
                txt.VerticalAlignment = VerticalAlignment.Center;

                if (szelveny.Tippek.Contains(i))
                {
                    border.Background = Brushes.Yellow;
                }
                else
                {
                    border.Background = Brushes.LightGreen;
                }

                border.Child = txt;
                ugSzelvenySzamok.Children.Add(border);
            }
        }

        private void SzamolNyeremeny()
        {
            if (kivalasztottSzelveny != null && napiKeno.HuzottSzamok.Count > 0)
            {
                int szorzo = KenoHelper.Szorzo(kivalasztottSzelveny.Tippek, napiKeno);
                int tet = kivalasztottSzelveny.TetSzorzo;
                int alapertek = 200;
                int nyeremeny = alapertek * tet * szorzo;

                tbNyeremeny.Text = $"{nyeremeny} Ft (szorzó: {szorzo})";
            }
        }
    }
}