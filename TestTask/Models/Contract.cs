using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class Contract
    {
        public int Id { get; set; } 

        public string Code { get; set; }//шифр договора

        public string Name { get; set; }//наименование договора

        public string Owner { get; set; }//заказчик

        public List<Stage> Stages { get; set; } = new List<Stage>();//у договора может быть несколько этапов

    }
}
