using System;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Collections.Generic;

namespace testCons
{
    public class Cat
    {
        public string Name {get; set;}
        public int Age {get; set;}
        public List<string> Babies {get; set;}

        public Cat()
        {
            Age = 1;
            Name = string.Empty;
        }
        public Cat(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public Cat(string name, int age, IEnumerable<string> babies)
        {
            Name = name;
            Age = age;
            Babies = new List<string>();
            Babies.AddRange(babies);
        }
        public static Cat Load(string json)
        {
            return JsonSerializer.Deserialize<Cat>(json);
        }

        public virtual void Roar()
        {
            Console.WriteLine($"I am a Cat, my name is {Name}, meow.");
        }

        // public override bool Equals(object obj)
        // {
        //     //Check for null and compare run-time types.
        //     if ((obj == null) || ! this.GetType().Equals(obj.GetType()))
        //     {
        //         return false;
        //     }
        //     else
        //     {
        //         Cat cat = (Cat)obj;
        //         return (this.Age == cat.Age && this.Name.ToLower() == cat.Name.ToLower());
        //     }
        // }
        
        public override bool Equals(object obj)
        {
            try
            {
                Cat cat = (Cat)obj;
                return (this.Age == cat.Age && this.Name.ToLower() == cat.Name.ToLower());
            }
            catch (Exception) {}
 
            return false;
        }

        public override string ToString()
        {
            if (Name == null)
                return $"<NULL> is a {Age} year-old Cat.";
            else
            {
                if (Name.Length == 0)
                    return $"<NoName> is a {Age} year-old Cat.";
                else
                    return $"{Name} is a {Age} year-old Cat.";
            }
        }

        public static T Produce<T>(string name) where T: Cat, new()
        {
            T cat = new T();
            cat.Name = name;
            return cat;
        }

        public void CopyFrom(Cat org)
        {
            Name = org.Name;
            Age = org.Age;
            Babies = org.Babies;
        }
    }

    public class FlyingCat: Cat
    {
        public string wing {get; set;} 
        public override void Roar()
        {
            Console.WriteLine($"I am a flying cat, my name is {Name}.");
        }
    }
    public class CampingCat: Cat
    {
        public string paw {get; set;}
        public override void Roar()
        {
            Console.WriteLine($"I am a camping Cat, my name is {Name}.");
        }
    }
    
    class OBJ1
    {
        public int id {get; set;}
        public bool bCheck2 {get; set;}
        public bool bCheck {get; set;}

        public static string currentTime {
            get { return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff"); }
        }
        public static string[] currentTimes {
            get { return new string[] {DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff"), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff")}; }
        }
    }

    // J4T
    public class J4T
    {
        public int id = 1;
        public bool flag;

        public J4T()
        {
            id = 2;
        }

        public void CheckId()
        {
            Console.WriteLine($"Id = {id}");
        }
    }
}