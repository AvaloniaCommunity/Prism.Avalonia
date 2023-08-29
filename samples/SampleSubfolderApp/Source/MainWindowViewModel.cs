using Prism.Mvvm;

namespace SampleSubfolderApp;

public class MainWindowViewModel : BindableBase
{
    public MainWindowViewModel()
    {
        Title = "Sample App with Bin Folder!";
    }

    private string _title = "";

    /// <summary>Gets or sets the title of the View.</summary>
    public string Title { get => _title; set => SetProperty(ref _title, value); }
}
