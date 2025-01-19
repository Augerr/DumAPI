using Microsoft.AspNetCore.Mvc;
using DumAPI.Persistence.Models;
using DumAPI.Persistence.Services;
using DumAPI.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace DumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DummyUsersController(UserService service) : ControllerBase
    {
        private readonly UserService _service = service;

        // GET: api/DummyUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DummyProfileDTO>>> GetUsers()
        {
            return Ok(await _service.GetAll());
        }

        // GET: api/DummyUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DummyProfileDTO>> GetDummyUserProfile(int id)
        {
            var dummyUser = await _service.Get(id);

            if (dummyUser == null)
            {
                return Ok("Object not found");
            }

            return Ok(dummyUser);
        }

        // GET: api/DummyUsers/5
        [HttpGet("{id}/Details")]
        public async Task<ActionResult<DummyDetailsDTO>> GetDummyUserDetails(int id)
        {
            var dummyUser = await _service.Get(id);

            if (dummyUser == null)
            {
                return Ok("Object not found");
            }

            return Ok(dummyUser);
        }

        // GET: api/DummyUsers/{id}/Rights
        [HttpGet("{id}/Rights")]
        public async Task<ActionResult<ICollection<DummyUserRight>>> GetDummyUserRights(int id)
        {
            var dummyUserRights = await _service.GetUserRights(id);

            if (dummyUserRights == null)
            {
                return Ok("Object not found");
            }

            return Ok(dummyUserRights);
        }

        // PUT: api/DummyUsers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDummyUser(int id, DummyUser dummyUser)
        {
            if (dummyUser == null || id != dummyUser.Id)
            {
                return Ok("Wrong user id sent");
            }

            DummyUser? updatedUser = await _service.Update(dummyUser);

            return updatedUser != null ? Ok(updatedUser) : Ok("Could not update user");
        }

        // POST: api/DummyUsers
        [HttpPost]
        public async Task<ActionResult<DummyUser>> PostDummyUser(DummyUser dummyUser)
        {
            try
            {
                await _service.Add(dummyUser);
            } catch (EntityExistsException<DummyUser> e)
            {
                return Ok(e.Message);
            }

            return CreatedAtAction("GetDummyUser", new { id = dummyUser.Id }, dummyUser);
        }

        // DELETE: api/DummyUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDummyUser(int id)
        {
            
            bool entityRemoved = await _service.Remove(id);

            return entityRemoved ? Ok("Success") : Ok("Failure");
        }
    }
}
