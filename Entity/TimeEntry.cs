namespace TimerApp.Entity
{
    public class TimeEntry
    {
        public int Id { get; set; }
        public required int Minutes { get; set; }
        public required Category Category { get; set; }
        public required DateTime DateTime { get; set; } = DateTime.UtcNow;

    }

    public enum Category
    {
       Estudio,// 0
       Trabajo, // 1
       Programación, // 2
       Ejercicio, // 3
       Meditación, // 4
       Lectura // 5
    }
}
