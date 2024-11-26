using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TimerApp.Data;
using TimerApp.Entity;

namespace TimerApp.Services
{
    public class TimeEntryService : ITimeEntryService
    {
        private readonly DataContext _context;

        public TimeEntryService(DataContext context)
        {
            _context = context;
        }

        public async Task<TimeEntry> AddTimeEntry(TimeEntry timeEntry)
        {
            _context.TimeEntries.Add(timeEntry);
            await _context.SaveChangesAsync();

            return timeEntry;
        }

        public async Task<bool> DeleteTimeEntry(int id)
        {
            TimeEntry TE = await _context.TimeEntries.Where(t => t.Id == id).FirstOrDefaultAsync();
            _context.Remove(TE);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<TimeEntry> EditTimeEntry(int id, TimeEntry timeEntry)
        {
            var dbTimeEntry = await _context.TimeEntries.FindAsync(id);
            if (dbTimeEntry == null)
            {
                throw new KeyNotFoundException("Time entry not found.");
            }

            dbTimeEntry.Category = timeEntry.Category;
            dbTimeEntry.Minutes = timeEntry.Minutes;
            dbTimeEntry.DateTime = dbTimeEntry.DateTime;
            dbTimeEntry.Id = dbTimeEntry.Id; 

            await _context.SaveChangesAsync();

            return dbTimeEntry;
        }

        public async Task<List<TimeEntry>> GetAllTimeEntries()
        {
            return await _context.TimeEntries.ToListAsync();
        }

        public async Task<List<TimeEntry>> GetDailyTimeEntries()
        {
            var todayUtc = DateTime.UtcNow.Date; // Start of today in UTC
            var tomorrowUtc = todayUtc.AddDays(1); // Start of tomorrow in UTC

            return await _context.TimeEntries
                .Where(t => t.DateTime >= todayUtc && t.DateTime < tomorrowUtc) // Filter entries within today's UTC range
                .ToListAsync();
        }

        public async Task<TimeEntry> GetTimeEntry(int id)
        {
            var timeEntry = await _context.TimeEntries.Where(t => t.Id == id).FirstOrDefaultAsync();

            return timeEntry;
        }
    }
}
