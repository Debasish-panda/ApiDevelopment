using ApiDevelopment.Context;
using ApiDevelopment.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDevelopment.Controllers
{
    [ApiController]    
    public class RegistrationController : ControllerBase
    {
        public readonly PostgreSQLDataContext _context;
        public RegistrationController(PostgreSQLDataContext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("Registrer")]
        public ActionResult<Registration> PostUser(Registration registration)
        {
            if(registration != null)
            {
                _context.registrations.Add(registration);
                _context.SaveChanges();
            }
            return Ok();
        }
    }
}
