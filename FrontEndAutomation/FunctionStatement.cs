﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace FrontEndAutomation
{
    public class FunctionStatement : Statement
    {
        public string Name { get; set; }

        public FunctionStatement(Scope scope, string name)
        {
            statements = new List<Statement>();
            this.Name = name;
        }

        public FunctionStatement(string name, XmlNode node)
        {
            statements = new List<Statement>();
            Name = name;
            foreach(XmlNode step in node.ChildNodes)
            {
                statements.Add(Compiler.Compile(step));
            }
        }

        public void Add(Statement statement)
        {
            statements.Add(statement);
        }
        private List<Statement> statements;
        public Scope Scope
        {
            get; set;
        }
        
        public object Process(Executor executor)
        {
            executor.Scope.Set(Name, null);
            Exception ex = null;
            foreach(Statement statement in statements)
            {
                //try {
                    statement.Process(executor);
                //}
                //catch(Exception e)
                //{
                //    ex = e;
                //}
                //Console.WriteLine(statement.ToString());
            }
            //if (ex != null)
            //    throw ex;
            return executor.Scope.Resolve(Name);
        }
    }
}