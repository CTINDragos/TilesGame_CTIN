using System;
using System.Collections.Generic;
using System.IO;
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

namespace TilesGame_CTIN
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
	
		List<string> availableImages = new List<string>
{
	"C:/Users/bogst/source/repos/TilesGame_CTIN/photos/image1.jpg",
	"C:/Users/bogst/source/repos/TilesGame_CTIN/photos/image2.jpg",
	"C:/Users/bogst/source/repos/TilesGame_CTIN/photos/image3.jpg",
	"C:/Users/bogst/source/repos/TilesGame_CTIN/photos/image4.jpg",
	"C:/Users/bogst/source/repos/TilesGame_CTIN/photos/image5.jpg",
	"C:/Users/bogst/source/repos/TilesGame_CTIN/photos/image7.png"
};
		private int currentIndex = 0;

		private void NextButton_Click(object sender, RoutedEventArgs e)
		{
			currentIndex++;
			if (currentIndex >= availableImages.Count)
			{
				currentIndex = 0;
			}
			string nextImage = availableImages[currentIndex];
			myImage.Source = new BitmapImage(new Uri(nextImage, UriKind.Absolute));
		}
		private void PreviousButton_Click(object sender, RoutedEventArgs e)
		{
			currentIndex--;
			if (currentIndex <=0)
			{
				currentIndex = availableImages.Count-1;
			}
			string prevImage = availableImages[currentIndex];
			myImage.Source = new BitmapImage(new Uri(prevImage, UriKind.Absolute));
		}
		private String GetName()
		{
			return nameTextField.Text;
		}
		private void PrintInFolderFunction(object sender,RoutedEventArgs e)
		{
			using (StreamWriter writer = File.AppendText("example.txt"))
			{
				writer.WriteLine(GetName() + "," + availableImages[currentIndex] +",1,0,0");
			}
			Window1 nextWi = new Window1();
			nextWi.Show();
			this.Close();
		}
		private void BackToMainWindow(object sender, RoutedEventArgs e)
		{
			Window1 nextWi = new Window1();
			nextWi.Show();
			this.Close();
		}
		public MainWindow()
		{
			InitializeComponent();
		}
	}
}
