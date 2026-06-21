using System;

namespace gnericList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyList<string> oList = new MyList<string>();

            Console.WriteLine(oList.Count);
            Console.WriteLine(oList.Capacity);
            oList.Add("yousef");
            oList.Add("mo");
            oList.Add("Ah");
            oList.Add("Ali");
            oList.Add("dd");
            oList.Add("jjj");


            //oList[0] = "yousef";
            //oList[1] = "mohamed";
            //oList[2] = "Ahmed";
            //oList[100] = "Ahmed";

            //Console.WriteLine(oList.Count);
            //Console.WriteLine(oList.Capacity);
            //List<string> lst = oList["0,1,2"];

            //oList.RemoveAt(1);
            //oList.RemoveAt(0);
            //oList.RemoveAt(5);

            //oList.IndexOf("Ah");
            //oList.IndexOf("jjj");
            //oList.IndexOf("yousef");

            //oList.Remove("yousef");
            //oList.Remove("dd");
            //oList.Remove("jjj");
            string[] c = { "yyy", "fff", "ggg" };
            oList.AddRenge(c);




        }
    }
}
