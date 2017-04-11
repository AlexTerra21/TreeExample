using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TreeExample
{
    class Program
    {
        ///<summary>
        ///  Из строки inputStr формируется IntHolder, DoubleHolder или StringHolder,
        ///  полученный объект добавляется в новый объект Node и возвращается
        /// </summary>
        /// <param name="inputStr">Строка содержащая значение типа int, double или string</param>>
        /// <returns>Сформированный объект типа Node - узел дерева</returns>
        static Node BuildNode(string inputStr) 
        {
            try
            {
                int i = int.Parse(inputStr); // Пробуем преобразовать в Int
                return new Node(new IntHolder(i));
            }
            catch (Exception e)
            {
                try
                {
                    double d = Double.Parse(inputStr); // Пробуем преобразовать в Double
                    return new Node(new DoubleHolder(d));
                }
                catch (Exception e1)
                {
                    return new Node(new StringHolder(inputStr)); // Все остальное в String
                }
            }
        }

        static void Main(string[] args)
        {

            Console.WriteLine("****** Построение дерева ******");
            Console.WriteLine("Узлы дерева могут быть трех типов: int, double и string");


            XElement doc =
                new XElement("Root",
                    new XElement("Node", new XAttribute("Value","1"),
                        new XElement("Node", new XAttribute("Value","Str"),
                            new XElement("Node", new XAttribute("Value","18"))), 
                        new XElement("Node", new XAttribute("Value","3,14"),
                             new XElement("Node", new XAttribute("Value", "1,999"))), 
                        new XElement("Node", new XAttribute("Value","10000"))

                )
            );


            Tree finalTree = new Tree(BuildNode("1"));

            finalTree.SearchAndAddItem(BuildNode("1"), BuildNode("2011"));
            finalTree.SearchAndAddItem(BuildNode("1"), BuildNode("C++"));
            finalTree.SearchAndAddItem(BuildNode("1"), BuildNode("3,14"));
            finalTree.SearchAndAddItem(BuildNode("2011"), BuildNode("FULCRUM"));
            finalTree.SearchAndAddItem(BuildNode("FULCRUM"), BuildNode("LINUX"));
            finalTree.SearchAndAddItem(BuildNode("FULCRUM"), BuildNode("7"));
            finalTree.SearchAndAddItem(BuildNode("3,14"), BuildNode("TEST"));
            finalTree.SearchAndAddItem(BuildNode("3,14"), BuildNode("9"));
            finalTree.SearchAndAddItem(BuildNode("3,14"), BuildNode("6"));

            finalTree.PrintTree();
            finalTree.WriteToXML("finalTree.xml");
            Console.ReadLine();


            // Дальнейший закомментироаный код пока не работает поскольку я еще не разобрался с сериализацией!!!
            //var myStreamWriter = new StreamWriter("finalTreeSerialize.xml");

            //var mySerializer = new XmlSerializer(typeof(Tree));

            //mySerializer.Serialize(myStreamWriter, finalTree); 
            //myStreamWriter.Close();

            Console.WriteLine(doc);
        //    doc.Descendants().
            Console.Write("Введите кореневой элемент (0-для окончания):");
            string inputStr = Console.ReadLine();
            Tree myTree = new Tree(BuildNode(inputStr));
            myTree.PrintTree();
           

            while (true)
            {
                Console.Write("Выберите узел:");
                string selectedNode = Console.ReadLine();
                if (selectedNode == "0") break;
                Console.Write("Введите добавляемый узел:");
                string addedNode = Console.ReadLine();
                myTree.SearchAndAddItem(BuildNode(selectedNode),BuildNode(addedNode));
                myTree.PrintTree();
            }

            myTree.WriteToXML("myTree.xml");
            Console.WriteLine("Xml файл записан");
            Console.ReadLine();

            
        }
    } // End of class Program
} // TreeExample
