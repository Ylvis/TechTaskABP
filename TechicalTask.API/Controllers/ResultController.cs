using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;
using TechicalTask.API.Classes;
using TechicalTask.API.Data;

namespace TechicalTask.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ClientsDBContext _clientsDBContext;
        public ResultController(ClientsDBContext clientsDBContext)
        {
            _clientsDBContext = clientsDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Result>>> Select()
        {
            List<Result> select = await _clientsDBContext.Results.ToListAsync();
            return Ok(select);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Result>> GetById(int Id)
        {
            Result result = await _clientsDBContext.Results.FirstOrDefaultAsync(e => e.Id == Id);
            return Ok(result);
        }

        [HttpGet("{Token}")]
        public async Task<ActionResult<Result>> GetByToken(string token)
        {
            Result result = await _clientsDBContext.Results.FirstOrDefaultAsync(e => e.deviceToken == token);
            return Ok(result);
        }

        [HttpGet("{value}")]
        public async Task<ActionResult<List<Result>>> GetByValue(string value)
        {
            List<Result> result = await _clientsDBContext.Results.Where(e => e.value == value).ToListAsync();
            return Ok(result);
        }

        [HttpGet("{x_name}")]
        public async Task<ActionResult<List<Result>>> GetByXName(string x_name)
        {
            List<Result> result = await _clientsDBContext.Results.Where(e => e.X_Name == x_name).ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create(Result experiment)
        {
            if (experiment != null) await _clientsDBContext.Results.AddAsync(experiment);
            await _clientsDBContext.SaveChangesAsync();
            return experiment != null ? true : false;
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            Result experiment = await _clientsDBContext.Results.FirstOrDefaultAsync(e => e.Id == Id);
            if (experiment != null) _clientsDBContext.Results.Remove(experiment);
            await _clientsDBContext.SaveChangesAsync();
            return experiment != null ? true : false;
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Clear()
        {
            List<Result> list = _clientsDBContext.Results.ToList();
            foreach (var entity in list) _clientsDBContext.Results.Remove(entity);
            await _clientsDBContext.SaveChangesAsync();
            return true;
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update(Result experiment)
        {
            Result entity = await _clientsDBContext.Results.FirstOrDefaultAsync(e => e.Id == experiment.Id);
            if (entity != null) _clientsDBContext.Results.Remove(experiment);
            await _clientsDBContext.SaveChangesAsync();
            return entity != null ? true : false;
        }

        

        [HttpGet]
        public async Task<ActionResult<string>> PassExperiment1([FromQuery] string hash, [FromQuery] int id, [FromQuery] string button_color)
        {
            Result entity = new Result();
            Experiment experiment = await _clientsDBContext.Experiments.FirstOrDefaultAsync(e => e.Id == id);
            string token = null;
            Result tempResult = new Result();
            if (_clientsDBContext.Results.FirstOrDefault(e => e.deviceToken == hash) != null)
                tempResult = await _clientsDBContext.Results.FirstOrDefaultAsync(e => e.deviceToken == hash && e.X_Name == experiment.Name);
            if (tempResult != null)
            {
                token = tempResult.deviceToken;
            }
            else
            {
                token = hash;
                tempResult = new Result { deviceToken = hash};               
            }
            var list = await _clientsDBContext.Results.Where(e => e.deviceToken == hash).ToListAsync();
            if (experiment != null)
            {
                if ((token == null || (token != null && tempResult.X_Name != experiment.Name && list.Count < 2)) && button_color != "null")
                {
                    //code of generation result
                    Random random = new Random();
                    var number = 0;
                    switch (experiment.Name)
                    {
                        case "button_color":
                            {
                                entity.value = "#" + button_color;
                                break;
                            }
                        default: break;
                    }
                    entity.X_Name = experiment.Name;
                    entity.deviceToken = hash;
                    await _clientsDBContext.AddAsync(entity);
                    await _clientsDBContext.SaveChangesAsync();
                }
                else
                {
                    return Ok(tempResult.value);
                }
            }
            
            return Ok(entity.value);
        }
        
        [HttpGet]
        public async Task<ActionResult<string>> PassExperiment2([FromQuery] string hash, [FromQuery] int id)
        {
            Result entity = new Result();
            Experiment experiment = await _clientsDBContext.Experiments.FirstOrDefaultAsync(e => e.Id == id);
            string token = null;
            Result tempResult = new Result();
            if (_clientsDBContext.Results.FirstOrDefault(e => e.deviceToken == hash) != null)
                tempResult = await _clientsDBContext.Results.FirstOrDefaultAsync(e => e.deviceToken == hash && e.X_Name == experiment.Name);
            if (tempResult != null)
            {
                token = tempResult.deviceToken;
            }
            else
            {
                token = hash;
                tempResult.deviceToken = token;
            }
            var list = await _clientsDBContext.Results.Where(e => e.deviceToken == hash).ToListAsync();
            if (experiment != null)
            {
                if (token == null || (token != null && tempResult.X_Name != experiment.Name && list.Count < 2))
                {
                    //code of generation result
                    Random random = new Random();
                    var number = 0;
                    switch (experiment.Name)
                    {
                        case "price":
                            {
                                number = random.Next(0, 99);
                                if (number < 75)
                                    entity.value = 10.ToString();
                                else if (number >= 75 && number < 85)
                                    entity.value = 20.ToString();
                                else if (number >= 85 && number < 90)
                                    entity.value = 50.ToString();
                                else if (number >= 90)
                                    entity.value = 5.ToString();
                                break;
                            }
                        default: break;
                    }
                    entity.X_Name = experiment.Name;
                    entity.deviceToken = hash;
                    await _clientsDBContext.AddAsync(entity);
                    await _clientsDBContext.SaveChangesAsync();
                }
                else
                {
                    return Ok(tempResult.value);
                }
            }
            return Ok(entity.value);
        }
    }
}

