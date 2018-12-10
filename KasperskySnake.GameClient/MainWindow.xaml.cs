using System;
using System.Collections.Generic;
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


using KasperskySnake.Api;
using KasperskySnake.Api.Models;

using System.Windows.Threading;
using System.Collections.ObjectModel;

namespace KasperskySnake.GameClient
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{



		int PixelFactor = 10;
		KasperskySnakeApi Api;
		string Token = "MHr5ChxBxkpcXOryL$oK";
		string MyName = "";
		public ObservableCollection<PlayerState> onlinePlayers = new ObservableCollection<PlayerState>();
		public ObservableCollection<PlayerState> OnlinePlayers
		{
			get
			{
				return onlinePlayers;
			}
		}
		//MaxWidth="642" MaxHeight="422"
		void HeartBeat(object sender, EventArgs e)
		{
			
			var gameboard = Api.GetGameboard();

			mainGrid.Width = (gameboard.gameBoardSize.width * PixelFactor) * 1.5;
			mainGrid.Height = (gameboard.gameBoardSize.height * PixelFactor);

			onlinePlayers.Clear();
			gameboard.players.Where(x => x.snake != null).Distinct().ToList().ForEach(i => onlinePlayers.Add(i));
			//onlinePlayers = new ObservableCollection<PlayerState>(  gameboard.players.Where(x => x.snake != null));
			//listBoxPlayerList.ItemsSource = onlinePlayers;

			lblMyName.Text = $"Ваше имя: {MyName }";
			byte[] playerNameHashCode = BitConverter.GetBytes(MyName.GetHashCode());
			 
			lblMyName.Foreground = new SolidColorBrush(Color.FromRgb(playerNameHashCode[0], playerNameHashCode[1], playerNameHashCode[2])); 
			lblRoundNumber.Text = $"Текущий номер раунда: {gameboard.roundNumber}";
			lblPlayersCount.Text = $"Игроков онлайн: {gameboard.players.Count(x => x.snake!=null)}";
			paintCanvas.Children.Clear();
			foreach (Rectangle2D wall in gameboard.walls)
			{
				Rectangle newRectangle = new Rectangle();
				newRectangle.Fill = Brushes.Gray;
				newRectangle.Width = PixelFactor* wall.width;
				newRectangle.Height = PixelFactor* wall.height;

				Canvas.SetTop(newRectangle, wall.y* PixelFactor);
				Canvas.SetLeft(newRectangle, wall.x* PixelFactor);
				paintCanvas.Children.Add(newRectangle);
			}




			foreach (Point2D food in gameboard.food)
			{
				Ellipse newEllipse = new Ellipse();
				newEllipse.Fill = Brushes.Red;
				newEllipse.Width = PixelFactor;
				newEllipse.Height = PixelFactor;

				Canvas.SetTop(newEllipse, food.y * PixelFactor);
				Canvas.SetLeft(newEllipse, food.x * PixelFactor);
				paintCanvas.Children.Add(newEllipse);
			}


			foreach (PlayerState player in gameboard.players)
			{
				if (player.snake != null)
				foreach (Point2D playerElement in player.snake)
				{
					Rectangle newRectangle = new Rectangle();
					playerNameHashCode = BitConverter.GetBytes( player.name.GetHashCode());
					newRectangle.Fill = new SolidColorBrush(Color.FromRgb (playerNameHashCode[0], playerNameHashCode[1], playerNameHashCode[2]));
					newRectangle.Width = PixelFactor;
					newRectangle.Height = PixelFactor;

					Canvas.SetTop(newRectangle, playerElement.y * PixelFactor);
					Canvas.SetLeft(newRectangle, playerElement.x * PixelFactor);
					paintCanvas.Children.Add(newRectangle);
				}
			}

		}


		public MainWindow()
		{
			//onlinePlayers = new ObservableCollection<PlayerState>();
			

			InitializeComponent();

			this.DataContext = this;
			Api = new KasperskySnakeApi();
			MyName = Api.GetPlayerNameByToken(Token).name;
			DispatcherTimer heartbeatTimer = new DispatcherTimer();

			heartbeatTimer.Tick += new EventHandler(HeartBeat);
			heartbeatTimer.Interval = TimeSpan.FromMilliseconds(500);
			heartbeatTimer.Start();


			this.KeyDown += new KeyEventHandler(OnButtonKeyDown);

		}


		private void OnButtonKeyDown(object sender, KeyEventArgs e)
		{



			switch (e.Key)
			{
				case Key.Down:
					Task.Run(() => Api.PlayerSetDirection(new DirectionRequest("Bottom", Token)));
					break;
				case Key.Up:
					Task.Run(() => Api.PlayerSetDirection(new DirectionRequest("Top", Token)));
					break;
				case Key.Left:
					Task.Run(() => Api.PlayerSetDirection(new DirectionRequest("Left", Token)));
					break;
				case Key.Right:
					Task.Run(() => Api.PlayerSetDirection(new DirectionRequest("Right", Token)));
					break;

			}

		}

	}
}
