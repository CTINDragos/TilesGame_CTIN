using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TilesGame_CTIN
{
	/// <summary>
	/// Interaction logic for Window2.xaml
	/// </summary>
	public partial class Window2 : Window
	{
		List<Tile>tiles = new List<Tile>();
		int Clicks = 0;
		int idOfSelectedComponent = -1;
		Button FirstButton = null;
		Button SecondButton = null;
		Player player1;
		bool WonGame=false;
		public Window2(Player player, int rows, int cols)
		{

			InitializeComponent();
			player1 = player;
			Closing += Window2_Closing;
			WindowState = WindowState.Maximized;
			List<Tile> tiles = GetTiles(rows, cols);
			ShuffleTiles(tiles);
			for (int i = 0; i < rows; i++)
			{
				ButtonsGrid.RowDefinitions.Add(new RowDefinition());
			}
			for (int j = 0; j < cols; j++)
			{
				ButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition());
			}
			for (int i = 0; i < tiles.Count; i++)
			{
				Button button = new Button();
				Image image = new Image();
				image.Source = new BitmapImage(new Uri(tiles[i].imagePath));
				button.Content = image;
				button.Click += AllButtons_Click;
				button.Tag = (int)tiles[i].id;
				button.Opacity = 0;
				Border border = new Border();
				border.Child = button;
				border.BorderBrush = new SolidColorBrush(Colors.Black);
				border.BorderThickness = new Thickness(2);
				border.Opacity = 1;
				ButtonsGrid.Children.Add(border);
				Grid.SetRow(border, i / cols);
				Grid.SetColumn(border, i % cols);
			}
			if (rows % 2 == 1 && cols % 2 == 1)
			{
				Image lastImage = new Image();
				lastImage.Source = new BitmapImage(new Uri("C:/Users/bogst/source/repos/TilesGame_CTIN/TilesPhotos/LastImage.jpg"));
				Border lastBorder = new Border();
				lastBorder.Child = lastImage;
				lastBorder.BorderBrush = new SolidColorBrush(Colors.Black);
				lastBorder.BorderThickness = new Thickness(2);
				lastBorder.Opacity = 1;

				ButtonsGrid.Children.Add(lastBorder);
				Grid.SetRow(lastBorder, tiles.Count / cols);
				Grid.SetColumn(lastBorder, tiles.Count % cols);
			}
		}
		private async void AllButtons_Click(object sender, RoutedEventArgs e)
		{
			if (FirstButton == null)
			{
				FirstButton = (Button)sender;
				(sender as Button).Opacity = 1;
				player1.Clicked++;
			}
			else if(SecondButton == null) 
			{
				player1.Clicked++;
				SecondButton = (Button)sender;
				(sender as Button).Opacity = 1;

				if (Convert.ToInt32(SecondButton.Tag) == Convert.ToInt32(FirstButton.Tag))
				{
					SecondButton.Visibility = Visibility.Collapsed;
					FirstButton.Visibility = Visibility.Collapsed;
					Border border1 = GetBorderFromButton(FirstButton);
					border1.BorderThickness = new Thickness(400);
					Border border2 = GetBorderFromButton(SecondButton);
					border2.BorderThickness = new Thickness(400);
					FirstButton = null;
					SecondButton = null;
					MessageBox.Show("Good Move");
					if (VerifyWin() == true)
					{

						MessageBox.Show("You won!");
						WonGame = true;
						player1.Level++;
						PrintInFolderFct(player1);
						this.Close();
					}
				}
				else 
				{
					await Task.Delay(TimeSpan.FromSeconds(1));
					Button Fbutton = FirstButton;
					Fbutton.Opacity = 0;
					Button Sbutton= SecondButton;
					Sbutton.Opacity = 0;
					FirstButton = null;
					SecondButton = null;
				}
			}
		}
		private Border GetBorderFromButton(Button button)
		{
			DependencyObject parent = VisualTreeHelper.GetParent(button);
			while (parent != null && !(parent is Border))
			{
				parent = VisualTreeHelper.GetParent(parent);
			}
			return parent as Border;
		}
		private void Window2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (WonGame == false)
			{
				player1.TimesHeLost++;
				PrintInFolderFct(player1);
			}
			Window1 window1= new Window1();
			window1.Show();
		}
		public static void ShuffleTiles(List<Tile> tiles)
		{
			Random rand = new Random();
			int n = tiles.Count;
			while (n > 1)
			{
				n--;
				int k = rand.Next(n + 1);
				Tile temp = tiles[k];
				tiles[k] = tiles[n];
				tiles[n] = temp;
			}
		}
		public bool VerifyWin()
		{
			foreach (var child in ButtonsGrid.Children)
			{
				if (child is Border border)
				{
					if (border.Child is Button button && button.Visibility == Visibility.Visible)
					{
						return false;
					}
				}
			}
			return true;
		}
		private void PrintInFolderFct(Player player)
		{
			using (StreamWriter writer = File.AppendText("example.txt"))
			{
				writer.WriteLine(player.Name + "," +player.Image  + ","+player.Level+","+player.TimesHeLost+","+player.Clicked);
			}
		}
		public List<Tile> GetTiles(int rows, int cols)
		{
		
			List<Tile> list = new List<Tile>();
			for (int i = 0; i < (rows * cols) / 2; i++)
			{
				Tile tile = new Tile(i, $"C:/Users/bogst/source/repos/TilesGame_CTIN/TilesPhotos/image{i + 1}.jpg");
				Tile tileDuplicate = new Tile(i, $"C:/Users/bogst/source/repos/TilesGame_CTIN/TilesPhotos/image{i + 1}.jpg");
				list.Add(tile);
				list.Add(tileDuplicate);
			}

			return list;
		}

	}
}
