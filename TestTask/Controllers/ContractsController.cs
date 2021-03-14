using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTask.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using ClosedXML.Excel;

namespace TestTask.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContractsController : ControllerBase
    {

        ContractsContext db;

        public ContractsController(ContractsContext context)
        {
            db = context;
        }

        [HttpGet]//список контрактов в json
        public async Task<ActionResult<IEnumerable<Contract>>> GetContracts()
        {
            return await db.Contracts.ToListAsync();
        }


        [HttpGet("{id}")]//список этапов контракта в json
        public async Task<ActionResult<IEnumerable<Stage>>> GetContractStages(int id)
        {
            return await db.Stages.Where(x=>x.ContractId==id).ToListAsync();
        }
        

    }
}
