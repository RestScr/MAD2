using SQLite;

namespace Model;

/// <summary>
/// Синглтон класс оператора с локальной БД.
/// </summary>
public class SQLiteOperator
{
    /// <summary>
    /// Частное свойство местонахождения файла БД.
    /// </summary>
    private string Filename { get; set; } = "DailyPlanner.db";

    /// <summary>
    /// Экземпляр асинхронного соединения к БД.
    /// </summary>
    public SQLiteAsyncConnection AsyncConnection { get; set; }

    /// <summary>
    /// Экземпляр синхронного соединения к БД.
    /// </summary>
    public SQLiteConnection Connection { get; set; }

    /// <summary>
    /// Статический частный экземпляр сущности синглтона.
    /// </summary>
    private static SQLiteOperator? _instance = null;

    /// <summary>
    /// Свойство получение сущности синглтона.
    /// </summary>
    public static SQLiteOperator Instance 
    { 
        get
        {
            if (_instance == null)
            {
                _instance = new SQLiteOperator();
            }

            return _instance;
        }
    }

    /// <summary>
    /// Закрытый конструктор синглтона.
    /// </summary>
    private SQLiteOperator()
    {
        AsyncConnection = new SQLiteAsyncConnection(Filename);
        Connection = new SQLiteConnection(Filename);
        Connection.CreateTable<Note>();
    }
}
