using System.Collections;
using System.Collections.Generic;
/*
class Program
{
    public static void Main(string[] args)
    {
        List<string> list = new List<string>();
        //list.Add(1);
        list.Add("Hello");
        //list.Add(true);
        foreach (string o in list)
            Console.WriteLine(o);

        List<string> fruits = new List<string>();
        fruits.Add("Apple");
        fruits.Add("Mango");
        fruits.Add("Orange");

        list.AddRange(fruits);

        list.Insert(0, "Banana");
        list[0] = "tomato";
        list.RemoveAt(1);
        foreach (string o in list)
        {
            Console.WriteLine(o);
        }

        //Console.WriteLine(list.Capacity);
        //Console.WriteLine(list.Count);
    }

}
*/

class Program
{
    public static void Main(string[] args)
    {
        Dictionary<int, string> dict = new Dictionary<int, string>();

        dict.Add(1, "Hello");
        dict.Add(10, "Orange");
        dict.Add(15, "Mango");

        dict.Remove(1);

        if (dict.ContainsKey(10))
            Console.WriteLine("sucess");
        else
            Console.WriteLine("Failure");
            foreach (var o in dict)
                Console.WriteLine(o);
        

    }

}
