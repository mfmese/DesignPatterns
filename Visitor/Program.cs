using System;
using System.Collections.Generic;

namespace Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager ali = new Manager() { EmployeeName = "Ali", Salary = 10000 };
            Manager veli = new Manager() { EmployeeName = "Veli", Salary = 9000 };

            Worker mahmut = new Worker() { EmployeeName = "Mahmut", Salary = 7000 };
            Worker zeyno = new Worker() { EmployeeName = "Zeyno", Salary = 7100 };

            ali.subordinates.Add(veli);
            veli.subordinates.Add(mahmut);
            veli.subordinates.Add(zeyno);

            OrganizationService service = new OrganizationService(ali);

            PayrollVisitor payrollVisitor = new PayrollVisitor();
            PayRaiseVisitor payRaiseVisitor = new PayRaiseVisitor();

            service.Accept(payrollVisitor);
            service.Accept(payRaiseVisitor);

            Console.Read();
        }
    }

    class OrganizationService
    {
        private EmployeeBase _employee;

        public OrganizationService(EmployeeBase employee)
        {
            _employee = employee;
        }

        public void Accept(VisitorBase visitor)
        {
            _employee.Accept(visitor);
        }
    }

    abstract class EmployeeBase
    {
        public abstract void Accept(VisitorBase visitor);
        public string EmployeeName { get; set; }
        public decimal Salary { get; set; }
    }

    class Manager : EmployeeBase
    {
        public List<EmployeeBase> subordinates { get; set; }

        public Manager()
        {
            subordinates = new List<EmployeeBase>();
        }

        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);

            foreach (var subordinate in subordinates)
            {
                subordinate.Accept(visitor);
            }
        }
    }

    class Worker : EmployeeBase
    {   
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }

    abstract class VisitorBase
    {
        public abstract void Visit(Manager manager);
        public abstract void Visit(Worker worker);
    }

    class PayrollVisitor : VisitorBase
    {

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} paid {1}", manager.EmployeeName, manager.Salary);
        }

        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} paid {1}", worker.EmployeeName, worker.Salary);
        }
    }

    class PayRaiseVisitor : VisitorBase
    {

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} salary increased from {1} to {2}", manager.EmployeeName, manager.Salary, manager.Salary * (decimal)1.5);
        }

        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} salary increased from {1} to {2}", worker.EmployeeName, worker.Salary, worker.Salary * (decimal)1.2);
        }
    }
}
