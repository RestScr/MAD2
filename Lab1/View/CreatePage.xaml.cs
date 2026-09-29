using System.Diagnostics;
using Model;

namespace Lab1;

public partial class CreatePage : ContentPage
{
    Note? EditingNote { get; set; } = null;

	public CreatePage(Note? selectedNote)
	{
        EditingNote = selectedNote;
		InitializeComponent();

        if (EditingNote != null)
        {
            TitleEntry.Text = EditingNote.Title;
            DescriptionEntry.Text = EditingNote.Description;
            DatePick.Date = EditingNote.PickedDateTime;
        }
	}

    public event EventHandler<Note?> NoteEntered;

    /// <summary>
    /// Логика сохранения заметки.
    /// </summary>
    /// <param name="sender"> Объект, пославший событие. </param>
    /// <param name="e"> Параметры события. </param>
    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        string title = TitleEntry.Text;
        string description = DescriptionEntry.Text;
        DateTime date = DatePick.Date;

        try
        {
            if (EditingNote == null)
            {
                Note newNote = new Note(title, description, date);
                EditingNote = newNote;
                await SQLiteOperator.Instance.AsyncConnection.InsertAsync(EditingNote);
            }
            else
            {
                EditingNote.Title = title;
                EditingNote.Description = description;
                EditingNote.PickedDateTime = date;
                EditingNote.Updated = DateTime.Now;
                await SQLiteOperator.Instance.AsyncConnection.UpdateAsync(EditingNote);
            }
            await Navigation.PopAsync();
            NoteEntered?.Invoke(this, EditingNote);
            Debug.WriteLine("dffdsddsfdfssdf");
        }
        catch (ArgumentException)
        {

        }
    }

    /// <summary>
    /// Логика отмены создания заметки.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CancelButton_Clicked(object sender, EventArgs e)
    {
        NoteEntered?.Invoke(this, null);
        Navigation.PopAsync();
    }
}