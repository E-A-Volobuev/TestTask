using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestTask.Models;

namespace TestTask.Controllers
{
    
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ImportController : ControllerBase
    {
        ContractsContext db;
        IWebHostEnvironment _appEnvironment;
        public ImportController(ContractsContext context, IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            db = context;
        }
        public void CreateFile(IFormFile fileExcel)//копирование выбранного файла в папку с программой
        {
            var fileName = System.IO.Path.GetFileName(fileExcel.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files", fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileExcel.CopyTo(fileStream);
            }
        }
        [HttpPost]//выбор файла excel c информацией об этапах договора и загрузка в бд
        public string AddFileStage(IFormFile fileExcel)
        {
            if (fileExcel != null && fileExcel.Length > 0)
            {
                CreateFile(fileExcel);
            }
            if (fileExcel != null)
            {
                // путь к папке Files
                string path = "/Files/" + fileExcel.FileName;
                string map = "";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    map = _appEnvironment.WebRootPath + path;
                    fileExcel.CopyTo(fileStream);
                }
                // начало использования библиотеке ClosedXML
                var workbook = new XLWorkbook(map);
                var worksheet = workbook.Worksheet(1);
                // получим все строки в файле
                var rows = worksheet.RangeUsed().RowsUsed();
                // пример чтения строк файла.
                foreach (var row in rows)
                {
                    Stage stage = new Stage { Name = row.Cell(1).Value.ToString(), StartTime = row.Cell(2).Value.ToString(), EndTime = row.Cell(3).Value.ToString(), ContractId = Convert.ToInt32(row.Cell(4).Value.ToString()) };
                    db.Stages.Add(stage);
                }

                db.SaveChanges();
            }
            return "Данные этапов договоров загружены в БД";

        }
        [HttpPost]//выбор файла excel c информацией о договорах и загрузка в бд
        public string AddFileContract(IFormFile fileExcel)
        {
            if (fileExcel != null && fileExcel.Length > 0)
            {
                CreateFile(fileExcel);
            }
            if (fileExcel != null)
            {
                // путь к папке Files
                string path = "/Files/" + fileExcel.FileName;
                string map = "";
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    map = _appEnvironment.WebRootPath + path;
                    fileExcel.CopyTo(fileStream);
                }
                // начало использования библиотеке ClosedXML
                var workbook = new XLWorkbook(map);
                var worksheet = workbook.Worksheet(1);
                // получим все строки в файле
                var rows = worksheet.RangeUsed().RowsUsed();
                // пример чтения строк файла.
                foreach (var row in rows)
                {
                    Contract contract = new Contract { Code = row.Cell(1).Value.ToString(), Name = row.Cell(2).Value.ToString(), Owner = row.Cell(3).Value.ToString() };
                    db.Contracts.Add(contract);
                }

                db.SaveChanges();
            }
            return "Данные о договорах загружены в БД";

        }

    }
}
