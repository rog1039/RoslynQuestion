using System;
using System.Xml;
using Newtonsoft.Json;

namespace Project1
{
    public class Class1
    {
        //mscorlib types
        public int Id1A { get; set; }
        public Int32 Id1B { get; set; }
        public string Description1A { get; set; }
        public String Description1B { get; set; }
        public DateTime DateTime1 { get; set; }
        
        //External assembly types
        public XmlComment XmlComment { get; set; }
        public JsonReader JsonReader { get; set; }
    }
    
    public class Class1B{}
}