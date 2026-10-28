using SQLite;

namespace Model;

/// <summary>
/// Системный класс сохраняемых конфигураций приложения
/// </summary>
[Table("app_config")]
public class AppConfig
{
    private UsingTheme _applicationTheme = UsingTheme.DarkTheme;

    [PrimaryKey, AutoIncrement, Column("_id")]
    public int Id { get; init; }

    public UsingTheme ApplicationTheme { 
        get
        {
            return _applicationTheme;
        }
        set
        {
            _applicationTheme = value;
        }
    }
}
