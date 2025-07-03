using System;
using NotesAPI.Models;

namespace NotesAPI.Repositories;

public interface INoteRepository
{
    IEnumerable<Note> GetAll(string? search = null);
    Note? GetById(int id);
    void Add(Note note);
    void Update(Note note);
    void Delete(int id);
    bool Exists(int id);
}
