using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _1.API.Request;
using _1.API.Response;
using _2.Domain;
using _3.Data.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private IUserData _tuserData;
        private IUserDomain _tUserDomain;
        private IMapper _mapper;

        // Constructor injection of necessary services and dependencies
        public ProfileController(IUserData userData, IUserDomain userDomain, IMapper mapper)
        {
            _tuserData = userData;
            _tUserDomain = userDomain;
            _mapper = mapper;
        }
        
        // GET: api/profile/id
        /// <summary>
        /// Retrieves a user profile by ID.
        /// </summary>
        /// <response code="200">Returns the found profile</response>
        /// <response code="404">Profile is null</response>
        [HttpGet("{id}", Name = "Get")]
        [Produces("application/json")]
        public IActionResult Get(int id)
        {
            // Retrieve user data based on the provided ID
            User user = _tuserData.GetById(id);
                
            // Check if the user exists
            if (user == null)
            {
                return NotFound(); 
            }
            // Map the user to a specific profile response based on the user type
            if(user.UserType == UserType.Arrendatario)
            {
                ProfileResponseOwner profileResponseOwner = _mapper.Map<ProfileResponseOwner>(user);
                return Ok(profileResponseOwner);
            }
            else
            {
                ProfileResponseOwner profileResponseOwner = _mapper.Map<ProfileResponseOwner>(user);
                return Ok(profileResponseOwner);
            }
        }

        // PUT: api/profile/id
        /// <summary>
        /// Updates a user profile by ID.
        /// </summary>
        /// <response code="200">Profile update</response>
        [HttpPut("{id}")]
        public bool Put(int id, [FromBody] ProfileUpdateRequest request)
        {
            // Map the update request data to the User model
            var users = _mapper.Map<ProfileUpdateRequest, User>(request);
           
            // Update the user profile with the new data
            return _tUserDomain.Update(users, id);
        }
        
    }
}
