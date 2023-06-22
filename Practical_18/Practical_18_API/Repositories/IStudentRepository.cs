using Practical_18_API.Entities;
using Practical_18_API.Services;

namespace Practical_18_API.Repositories;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetStudentsAsync();
    public Task<(IEnumerable<Student>, PaginationMetaData)> GetStudentsAsync(string? name, string? searchQuery, int pageNumber, int pageSize);
    Task<Student?> GetStudentByIdAsync(Guid id);
    public Task CreateStudent(Student student);
    public void DeleteStudent(Student student);
    public Task<bool> SaveChangesAsync();
}
