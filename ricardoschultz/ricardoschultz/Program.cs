using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ricardoschultz
{
    class Program
    {
        static void Main(string[] args)
        {
            //AvaliacaoCWI data = new AvaliacaoCWI();
            alteraData data = new alteraData();
            data.changeDate("17/11/2016 12:10", '+', 4000);

        }
    }
}


