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
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace _1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutomobileController : ControllerBase
    {
        private IAutomobileData automobileData;
        private IAutomobileDomain automobileDomain;
        private IUserData _userData;
        private IMapper _mapper;

        public AutomobileController(IAutomobileData automobileData, IAutomobileDomain automobileDomain, IMapper mapper
        , IUserData userData)
        {
            this.automobileData = automobileData;
            this.automobileDomain = automobileDomain;
            this._mapper = mapper;
            this._userData = userData;
        }
        // GET: api/Automobile
        [HttpGet("search-car/getAll")]
        public Task<List<Automobile>> Get()
        {
            return automobileData.GetAllAsync();
        }

        // // GET: api/Automobile/userid/automovileid
        // [HttpGet("userid/automovileid")]
        // public Automobile Get(int id)
        // {
        //     return automobileData.GetById(id);
        // }
        
        // GET: api/Automobile/userid/automovileid
        [HttpGet("userid/automovileid")]
        public async Task<IActionResult> Get(int id, int userid)
        {
            var result = await automobileData.GetByUserAutomobile(id, userid);
    
            if (result != null)
            {
                return Ok(result); // Devuelve un resultado 200 OK
            }
    
            return NotFound(); // Devuelve un resultado 404 Not Found u otro resultado apropiado si los datos no se encuentran
        }
        
         
        // GET: api/Automobile/search
        [HttpGet(  "search-car/getfilter")]
        public IActionResult Get(int id,string Brand,string Model)
        {
            Automobile automobile = automobileData.GetBySearch(id,Brand,Model);
            if (automobile == null)
            {
                return NotFound(); 
            }

            SearchAutomovilFilterResponse searchAutomovilFilterResponse =
                _mapper.Map<SearchAutomovilFilterResponse>(automobile);
            return Ok(searchAutomovilFilterResponse);
        }

        
        // POST: api/Automobile
        [HttpPost("register")]
        public IActionResult Post([FromBody] AutomobileCreateRequest value)
        {
            var usuario = _userData.GetById(value.UserId);
            var automobile = _mapper.Map<AutomobileCreateRequest,Automobile>(value);
            automobile.IsAvailable = true;
            return Ok(automobileDomain.Create(automobile, value.UserId));
        }
        
        // DELETE: api/Automobile/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(automobileData.Delete(id));
        }
    }
}
