using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Model;

/// <summary>
/// Класс заметки
/// </summary>
public class Note : INotifyPropertyChanged
{
    /// <summary>
    /// Поле названия заметки.
    /// </summary>
    private string _title = "New Note";

    /// <summary>
    /// Поле описания заметки.
    /// </summary>
    private string _description = "Description";

    /// <summary>
    /// Поле времени создания заметки.
    /// </summary>
    private DateTime _created;

    /// <summary>
    /// Поле времени изменения заметки.
    /// </summary>
    private DateTime _updated;

    /// <summary>
    /// Поле выбранного даты и времени пользователем.
    /// </summary>
    private DateTime _pickedDateTime = DateTime.Now;

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    /// <summary>
    /// Идентификатор заметки.
    /// </summary>
    public int Id { get; init; } = IdGenerator.GenerateId(nameof(Note));

    /// <summary>
    /// Название заметки.
    /// </summary>
    public string Title
    {
        get
        {
            return _title;
        }
        set
        {
            if (value == null || value == new String(' ', value.Length))
            {
                throw new ArgumentException("Error, The Title of Note cannot be empty.");
            }

            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    /// <summary>
    /// Текст заметки.
    /// </summary>
    public string Description
    { 
        get
        {
            return _description;
        }
        set
        {
            _description = value;
            OnPropertyChanged(nameof(Description));
        }
    }

    /// <summary>
    /// Время создания заметки.
    /// </summary>
    public DateTime Created
    {
        get
        {
            return _created;
        }
        init
        {
            _created = value;
            Updated = value;
        }
    }

    /// <summary>
    /// Время изменения заметки.
    /// </summary>
    public DateTime Updated
    {
        get
        {
            return _updated;
        }
        set
        {
            _updated = value;
            OnPropertyChanged(nameof(Updated));
        }
    }

    /// <summary>
    /// Свойство даты и времени, выбираемого пользователем.
    /// </summary>
    public DateTime PickedDateTime 
    {
        get
        {
            return _pickedDateTime;
        }
        set
        {
            _pickedDateTime = value;
            OnPropertyChanged(nameof(PickedDateTime));
        }
    }

    /// <summary>
    /// Публичный конструктор заметки.
    /// </summary>
    /// <param name="title"> Заголовок заметки. </param>
    /// <param name="description"> Текст заметки.</param>
    public Note(string title, string description, DateTime pickedDateTime)
    {
        Title = title;
        Description = description;
        Created = DateTime.Now;
        PickedDateTime = pickedDateTime;
    }
}

