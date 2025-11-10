// THIS IS THE CODE FOR THE ASP.NET CORE API CRUD OPERATION USING SWAGGER OPERATION.
using APICRUD.DbContext;
using APICRUD.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICRUD.Controllers
{
    // This will goes to like --> Localhost:xxxx/api/student
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // this is dependency injection of dbcontext and this is constructor injection
        private readonly ApplicationDbContext dbContext;
        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult getAllStudent()
        {
            var allStudent = dbContext.Students.ToList();
            return Ok(allStudent);
        }
        // This GET method is for getting student by id
        [HttpGet]
        [Route("{id:guid}")]
        // This route id must be same as the parameter id
        public IActionResult getStudentById(Guid id)
        {
            var getStudentId = dbContext.Students.Find(id);
            if (getStudentId is null)
            {
                return NotFound("This is an Invalid Id");
            }
            return Ok(getStudentId);
        }
        [HttpPost]
        public IActionResult CreateStudent(AddStudentDto StudentDto)
        {
            var studentData = new Student()
            {
                Id=Guid.NewGuid(),
                Name = StudentDto.Name,
                Email = StudentDto.Email,
                Password = StudentDto.Password,
                Age = StudentDto.Age
            };
            dbContext.Students.Add(studentData);
            dbContext.SaveChanges();
            return Ok(studentData);
        }
        // Now we will create HttpPut method for updating student
        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateStudentbyId(Guid id, UpdateStudentDTO updateStudent)
        {
            var findStudent = dbContext.Students.Find(id);
            if (findStudent is null)
            {
                return NotFound("This is an Invalid Id");
            }
            findStudent.Name = updateStudent.Name;
            findStudent.Email = updateStudent.Email;
            findStudent.Password = updateStudent.Password;
            findStudent.Age = updateStudent.Age;
            dbContext.SaveChanges();
            return Ok(findStudent);
        }
        // Now This will Be for the Delete Method 
        [HttpDelete]
        [Route("{id:guid}")]  // Make sure the route id is same as parameter id
        public IActionResult DeleteStudentbyId(Guid id)
        {
            var findStudent = dbContext.Students.Find(id);
            if(findStudent is null)
            {
                return NotFound("This is an Invalid Id");
            }
            dbContext.Students.Remove(findStudent);
            dbContext.SaveChanges();
            return Ok(findStudent);
        }

    }
}
