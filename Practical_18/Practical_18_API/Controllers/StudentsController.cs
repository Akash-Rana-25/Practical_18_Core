using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Practical_18_API.Entities;
using Practical_18_API.Models;
using Practical_18_API.Repositories;

namespace Practical_18_API.Controllers;

[ApiController]
[Route("api/v1/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public StudentsController(IStudentRepository studentRepository, IMapper mapper)
	{
        _studentRepository = studentRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetStudentsAsync()
    {
        var students = await _studentRepository.GetStudentsAsync();
        return Ok(_mapper.Map<IEnumerable<StudentDto>>(students));
    }

    [HttpGet("{id:guid}", Name = "GetStudentByIdAsync")]
    public async Task<IActionResult> GetStudentByIdAsync(Guid id)
    {
        var student = await _studentRepository.GetStudentByIdAsync(id);
        if (student is null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<StudentDto>(student));
    }

    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudentAsync(CreateStudentDto createStudent)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        var studentToInsert = _mapper.Map<Student>(createStudent);
        await _studentRepository.CreateStudent(studentToInsert);
        await _studentRepository.SaveChangesAsync();

        var createdCityToReturn = _mapper.Map<StudentDto>(studentToInsert);

        return CreatedAtRoute("GetStudentByIdAsync", new { id = createdCityToReturn.Id }, createdCityToReturn);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<StudentDto>> UpdateStudentAsync(Guid id, UpdateStudentDto updateStudent)
    {
        var studentEntity = await _studentRepository.GetStudentByIdAsync(id);
        if (studentEntity is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        _mapper.Map(updateStudent, studentEntity);
        await _studentRepository.SaveChangesAsync();
        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public async Task<ActionResult<StudentDto>> UpdateStudentAsync(Guid id, JsonPatchDocument<UpdateStudentDto> patchDocument)
    {
        var studentEntity = await _studentRepository.GetStudentByIdAsync(id);
        if (studentEntity is null)
        {
            return NotFound();
        }

        var studentToPatch = _mapper.Map<UpdateStudentDto>(studentEntity);
        patchDocument.ApplyTo(studentToPatch);

        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }
        if (!TryValidateModel(studentToPatch))
        {
            return UnprocessableEntity(ModelState);
        }

        _mapper.Map(studentToPatch, studentEntity);
        await _studentRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<StudentDto>> DeleteStudentAsync(Guid id)
    {
        var studentEntity = await _studentRepository.GetStudentByIdAsync(id);
        if (studentEntity is null)
        {
            return NotFound();
        }
        _studentRepository.DeleteStudent(studentEntity);
        await _studentRepository.SaveChangesAsync();
        return NoContent();
    }
}
