using ApiDevelopment.Context;
using ApiDevelopment.Entity;
using ApiDevelopment.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDevelopment.Controllers
{
    [Route("api/v0/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : ControllerBase
    {
        public readonly PostgreSQLDataContext _context;
        public StudentController(PostgreSQLDataContext context)
        {
            _context = context;
        }

        [HttpPost("StudentRecordUpload")]
        public ActionResult PostFile(StudentModel studentModel)
        {
            if (studentModel == null)
            {
                return BadRequest();
            }
            Student data = new Student
            {
                Name = studentModel.Name,
                Age = studentModel.Age,
                Address = studentModel.Address,
                Section = studentModel.Section,
            };
            var response = _context.students.Add(data);
            _context.SaveChanges();
            int newId = _context.students.Max(x => x.Id);
            var result = _context.students.Find(newId);
            return Ok(result);
        }

        [HttpGet("StudentList")]        
        public IEnumerable<Student> GetStudents() {

            return _context.students.ToList();
        }
     
        [HttpGet("GetStudentById")]
        public Student GetStudentById(int id)
        {
            var result =  _context.students.Where(i=> i.Id== id).FirstOrDefault();
            return result;
        }

        [HttpPut("UpdateStudent")]
        public IActionResult PutStudent(Student student) {
            if (student != null)
            {
                if(student.Section!=null && student.Name!=null && student.Address!=null && student.Age!=null) {
                    _context.students.Update(student);
                    _context.SaveChanges();
                }
            }
            return Ok(new
            {
                message = "data updated successfully"
            });
        }

        [HttpGet("PaginationRecord")]
        public PaginatedResponse<IEnumerable<Student>> GetStudentPagination([FromQuery] PagingModel model) 
        {
            var response = new PaginatedResponse<IEnumerable<Student>>();
            var userQuery = _context.students.AsQueryable().Select(c=> new Student
            {
                Id= c.Id,
                Name= c.Name,
                Address= c.Address,
                Age= c.Age,
                Section= c.Section,
            });
            if (!string.IsNullOrEmpty(model.Query))
            {
                userQuery = userQuery.Where(u=> u.Address.ToUpper().Contains(model.Query.ToUpper()) ||
                u.Name.ToUpper().Contains(model.Query.ToUpper()) ||
                u.Section.ToUpper().Contains(model.Query.ToUpper()) ||
                u.Age.ToString().Contains(model.Query.ToString()));
            }
            response.TotalRecords = userQuery.Count();
            response.Data = userQuery.Skip((model.PageNumber -1) * model.PageSize)
                .Take(model.PageSize).Select(s => new Student 
                 { 
                     Id=s.Id,
                     Name= s.Name,
                     Address= s.Address,
                     Age= s.Age,
                     Section= s.Section,
                 }).ToList();
            return response;
        }

        [HttpPut("FileUpdate")]
        public IActionResult UploadFile(int id, IFormFile file)
        {
            var repoData = _context.students.Where(x => x.Id == id).FirstOrDefault();
            if(file!= null)
            {
                using(var memorySteam = new MemoryStream())
                {
                    file.CopyTo(memorySteam);
                    repoData.Image = memorySteam.ToArray();
                    _context.students.Update(repoData);
                    _context.SaveChanges();
                }
            }
            return Ok();
        }

        [HttpDelete("DeleteStudent")]
        public IActionResult DeleteStudent(int id)
        {
            var result = _context.students.Where(obj => obj.Id == id).FirstOrDefault();
            _context.students.Remove(result);
            _context.SaveChanges();
            return Ok(new
            {
                message = "Row deleted successfully"
            });
        }

    }
}
