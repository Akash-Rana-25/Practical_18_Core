using Microsoft.EntityFrameworkCore;
using Practical_18_API.Context;
using Practical_18_API.Entities;
using Practical_18_API.Repositories;
using Practical_18_API.Services;

namespace Practical_18_API.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public StudentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateStudent(Student student)
    {
        await _dbContext.Students.AddAsync(student);
    }

    public void DeleteStudent(Student student)
    {
        _dbContext.Students.Remove(student);
    }

    public async Task<Student?> GetStudentByIdAsync(Guid id)
    {
        return await _dbContext.Students.FirstOrDefaultAsync(student => student.Id == id);
    }

    public async Task<IEnumerable<Student>> GetStudentsAsync()
    {
        return await _dbContext.Students.ToListAsync();
    }

    public async Task<(IEnumerable<Student>, PaginationMetaData)> GetStudentsAsync(string? name, string? searchQuery, int pageNumber, int pageSize)
    {
        var collection = _dbContext.Students as IQueryable<Student>;

        if (!string.IsNullOrWhiteSpace(name))
        {
            name = name.Trim();
            collection = collection.Where(student => student.FirstName == name || student.LastName == name);
        }
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            searchQuery = searchQuery.Trim();
            collection = collection.Where(student =>
                (student.FirstName != null && student.FirstName.Contains(searchQuery)) ||
                (student.LastName != null && student.LastName.Contains(searchQuery)) ||
                (student.Email != null && student.Email.Contains(searchQuery)) ||
                (student.MobileNumber != null && student.MobileNumber.Contains(searchQuery)) ||
                (student.Address != null && student.Address.Contains(searchQuery))
            );
        }

        var totalItemCount = await collection.CountAsync();
        var paginationMetadata = new PaginationMetaData(totalItemCount, pageSize, pageNumber);

        var collectionToReturn = await collection.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToListAsync();

        return (collectionToReturn, paginationMetadata);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return (await _dbContext.SaveChangesAsync() >= 0);
    }
}
