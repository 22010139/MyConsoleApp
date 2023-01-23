using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyConsoleApp

{
   class MyObjects { //объявление класса 
   
    public string time1; // первое время
    public string time2; // второе время
    public string FIO; // ФИО
    public string Work; // Название темы для конференции

        public MyObjects (string t1, string t2, string fio, string work) {
            time1 = t1;
            time2 = t2;
            FIO = fio;
            Work = work;
        }

        public MyObjects()
        {
            Console.Write("Введите первое время в формате чч:мм  -  ");
            time1 = Console.ReadLine();
            Console.Write("Введите второе время в формате чч:мм  -  ");
            time2 = Console.ReadLine();
            Console.Write("Введите ФИО выступающего   -   ");
            FIO = Console.ReadLine();
            Console.Write("Введите тему конференции   -   ");
            Work = Console.ReadLine();
        }
        public double count_min() {

           
               TimeSpan dt= Convert.ToDateTime(time1).Subtract(Convert.ToDateTime(time2));
            return dt.TotalMinutes;
        }

        public void Show() 
        {
            Console.WriteLine($"Начало........: {time1}\n" +
                              $"Конец.........: {time2}\n"+
                              $"Выступающий...: {FIO}\n" +
                              $"Тема..........: {Work}\n"); 
        }
       




    }




    class Program
    {
        public static void SortObj(List <MyObjects> l)
        {
            MyObjects temp;

            for (int i = 0; i < l.Count; i++)
            {
                for (int j = 0; j < l.Count; j++)
                {
                    if (l[i].count_min() < l[j].count_min())
                    {
                        temp = l[i];
                        l[i] = l[j];
                        l[j] = temp;
                    }
                }
            }
        }

        

        static void Main(string[] args)
        {
            Console.Write($"Лабораторная работа №1. GIT\n" +
                           $"Вариант №2. Программа конференции\n" +
                           $"Автор: Игорь Ходасевич\n" +
                           $"Группа: XXII\n\n" +
                           $"***** Программа конференции *****\n\n");
            int count = System.IO.File.ReadAllLines("data.txt").Length;
            List<MyObjects> mlist = new List<MyObjects>();
           // MyObjects[] mass = new MyObjects[count];
            string[] line;
            StreamReader sr = new StreamReader("data.txt");
            for (int i = 0; i < count; i++)
            {   mlist.Add(new MyObjects("00:00","00:00","noname","noname"));
                line = sr.ReadLine().Split('+');

                mlist[i].time1 = line[0];
                mlist[i].time2 = line[1];
                mlist[i].FIO = line[2];
                mlist[i].Work = line[3];

                mlist[i].Show();
            }
           
            sr.Close();
            int value;
         link1:   Console.WriteLine("\nВыберите способ фильтрации или обработки данных:\n\n" +
                "1)Добавить новую запись\n" +
                "2)Вывести доклады по убыванию времени проведения\n" +
                "3)Вывести все доклады Иванова Ивана Ивановича\n" +
                "4)Вывести все доклады длительностью больше 15 минут\n" +
                "\nВведите номер выбранного пункта: ");
            value =Convert.ToInt32( Console.ReadLine());
            switch (value) {
                case 1: {
                        mlist.Add(new MyObjects());
                        StreamWriter sw = new StreamWriter("data.txt", true);
                        sw.WriteLine($"{mlist[count].time1}+{mlist[count].time2}+{mlist[count].FIO}+{mlist[count].Work}");
                        sw.Close();
                        break; }
                case 2:
                    Console.Write($"*****Доклады по возрастанию времени проведения*****\n\n");
                    {
                        SortObj(mlist);
                        Console.WriteLine("-----------------------------------------------------------------");
                        for (int i = 0; i < count; i++)
                        {
                            mlist[i].Show();
                        }
                        break; }
                case 3:
                    Console.Write($"*****Все доклады Иванова Ивана Ивановича*****\n\n");
                    {
                        List<MyObjects> result = mlist.FindAll(delegate(MyObjects s)
                        {
                            bool a = s.FIO.ToString() == "Иванов Иван Ивановича";
                            return a;
                        });
                        foreach (var item in result)
                        {
                            Console.WriteLine(item.Work);
                           
                        }
                        
                        break;
                     }

                     case 4:
                    Console.Write($"*****Все доклады длительностью больше 15 минут*****\n\n");

                    {
                        List<MyObjects> result = mlist.FindAll(delegate (MyObjects s)
                        {
                            var a = Convert.ToDateTime(s.time1);
                            var b = Convert.ToDateTime(s.time2);
                            var c = (b - a).TotalMinutes;
                            bool d = c > 15;
                            ;
                            return d;
                        });
                        foreach (var item in result)
                        {
                            Console.WriteLine(item.Work);
                        }

                        break;
                    }



                case 0: { break; }
            }

            goto link1;
          




            Console.ReadKey();
        }
    }
}
