using System.Collections.ObjectModel;
using Model;

namespace Lab1;

public partial class MainPage : ContentPage
{
    /// <summary>
    /// Свойство выбранной заметки.
    /// </summary>
    public Note? SelectedNote { get; set; } = null;

    /// <summary>
    /// Свойство списка с заметками.
    /// </summary>
    public ObservableCollection<Note> Notes { get; set; } = new ObservableCollection<Note>();

    /// <summary>
    /// Стандартный конструктор главной страницы.
    /// </summary>
    public MainPage()
    {
        
        InitializeComponent();
        List<Note> table = SQLiteOperator.Instance.Connection.Table<Note>().ToList();
        Notes = new ObservableCollection<Note>(table);
        NoteList.ItemsSource = Notes;
    }

    /// <summary>
    /// Функция, срабатывающая при нажатии кнопки изменения.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnEditClicked(object sender, EventArgs e)
    {
        if (SelectedNote == null)
        {
            return;
        }
        CreatePage createPage = new CreatePage(SelectedNote);
        Navigation.PushAsync(createPage);
    }

    /// <summary>
    /// Функция, срабатывающая при нажатии кнопки создания.
    /// </summary>
    /// <param name="sender"> Отправитель события </param>
    /// <param name="e"> Аргументы события. </param>
    private void OnCreateClicked(object sender, EventArgs e)
    {
        CreatePage createPage = new CreatePage(null);
        createPage.NoteEntered += OnNoteEnteredOrUpdated;
        Navigation.PushAsync(createPage);
    }

    /// <summary>
    /// Функция события обновления или создания заметки, 
    /// которая обновляет основной список заметок.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="note"></param>
    private void OnNoteEnteredOrUpdated(object sender, Note? note)
    {
        if (note == null)
        {
            return;
        }
        List<Note> table = SQLiteOperator.Instance.Connection.Table<Note>().ToList();
        Notes = new ObservableCollection<Note>(table);
        NoteList.ItemsSource = Notes;
    }

    /// <summary>
    /// Функция, вызывающаяся при изменении выбранной заметки.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// Функция клиика на кнопку удаления.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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
