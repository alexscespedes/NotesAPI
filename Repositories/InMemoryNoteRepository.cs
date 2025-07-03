using System;
using NotesAPI.Models;

namespace NotesAPI.Repositories;

public class InMemoryNoteRepository : INoteRepository
{
    private readonly List<Note> _notes = new();
    private int _nextId = 1;
    public void Add(Note note)
    {
        note.Id = _nextId++;
        note.CreatedAt = DateTime.UtcNow;
        _notes.Add(note);
    }

    public void Delete(int id)
    {
        var note = GetById(id);

        if (note != null) _notes.Remove(note);
    }

    public bool Exists(int id) => _notes.Any(n => n.Id == id);

    public IEnumerable<Note> GetAll(string? search = null)
    {
        if (string.IsNullOrWhiteSpace(search))
            return _notes;

        var keyword = search.Trim().ToLower();
        return _notes.Where(n => (n.Title?.ToLower().Contains(keyword) ?? false) ||
        (n.Content?.ToLower().Contains(keyword) ?? false));
    }

    public Note? GetById(int id) => _notes.FirstOrDefault(n => n.Id == id);

    public void Update(Note note)
    {
        var existing = GetById(note.Id);
        if (existing == null) return;

        existing.Title = note.Title;
        existing.Content = note.Content;
    }
}
