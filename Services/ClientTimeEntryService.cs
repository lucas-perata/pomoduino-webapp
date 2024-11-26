using Microsoft.EntityFrameworkCore;
using TimerApp.Data;
using TimerApp.Entity;

namespace TimerApp.Services
{
    public class ClientTimeEntryService : ITimeEntryService
    {
        private readonly HttpClient _httpClient;
        private readonly DataContext _context; 

        public ClientTimeEntryService(DataContext context, HttpClient httpClient)
        {
            _httpClient = httpClient;  
            _context = context;
        }

        public async Task<TimeEntry> AddTimeEntry(TimeEntry timeEntry)
        {
            if (timeEntry is null)
            {
                throw new ArgumentNullException(nameof(timeEntry));
            }

            timeEntry.DateTime = DateTime.UtcNow;

            var response = await _httpClient.PostAsJsonAsync("api/timeentry", timeEntry);
            
            return await response.Content.ReadFromJsonAsync<TimeEntry>();
            
         }
        

        public async Task<bool> DeleteTimeEntry(int id)
        {
            if(id < 0)
            {
                throw new ArgumentException("Id inválido");
            }
            var result = await _httpClient.DeleteAsync($"api/timeentry/{id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<TimeEntry> EditTimeEntry(int id, TimeEntry timeEntry)
        {
            if (timeEntry is null)
            {
                throw new ArgumentNullException(nameof(timeEntry));
            }

            var response = await _httpClient.PutAsJsonAsync($"api/timeentry/{id}", timeEntry);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<TimeEntry>();
            }
            else
            {
                // Manejar el error de la solicitud
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error al editar la entrada de tiempo: {response.ReasonPhrase}, Detalles: {errorContent}");
            }
        }
        public async Task<List<TimeEntry>> GetAllTimeEntries()
        {
            var result = await _httpClient.GetAsync($"api/timeentry"); 
            
            return await result.Content.ReadFromJsonAsync<List<TimeEntry>>();
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
