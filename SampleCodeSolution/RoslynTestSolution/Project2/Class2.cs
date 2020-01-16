using System;
using System.Xml;
using Newtonsoft.Json;
using Project1;

namespace Project2
{
    public class Class2
    {
        //mscorlib types
        public int Id2A { get; set; }
        public Int32 Id2B { get; set; }
        public string Description2A { get; set; }
        public String Description2B { get; set; }
        public DateTime DateTime1 { get; set; }
        
        //External assembly types
        public XmlComment XmlComment { get; set; }
        public JsonReader JsonReader { get; set; }

        //Referenced project type
        public Class1B Class1B { get; set; }
    }
}