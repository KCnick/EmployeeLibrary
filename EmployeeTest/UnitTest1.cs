using Microsoft.VisualStudio.TestTools.UnitTesting;
using Employee;
using System;
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
        //test invalid salary value for employee 6
        [FactMethod]
        public void Test3()
        {
            var lines = File.ReadAllLines("./test3.txt");
            Employees employees = new Employees(lines);
            Assert.AreEqual(3300,employees.SalaryBudget("Employee1"));
            Assert.AreEqual(1300,employees.SalaryBudget("Employee2"));
            Assert.AreEqual(500,employees.SalaryBudget("Employee3"));
            Assert.AreEqual(0,employees.SalaryBudget("Employee6"));
 
        }

        [FactMethod]
        public void Test2()
        {
            var lines = File.ReadAllLines("./test2.txt");
            Employees employees = new Employees(lines);
            Assert.AreEqual(3800,employees.SalaryBudget("Employee1"));
            Assert.AreEqual(1800,employees.SalaryBudget("Employee2"));
            Assert.AreEqual(500,employees.SalaryBudget("Employee3"));
 
        }

         [FactMethod]
        public void TestDoubleLink()
        {
            var lines = File.ReadAllLines("./../test1.txt");
            Employees employees = new Employees(lines);
            Assert.AreEqual(3300,employees.SalaryBudget("Employee1"));
            Assert.AreEqual(1000,employees.SalaryBudget("Employee2"));
 
        }
    }
}
