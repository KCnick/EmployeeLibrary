﻿using  System;
using System.Linq;
using System.Collections.Generic;

namespace Employee
{
    public class Employees
    {
        private DirectedSparseGraph<Employe> graph;
        private Dictionary<string, Employe> employees;
        
        
        public Employees(string[] lines)
        {
            graph = new DirectedSparseGraph<Employe>();
            employees = new Dictionary<string, Employe>();
            var lns = lines.Select(a => a.Split('\t'));
            var csv = from line in lns
                select (from piece in line
                    select piece);
            int ceos = 0;
            foreach (var n in csv)
            {
                
                var p = n.GetEnumerator();
                while (p.MoveNext())
                {
                    try
                    {
                    var data = p.Current.Split(',');
                    if (string.IsNullOrEmpty(data[0]))
                    {
                        Console.WriteLine("Employee cannot have empty Id, skipping ...");
                        continue;
                    }

                    if (string.IsNullOrEmpty(data[1]) && ceos<1)
                    {
                        ceos ++;
                    }
                    else if (string.IsNullOrEmpty(data[1]) && ceos==1)
                    {
                        Console.WriteLine("There can only be 1 ceo in the organization, skipping ...");
                        continue;
                    }
                    
                    
                    int sal = 0;
                    // ensure that employee salary is a valid integer
                    if (Int32.TryParse(data[2], out sal))
                    {
                        var empl = new Employe(data[0], data[1], sal);
                        try
                        {
                            employees.Add(empl.Id,empl);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Error when adding employee to dictionary",e);
                        }
                        
                        if (!graph.HasVertex(empl))
                        {
                            graph.AddVertex(empl);
                        }
                       
                    }
                    else
                    {
                        Console.WriteLine("Salary not a valid integer, skipping ...");
                    }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                p.Dispose();

            }

            foreach (KeyValuePair<string,Employe> kvp in employees)
            {
                if (!string.IsNullOrEmpty(kvp.Value.Manager))
                {
                    // check for double linking
                    bool doubleLinked = false;
                    foreach (Employe employee in graph.DepthFirstWalk(kvp.Value).ToArray())
                    {
                        if (employee.Equals(kvp.Value.Manager))
                        {
                            doubleLinked = true;
                            break;
                        }
                    }
                    // ensure that each employee has only one manager
                    if (graph.IncomingEdges(kvp.Value).ToArray().Length < 1 && !doubleLinked )
                    {
                        graph.AddEdge( employees[kvp.Value.Manager],kvp.Value);
                    }
                    else
                    {
                        Console.WriteLine(graph.IncomingEdges(kvp.Value).ToArray().Length>=1 ?
                            String.Format("Employee {0} have more than one manager",kvp.Value.Id) :
                            "Double linking not allowed");
                    }
                }
               
            }
           
        }

        public long SalaryBudget(string manager)
        {
            var salaryBudget = 0;
            try
            {
                var employeesInPath = graph.DepthFirstWalk(employees[manager]).GetEnumerator();
                while (employeesInPath.MoveNext())
                {
                    salaryBudget += employeesInPath.Current.Salary;

                }
            }
            catch (Exception var0)
            {
                Console.WriteLine("Error occured when getting salary budget ",var0);
            }

            return salaryBudget;
        }
    }

    public class Employe : IComparable<Employe>
    {
        public string Id { get; set; }
        public int Salary { get; set; }
        
        public string Manager { get; set; }

        public Employe(string id,  string manager, int salary)
        {
            Id = id;
            Salary = salary;
            Manager = manager;
        }
        
        public int CompareTo(Employe other)
        {
            if(other == null) return -1;
            return string.Compare(this.Id,other.Id,
                StringComparison.OrdinalIgnoreCase);
        }
    }
}