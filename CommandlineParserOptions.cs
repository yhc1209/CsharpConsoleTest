using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace testCons
{
    [Verb("v1", HelpText = "Verb 1. ha ha.")]
    public class Verb1
    {
        [Option("f1", HelpText = "set Flag1.")]
        public bool Flag1 {get; set;}
        [Option("members", HelpText = "set Members.")]
        public IEnumerable<string> Members {get; set;}

        public static void Verb1Func(Verb1 op)
        {
            Console.WriteLine("------ Verb1 ------");
            Console.WriteLine($"Flag1: {op.Flag1}");
            if (op.Members != null)
            {
                Console.WriteLine("Members:");
                foreach (string m in op.Members)
                    Console.WriteLine($"  - {m}");
            }
            else
                Console.WriteLine("Members is null.");
        }
    }
    
    [Verb("v2", HelpText = "Verb 2. bla bla.")]
    public class Verb2
    {
        [Option("f1", HelpText = "set Flag1.", Group = "verb2")]
        public bool Flag1 {get; set;}
        [Option("members", HelpText = "set Members.", Group = "verb2")]
        public IEnumerable<string> Members {get; set;}

        public static void Verb2Func(Verb2 op)
        {
            Console.WriteLine("------ Verb2 ------");
            Console.WriteLine($"Flag1: {op.Flag1}");
            if (op.Members != null)
            {
                Console.WriteLine("Members:");
                foreach (string m in op.Members)
                    Console.WriteLine($"  - {m}");
            }
            else
                Console.WriteLine("Members is null.");
        }
    }
}