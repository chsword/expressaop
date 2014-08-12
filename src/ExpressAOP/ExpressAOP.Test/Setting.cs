using System.Collections.Generic;

namespace ExpressAOP.Test
{
    public class Setting
    {
        static Setting()
        {
            List=new List<string>();
        }
        static public List<string> List { get; set; } 
    }
}