namespace Model;

/// <summary>
/// Класс генератора идентификаторов.
/// </summary>
public class IdGenerator
{
    /// <summary>
    /// Статический словарь счетчика созданных объектов.
    /// </summary>
    public static Dictionary<string, int> CreatedObjects { get; private set; } = new Dictionary<string, int>();

    /// <summary>
    /// Статический метод генерации идентификаторов для классов.
    /// </summary>
    /// <param name="objectName">Имя класса (в основном полученное через nameof). </param>
    /// <returns> Личный идентификатор объекта. </returns>
    public static int GenerateId(string objectName)
    {
        if (!CreatedObjects.ContainsKey(objectName))
        {
            CreatedObjects.Add(objectName, 0);
        }

        return ++CreatedObjects[objectName];
    }
}
