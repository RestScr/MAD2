using System.Collections.ObjectModel;
using System.Diagnostics;
using Model;

namespace Lab1;

public partial class MainPage : ContentPage
{
    public Note? SelectedNote { get; set; } = null;

    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    public MainPage()
    {
        InitializeComponent();

        NoteList.ItemsSource = Notes;
    }

    private void OnEditClicked(object sender, EventArgs e)
    {
        if (SelectedNote == null)
        {
            return;
        }
        CreatePage createPage = new CreatePage(SelectedNote);
        Navigation.PushAsync(createPage);
    }

    private void OnCreateClicked(object sender, EventArgs e)
    {
        CreatePage createPage = new CreatePage(null);
        createPage.NoteEntered += OnNoteEntered;
        Navigation.PushAsync(createPage);
    }

    private void OnNoteEntered(object sender, Note? note)
    {
        if (note == null)
        {
            return;
        }

        Notes.Add(note);
    }

    private void NoteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedNote = (Note?)e.CurrentSelection.FirstOrDefault();

        if (SelectedNote == null)
        {
            EditNoteButton.IsEnabled = false;
        }
        else
        {
            EditNoteButton.IsEnabled = true;
        }
    }
}
