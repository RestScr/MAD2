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
        List<Note> table = SQLiteOperator.Instance.Connection.Table<Note>().ToList();
        Notes = new ObservableCollection<Note>(table);
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
        createPage.NoteEntered += OnNoteEnteredOrUpdated;
        Navigation.PushAsync(createPage);
    }

    private void OnNoteEnteredOrUpdated(object sender, Note? note)
    {
        if (note == null)
        {
            return;
        }
        List<Note> table = SQLiteOperator.Instance.Connection.Table<Note>().ToList();
        Debug.WriteLine(table.Count);
        Notes = new ObservableCollection<Note>(table);
        NoteList.ItemsSource = Notes;
    }

    private void NoteList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedNote = (Note?)e.CurrentSelection.FirstOrDefault();

        if (SelectedNote == null)
        {
            EditNoteButton.IsEnabled = false;
            DeleteNoteButton.IsEnabled = false;
        }
        else
        {
            EditNoteButton.IsEnabled = true;
            DeleteNoteButton.IsEnabled = true;
        }
    }

    private void OnDeleteClicked(object sender, EventArgs e)
    {
        if (SelectedNote == null)
        {
            return;
        }
        int index = Notes.IndexOf(SelectedNote);
        Notes.Remove(SelectedNote);
        index--;

        if (index > -1)
        {
            SelectedNote = Notes[index];
        }
        else
        {
            SelectedNote = null;
        }

        NoteList.SelectedItem = SelectedNote;
    }
}
