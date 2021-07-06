using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Employee;
using System.IO;

namespace EmployeeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestEmployees()
        {
            var lines = File.ReadAllLines("./test.txt");
            Employees employees = new Employees(lines);
            Assert.AreEqual(3300,employees.SalaryBudget("Employee1"));
            Assert.AreEqual(1000,employees.SalaryBudget("Employee2"));
        }

        [TestMethod]
        public void TestDoubleLink()
        {
            var lines = File.ReadAllLines("./test1.txt");
            Employees employees = new Employees(lines);
            Assert.AreEqual(3300,employees.SalaryBudget("Employee1"));
            Assert.AreEqual(1000,employees.SalaryBudget("Employee2"));
        }


        [TestMethod]
        public void TestInvalidSalaryValue()
        {
            var lines = File.ReadAllLines("./test3.txt");
            Employees employees = new Employees(lines);
            Assert.AreEqual(3300,employees.SalaryBudget("Employee1"));
            Assert.AreEqual(0,employees.SalaryBudget("Employee6"));
 
        }
    }
     public static class GraphsDirectedSparseGraphTest
    {
        [TestMethod]
        public static void DoTest()
        {
            var graph = new DirectedSparseGraph<Employe>();
            
            var employeeA = new Employe("a","",10);
            var employeeB = new Employe("b","a",10);
            var employeeC = new Employe("c","a",10);
            var employeeD = new Employe("d","a",10);
            var employeeE = new Employe("e","b",10);

            var verticesSet1 = new Employe[] { employeeA, employeeB, employeeC, employeeD, employeeE };

            graph.AddVertices(verticesSet1);

            graph.AddEdge(employeeA, employeeB);
            graph.AddEdge(employeeA, employeeC);
            graph.AddEdge(employeeA, employeeD);
            graph.AddEdge(employeeB, employeeE);

            Assert.IsTrue(graph.VerticesCount == 5, "Wrong vertices count.");
            Assert.IsTrue(graph.EdgesCount == 4, "Wrong edges count.");
           
            var dfsWalk = graph.DepthFirstWalk(employeeA);
           
            foreach (var node in dfsWalk)
            {
                Console.Write(String.Format("({0})", node.Id));
            }

            graph.Clear();
            
        }

    }
}