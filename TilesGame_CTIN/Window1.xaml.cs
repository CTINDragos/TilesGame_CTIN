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
using System.Windows.Shapes;
using System.IO;

namespace TilesGame_CTIN
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public List<Player> playerList { get; set; } = new List<Player>();
		public void ReadFile()
		{
			string filePath = "C:/Users/bogst/source/repos/TilesGame_CTIN/TilesGame_CTIN/bin/Debug/net6.0-windows/example.txt";

			using (StreamReader reader = new StreamReader(filePath))
			{
				string line;

				while ((line = reader.ReadLine()) != null)
				{
					List<string> words = new List<string>();
					words = line.Split(',').ToList();
					int levelPlayer = int.Parse(words[2]);
					int timeslost = int.Parse(words[3]);
					int clicked = int.Parse(words[4]);
					Player dataToSave = new Player(words[0], words[1], levelPlayer, timeslost, clicked);
					int isPresent = -1;
					for (int i = 0; i < playerList.Count; i++)
					{
						if (playerList[i].Name == dataToSave.Name)
						{
							isPresent = i; break;
						}
					}
					if (isPresent == -1)
						playerList.Add(dataToSave);
					else
					{
						playerList[isPresent] = dataToSave;
					}
				}
			}
		}
		public Window1()
		{
			ReadFile();
			DataContext = this;
			InitializeComponent();
		
		}
		private void playerListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (playerListView.SelectedItem != null)
			{
				Player selectedPlayer = playerListView.SelectedItem as Player;
				string imagePath = selectedPlayer.Image;
				playerImage.Source = new BitmapImage(new Uri(imagePath));
			}
		}
		private void StartGameButton_Click(object sender, RoutedEventArgs e)
		{
			if (playerListView.SelectedItem != null)
			{
				Player selectedPlayer = playerListView.SelectedItem as Player;
				string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the number of rows and columns separated by a comma", "Game Setup");
				if (!string.IsNullOrEmpty(input))
				{
					string[] values = input.Split(',');
					if (values.Length == 2 && int.TryParse(values[0], out int numRows) && int.TryParse(values[1], out int numCols))
					{
						if (numRows >2&&numCols>2)
						{
							Window2 window2 = new Window2(selectedPlayer, numRows, numCols);
							window2.Show();
							this.Close();
						}
						else { MessageBox.Show("Invalid input. Number of rows and number of columns must be bigger than 2 .", "Error"); }
					}
					else
					{
						// User entered invalid input
						MessageBox.Show("Invalid input. Please enter two integers separated by a comma.", "Error");
					}
				}
				else
				{
					// User clicked Cancel or closed the dialog box
					// TODO: handle this case
				}
				
			}
		}
		private void CreateCharButton_Click(object sender,RoutedEventArgs e) 
		{
			MainWindow main = new MainWindow();
			main.Show();
			this.Close();
			
		}
	}
}