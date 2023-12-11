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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LbTeams.ItemsSource = teams;
            GetData();   
        }

        private void btnWin_Click(object sender, RoutedEventArgs e)
        {
            var player = LbPlayers.SelectedItem as Player;
            player?.AddResult(MatchOutcome.Win);
            LbTeams.Items.Refresh();
            LbPlayers.Items.Refresh();
            SortTeams();
        }

        private void btnLoss_Click(object sender, RoutedEventArgs e)
        {
            var player = LbPlayers.SelectedItem as Player;
            player?.AddResult(MatchOutcome.Loss);
            LbPlayers.Items.Refresh();
            LbTeams.Items.Refresh();
            SortTeams();
        }

        private void btnDraw_Click(object sender, RoutedEventArgs e)
        {
            var player = LbPlayers.SelectedItem as Player;
            player?.AddResult(MatchOutcome.Draw);
            LbPlayers.Items.Refresh();
            LbTeams.Items.Refresh();
            SortTeams();
        }
    }
}
