using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Model;

/// <summary>
/// Класс оператора с локальной БД.
/// </summary>
public class SQLiteOperator
{
    private string Filename { get; set; }
    public SQLiteAsyncConnection Connection { get; set; }

    private static SQLiteOperator? _instance = null;

    public static SQLiteOperator? Instance 
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

    private SQLiteOperator()
    {
        Filename = "DailyPlanner.db";
        Connection = new SQLiteAsyncConnection(Filename);
        Connection.CreateTableAsync<Note>();
    }
}
