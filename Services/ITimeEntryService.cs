using TimerApp.Entity;

namespace TimerApp.Services
{
    public interface ITimeEntryService
    {
        Task<List<TimeEntry>> GetAllTimeEntries();
        Task<List<TimeEntry>> GetDailyTimeEntries();
        Task<TimeEntry> GetTimeEntry(int id);
        Task<TimeEntry> AddTimeEntry(TimeEntry timeEntry);
        Task<bool> DeleteTimeEntry(int id);
        Task<TimeEntry> EditTimeEntry(int id, TimeEntry timeEntry);
    }
}
