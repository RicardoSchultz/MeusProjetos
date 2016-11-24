using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ricardoschultz
{
    class teste
    {

        public string changeDate(string date, char op, long value)
        {

            int nHoras = (int)value / 60;
            int rHoras = (int)value % 60;
            int nDias = nHoras / 24;
            int nMeses = (int)value / 24;


            //int total_minutes = 4000;
            //int minutes = total_minutes % 60;
            //int total_hours = total_minutes / 60;
            //int horas = total_hours % 24;
            //int total_days = total_hours / 24;
            //int dias = total_days % 365;
            //int anos = total_days / 365;

            //Console.WriteLine(" "+ anos, dias, horas, minutes);

            DateTime dateValue = new DateTime(2016, 11, 17, 14, 0, 0);

            //double[] minutes = { 4000 };
            long minutes = 4000;

            Console.WriteLine(dateValue.AddMinutes(minutes));

            Console.ReadKey();
            return date;
        }
    }
}



