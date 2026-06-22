using GenericList;
using System;

namespace gnericList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // =========================================
            // Create New List Instance
            // =========================================

            MyList<string> oList = new MyList<string>();


            // =========================================
            // Test Initial State
            // BreakPoint Here:
            // Check default Count and Capacity
            // =========================================

            Console.WriteLine("Initial Count: " + oList.Count);
            Console.WriteLine("Initial Capacity: " + oList.Capacity);


            // =========================================
            // Test Add Method
            // BreakPoint Here:
            // Watch Count increase after each Add
            // Watch automatic resizing after capacity exceeded
            // =========================================

            oList.Add("yousef");
            oList.Add("mo");
            oList.Add("ahmed");
            oList.Add("ali");
            oList.Add("mohamed");
            oList.Add("khaled");


            // =========================================
            // BreakPoint Here:
            // Check:
            // Count
            // Capacity
            // Internal array values
            // =========================================

            Console.WriteLine("\nAfter Add:");
            Console.WriteLine("Count: " + oList.Count);
            Console.WriteLine("Capacity: " + oList.Capacity);


            // =========================================
            // Test Indexer Get
            // BreakPoint Here:
            // Verify returned element from index
            // =========================================

            Console.WriteLine("\nIndexer Get:");
            Console.WriteLine(oList[0]);
            Console.WriteLine(oList[1]);


            // =========================================
            // Test Indexer Set
            // BreakPoint Here:
            // Verify value replacement inside array
            // =========================================

            oList[1] = "mazen";

            Console.WriteLine("\nAfter Indexer Set:");
            Console.WriteLine(oList[1]);


            // =========================================
            // Test String Indexer
            // BreakPoint Here:
            // Verify multiple selected indexes
            // =========================================

            var selectedItems = oList["0,2,4"];

            Console.WriteLine("\nString Indexer:");

            foreach (var item in selectedItems)
            {
                Console.WriteLine(item);
            }


            // =========================================
            // Test Contains Method
            // BreakPoint Here:
            // Verify search behavior
            // =========================================

            Console.WriteLine("\nContains:");
            Console.WriteLine(oList.Contains("ali"));
            Console.WriteLine(oList.Contains("notfound"));


            // =========================================
            // Test IndexOf Method
            // BreakPoint Here:
            // Verify returned index
            // =========================================

            Console.WriteLine("\nIndex Of:");
            Console.WriteLine(oList.IndexOf("ali"));
            Console.WriteLine(oList.IndexOf("test"));


            // =========================================
            // Test Remove Method
            // BreakPoint Here:
            // Watch left shifting after removal
            // =========================================

            oList.Remove("ahmed");

            Console.WriteLine("\nAfter Remove:");
            foreach (var item in oList)
            {
                Console.WriteLine(item);
            }


            // =========================================
            // Test RemoveAt Method
            // BreakPoint Here:
            // Verify shifting and Count decrement
            // =========================================

            oList.RemoveAt(0);

            Console.WriteLine("\nAfter RemoveAt:");
            foreach (var item in oList)
            {
                Console.WriteLine(item);
            }


            // =========================================
            // Test AddRange Method
            // BreakPoint Here:
            // Verify bulk insertion and resizing
            // =========================================

            string[] newItems =
            {
                "one",
                "two",
                "three"
            };

            oList.AddRange(newItems);

            Console.WriteLine("\nAfter AddRange:");

            foreach (var item in oList)
            {
                Console.WriteLine(item);
            }


            // =========================================
            // Test Insert Method
            // BreakPoint Here:
            // Verify right shifting before insertion
            // =========================================

            oList.Insert(2, "inserted-item");

            Console.WriteLine("\nAfter Insert:");

            foreach (var item in oList)
            {
                Console.WriteLine(item);
            }


            // =========================================
            // Test InsertRange Method
            // BreakPoint Here:
            // Verify multi-item insertion
            // =========================================

            string[] insertedValues =
            {
                "A",
                "B",
                "C"
            };

            oList.InsertRange(1, insertedValues);

            Console.WriteLine("\nAfter InsertRange:");

            foreach (var item in oList)
            {
                Console.WriteLine(item);
            }


            // =========================================
            // Test RemoveRange Method
            // BreakPoint Here:
            // Verify range deletion and shifting
            // =========================================

            oList.RemoveRange(2, 3);

            Console.WriteLine("\nAfter RemoveRange:");

            foreach (var item in oList)
            {
                Console.WriteLine(item);
            }


            // =========================================
            // Test Reverse Method
            // BreakPoint Here:
            // Verify reverse swapping logic
            // =========================================

            oList.Reverse();

            Console.WriteLine("\nAfter Reverse:");

            foreach (var item in oList)
            {
                Console.WriteLine(item);
            }


            // =========================================
            // Test ToArray Method
            // BreakPoint Here:
            // Verify copied active elements only
            // =========================================

            string[] array = oList.ToArray();

            Console.WriteLine("\nToArray:");

            foreach (var item in array)
            {
                Console.WriteLine(item);
            }


            // =========================================
            // Test Foreach Enumeration
            // BreakPoint Here:
            // Verify IEnumerable implementation
            // =========================================

            Console.WriteLine("\nForeach Test:");

            foreach (var item in oList)
            {
                Console.WriteLine(item);
            }


            // =========================================
            // Test Clear Method
            // BreakPoint Here:
            // Verify reset behavior
            // =========================================

            oList.Clear();

            Console.WriteLine("\nAfter Clear:");
            Console.WriteLine("Count: " + oList.Count);
            Console.WriteLine("Capacity: " + oList.Capacity);


            // =========================================
            // Final BreakPoint
            // Inspect final object state
            // =========================================

            Console.ReadKey();
        }
    }
}