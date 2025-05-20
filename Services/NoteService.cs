using Gespraechsnotiz_App.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gespraechsnotiz_App.Services
{
    public class NoteService
    {
        private SQLiteAsyncConnection _database;

        public NoteService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Note>().Wait();
        }

        public Task<List<Note>> GetNotesAsync() => _database.Table<Note>().ToListAsync();

        public Task<Note> GetNoteAsync(int id) => _database.Table<Note>().Where(n => n.Id == id).FirstOrDefaultAsync();

        public Task<int> SaveNoteAsync(Note note) => note.Id != 0 ? _database.UpdateAsync(note) : _database.InsertAsync(note);

        public Task<int> DeleteNoteAsync(Note note) => _database.DeleteAsync(note);
    }
}
