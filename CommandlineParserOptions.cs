using System;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace testCons
{
    [Verb("verb1", HelpText = "Verb 1. ha ha.")]
    public class Verb1
    {
        [Option("f1", HelpText = "set Flag1.")]
        public bool Flag1 {get; set;}
        [Option("members", HelpText = "set Members.")]
        public IEnumerable<string> Members {get; set;}

        public static void Verb1Func(Verb1 op)
        {
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