using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class Stage
    {
        public int Id { get; set; } 

        public string Name { get; set; }//наименование этапа договора

        public string StartTime { get; set; }//дата начала договора

        public string EndTime { get; set; }//дата окончания договора

        public int ContractId { get; set; }

        public Contract Contract { get; set; }// навигационное свойство,т.к. у договора может быть несколько этапов
    }
}
