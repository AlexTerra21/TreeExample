using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TreeExample
{
    class Node
    {
        public List<Node> branch;
        public BaseHolder Value { get; set; }


        public Node(BaseHolder val) 
        {
            branch = new List<Node>();
            Value = val;
        }
 
    } // End of class Node

    class Tree
    {
        private Node root;
        private XmlDocument docXml = new XmlDocument(); // Xml документ для вывод дерева

        public Tree() { }
        public Tree (Node n)
        {
            root = n;
        }


        ///<summary>
        /// Запись дерева объектов в Xml файл
        /// </summary>
        /// <param name="pathToXml">Строка содержащая путь к записываемому xml-файлу</param>>
        public void WriteToXML(string pathToXml)
        {
            XmlElement head = docXml.CreateElement("Root"); // Создание корневого элемента
            XmlElement element = docXml.CreateElement("Node"); // Первый узел
            element.SetAttribute("Value", root.Value.ToString()); // Установка значения объекта в виде атрибута
            FormationOfXmlDocument(element, root); // Рекурсвный метод для формирования дерева Xml
            head.AppendChild(element); // Добавляем первый узел к корню документа
            docXml.AppendChild(head); // Формироум документ
            docXml.Save(pathToXml); // Запись в файл
        }


        ///<summary>
        /// Рекурсивный метод для обхода дерева объектов и формирования Xml документа
        /// </summary>
        /// <param name="rootElement">Текущий элемент Xml документа</param>>
        /// <param name="n">Текущий узел дерева</param>>
        private void FormationOfXmlDocument( XmlElement rootElement , Node n)
        {
            foreach (var i in n.branch) // Цикл по всем ветвям текущего узла
            {
                XmlElement newElement = docXml.CreateElement("Node"); // Добавление нового узла Xml документа
                newElement.SetAttribute("Value", i.Value.ToString()); // Установка значения объекта в виде атрибута
                rootElement.AppendChild(newElement); // Добавляемновый узел к текущему корню документа
                FormationOfXmlDocument(newElement,i); // Рекурсивыный обход
            }
        }

        
        public void AddRootBranch(Node n )
        {
            root.branch.Add(n);
        }


        // Добавление элемента addedNode к узлу selectedNode  
        public bool SearchAndAddItem(Node selectedNode,Node addedNode)
        {
            return SearchAndAddItemBranch(root, selectedNode, addedNode);     
        }

        private bool SearchAndAddItemBranch(Node treeNode, Node searchNode,Node addNode)
        {
            bool result = false;
            if (treeNode.Value.ToString() == searchNode.Value.ToString())
            {
                treeNode.branch.Add(addNode);
                return true;
            }
            foreach (var i in treeNode.branch)
            {
                result = SearchAndAddItemBranch(i, searchNode, addNode); 

            }
            return result;
        }

        public void PrintTree()
        {
            Console.WriteLine(root.Value.ToString()+" ("+root.Value.GetType()+")");
            PrintBranch(root,"-");

        }

        private void PrintBranch(Node n,string filler)
        {
            foreach (var i in n.branch)
            {
                Console.WriteLine(filler+i.Value.ToString() + " (" + i.Value.GetType() + ")");
                PrintBranch(i,filler+"-");
            }
        }

    }
}
