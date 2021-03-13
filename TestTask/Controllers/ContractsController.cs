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
            //первоначальная инициализация бд
            db = context;
            if (!db.Contracts.Any())
            {
                Contract contract_1 = new Contract { Code = "111-ПР-001", Name = "Реконструкция ПС-110 кВ", Owner = "Сибур" };
                Contract contract_2 = new Contract { Code = "222-ПР-002", Name = "Техническое перевооружение РВС", Owner = "АО Самаранефтегаз" };
                db.Contracts.AddRange(contract_1,contract_2);
                Stage stage_1 = new Stage { Name = "Согласование ОПР", StartTime = DateTime.Now.ToShortDateString(), EndTime = new DateTime(2021, 03, 20).ToShortDateString(), Contract = contract_1 };
                Stage stage_2 = new Stage { Name = "Запрос ТЗ", StartTime = DateTime.Now.ToShortDateString(), EndTime = new DateTime(2021, 05, 10).ToShortDateString(), Contract = contract_2 };
                db.Stages.AddRange(stage_1,stage_2);
                db.SaveChanges();
                
            }
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
