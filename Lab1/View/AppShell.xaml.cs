using Lab1.Resources.Themes;

namespace Lab1;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(CreatePage), typeof(CreatePage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
    }

    public void OnDarkThemeClicked(object sender, EventArgs e)
    {
        SwitchTheme(new DarkTheme());
        Application.Current!.UserAppTheme = AppTheme.Dark;
    }

    public void OnLightThemeClicked(object sender, EventArgs e)
    {
        SwitchTheme(new LightTheme());
        Application.Current!.UserAppTheme = AppTheme.Light;
    }

    /// <summary>
    /// Функция смены стиля приложения.
    /// </summary>
    /// <param name="theme"> Тема, на которую нужно сменить стиль. </param>
    private void SwitchTheme(ResourceDictionary theme)
    {
        var dictionaries = Application.Current!.Resources.MergedDictionaries;

        var oldTheme = dictionaries.FirstOrDefault(
            dictionary => dictionary is LightTheme ||
            dictionary is DarkTheme
        );

        // Удаление старой темы
        if (oldTheme != null)
        {
            dictionaries.Remove(oldTheme);
        }

        // Добавление новой темы
        dictionaries.Add(theme);
    }
}
