using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ricardoschultz
{
    class alteraData
    {
        public string changeDate(string date, char op, long value)
        {

            char[] delimiter = new char[] { '/', ' ', ':' };

            string[] array = date.Split(delimiter,
                    StringSplitOptions.None);

            foreach (string separar in array)
            {
            }
            
            long cAno = Convert.ToInt32(array[2]);
            long cMes = Convert.ToInt32(array[1]);
            long cDia = Convert.ToInt32(array[0]);
            long cHora = Convert.ToInt32(array[3]);
            long cMinuto = Convert.ToInt32(array[4]);

            long resto = 0;
            long nAnos, nMeses, nDias, nHoras;
            long rAnos, rMeses, rDias, rHoras, rMinutos;


            resto = value % 60;      

            nHoras = value / 60;
            nDias = nHoras / 24;
            nMeses = nDias / 30;            
            nAnos = nMeses / 12;
            

            if (op != '+' && op != '-')
            {
                Console.Write("Caractere Inválido!!");
            }
            else if (op == '+')
            {
                var contResto = resto + cMinuto; 

                //if(contResto > 60)
                //{
                //    cMinuto = contResto - 60;
                //    if(cMinuto < 10)
                //    {
                //        Console.WriteLine("Resultado: {0}/{1}/{2} {3}:0{4}", cDia, cMes, cAno, cHora, cMinuto);
                //    }
                //    cHora += 1;
                //}

                // 4000 minutos = 2 Dias 18 Horas 40 minutos
                // FIM
            }
            else
            {
 
            }
 
            //Console.WriteLine("Resultado: {0}/{1}/{2} {3}:{4}", cDia, cMes, cAno, cHora, cMinuto);

            date = cDia + "/" + cMes + "/" + cAno + " " + cHora + ":" + cMinuto;

            Console.ReadKey();
            return date;
        }
    }
}
