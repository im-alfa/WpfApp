using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Team> teams = new ObservableCollection<Team>();
        
        /// <summary>
        /// Sorts the teams by points and updates the listbox
        /// </summary>
        private void SortTeams()
        {
            // keep track of what was selected
            var selectedTeam = LbTeams.SelectedItem as Team;
            var selectedPlayer = LbPlayers.SelectedItem as Player;
            
            // .Sort() is not implemented for ObservableCollection<T> :(
            teams = new ObservableCollection<Team>(teams.OrderByDescending(team => team.Points));
            LbTeams.ItemsSource = null;
            LbTeams.ItemsSource = teams;
            
            // reselect the team
            LbTeams.SelectedItem = selectedTeam;
            LbPlayers.SelectedItem = selectedPlayer;
        }
        
        /// <summary>
        /// Populates the listbox with example data
        /// </summary>
        private void GetData()
        {
            // populates the listbox with data
            var t1 = new Team("France");
            var t2 = new Team("Italy");
            var t3 = new Team("Spain");
            
            //French players
            var p1 = new Player("Marie", "WWDDL");
            var p2 = new Player("Claude" , "DDDLW");
            var p3 = new Player("Antoine", "LWDLW"); 
            t1.AddPlayers(p1, p2, p3);
            
            //Italian players
            var p4 = new Player("Marco", "WWDLL");
            var p5 = new Player("Giovanni", "LLLLD");
            var p6 = new Player("Valentina", "DLWWW");
            t2.AddPlayers(p4, p5, p6);
            
            //Spanish players
            var p7 = new Player("Maria", "WWWWW");
            var p8 = new Player("Jose", "LLLLL");
            var p9 = new Player("Pablo", "DDDDD");
            t3.AddPlayers(p7, p8, p9);
            
            // add players to teams
            teams.Add(t1);
            teams.Add(t2);  
            teams.Add(t3);
            SortTeams();
        }
        
        /// <summary>
        /// Updates the listbox with the players of the selected team
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var team = LbTeams.SelectedItem as Team;
            LbPlayers.ItemsSource = null;
            LbPlayers.ItemsSource = team?.Players;
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Loads the data and sets the listbox items source on window load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LbTeams.ItemsSource = teams;
            GetData();   
        }

        /// <summary>
        /// Adds a win to the selected player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWin_Click(object sender, RoutedEventArgs e)
        {
            var player = LbPlayers.SelectedItem as Player;
            player?.AddResult(MatchOutcome.Win);
            LbTeams.Items.Refresh();
            LbPlayers.Items.Refresh();
            SortTeams();
        }

        /// <summary>
        /// Adds a loss to the selected player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoss_Click(object sender, RoutedEventArgs e)
        {
            var player = LbPlayers.SelectedItem as Player;
            player?.AddResult(MatchOutcome.Loss);
            LbPlayers.Items.Refresh();
            LbTeams.Items.Refresh();
            SortTeams();
        }

        /// <summary>
        /// Adds a draw to the selected player
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            var player = LbPlayers.SelectedItem as Player;
            player?.AddResult(MatchOutcome.Draw);
            LbPlayers.Items.Refresh();
            LbTeams.Items.Refresh();
            SortTeams();
        }

        /// <summary>
        /// Updates the stars based on the selected player's points
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var selectedPlayer = LbPlayers.SelectedItem as Player;
            var points = selectedPlayer?.Points;
            
            // Ugly workaround to get the image source from a string. Couldn't find a better way :'|
            // https://copyprogramming.com/howto/csharp-wpf-set-image-source-from-string
            var converter = new ImageSourceConverter();
            var emptyStar = converter.ConvertFromString("pack://application:,,,/staroutline.png") as ImageSource;
            var fullStar = converter.ConvertFromString("pack://application:,,,/starsolid.png") as ImageSource;
            
            switch (points)
            {
                case 0:
                    ImgStar1.Source = emptyStar;
                    ImgStar2.Source = emptyStar;
                    ImgStar3.Source = emptyStar;
                    break;
                case { } n when (n > 0 && n <= 5):
                    ImgStar1.Source = fullStar;
                    ImgStar2.Source = emptyStar;
                    ImgStar3.Source = emptyStar;
                    break;
                case { } n when (n > 5 && n <= 10):
                    ImgStar1.Source = fullStar;
                    ImgStar2.Source = fullStar;
                    ImgStar3.Source = emptyStar;
                    break;
                case { } n when (n > 10):
                    ImgStar1.Source = fullStar;
                    ImgStar2.Source = fullStar;
                    ImgStar3.Source = fullStar;
                    break;
                default:
                    ImgStar1.Source = emptyStar;
                    ImgStar2.Source = emptyStar;
                    ImgStar3.Source = emptyStar;
                    break;
            }
        }
    }
}
