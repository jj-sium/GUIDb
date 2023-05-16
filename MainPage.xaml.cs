using SQLite;

namespace ghinelli.johan._4h.GUIDb;

public partial class MainPage : ContentPage
{
	int count = 0;
	string targetFile = System.IO.Path.Combine(FileSystem.AppDataDirectory, "chinook.db");
	SQLite.SQLiteOpenFlags sium = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.SharedCache;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
		if (!File.Exists(targetFile))
		{
			using (Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("chinook.db"))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					fileStream.CopyTo(memoryStream);
					File.WriteAllBytes(targetFile, memoryStream.ToArray());
				}
			}
		}
		SQLite.SQLiteOpenFlags sium = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.SharedCache;
		SQLiteAsyncConnection cn1 = new SQLiteAsyncConnection(targetFile, sium);

		List<Artist> tblArtists;

		tblArtists = await cn1.QueryAsync<Artist>("select * from artists where name like 'b%'");

		CounterBtn.Text = $"In questo database ci sono {tblArtists.Count()} artisti.";
		dgDati.ItemsSource = tblArtists;
	}
}

