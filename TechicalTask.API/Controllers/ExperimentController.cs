using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TechicalTask.API.Classes;
using TechicalTask.API.Data;


namespace TechicalTask.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentController : ControllerBase
    {
        private readonly ClientsDBContext _clientsDBContext;
        public ExperimentController(ClientsDBContext clientsDBContext)
        {
            _clientsDBContext = clientsDBContext;
            int i = 0;
            string str = i.ToString();
        }

        [HttpGet]
        public async Task<ActionResult<List<Experiment>>> Select()
        {
            List<Experiment> select = await _clientsDBContext.Experiments.ToListAsync();
            return Ok(select);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Experiment>> GetById(int Id)
        {
            Experiment Experiment = await _clientsDBContext.Experiments.FirstOrDefaultAsync(e => e.Id == Id);
            return Ok(Experiment);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Create(Experiment experiment)
        {
            if (experiment != null) await _clientsDBContext.Experiments.AddAsync(experiment);
            await _clientsDBContext.SaveChangesAsync();
            return experiment != null ? true : false;
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete(int Id)
        {
            Experiment experiment = await _clientsDBContext.Experiments.FirstOrDefaultAsync(e => e.Id == Id);
            if (experiment != null) _clientsDBContext.Experiments.Remove(experiment);
            await _clientsDBContext.SaveChangesAsync();
            return experiment != null ? true : false;
        }
        [HttpPut]
        public async Task<ActionResult<bool>> Update(Experiment experiment)
        {
            Experiment entity = await _clientsDBContext.Experiments.FirstOrDefaultAsync(e => e.Id == experiment.Id);
            if (entity != null) _clientsDBContext.Experiments.Remove(experiment);
            await _clientsDBContext.SaveChangesAsync();
            return entity != null ? true : false;
        }
    }
}
