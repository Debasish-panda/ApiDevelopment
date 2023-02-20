using ApiDevelopment.Context;
using ApiDevelopment.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDevelopment.Controllers
{
    [Route("api/v0/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeController : Controller
    {
        public readonly PostgreSQLDataContext _context;
        public EmployeController(PostgreSQLDataContext context)
        {
            _context = context;
        }
        [HttpPost("SaveEmployeData")]
        public ActionResult PostEmploye(Employe employe)
        {
            if(employe == null)
            {
                return BadRequest();
            }
            _context.employe.Add(employe);
            _context.SaveChanges();
            return Ok();
        }
        [HttpGet("GetAllEmploye")]
        public IEnumerable<Employe> GetEmployeList()
        {
            var emp = _context.employe.ToList();
            return emp;
        }

    }
}
