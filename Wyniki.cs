using System;
using System.Collections.Generic;
using System.Text;

namespace LigaSiatkarskaProjekt
{
    class Wyniki
    {
       public string DruzynaGospodarzy { get; set; }
       public string DruzynaGosci { get; set; }
       
       public int LiczbaSetowGospodarzy { get; set; }
        
       public int LiczbaSetowGosci { get; set; }
        public Wyniki(string Gospodarz,string Gosc, int setyGosp, int setyGos)
        {
            DruzynaGospodarzy = Gospodarz;
            DruzynaGosci = Gosc;
            LiczbaSetowGospodarzy = setyGosp;
            LiczbaSetowGosci = setyGos;
        }
        public override string ToString() => $"{DruzynaGospodarzy} {LiczbaSetowGospodarzy} : {LiczbaSetowGosci} {DruzynaGosci}";

        
            
        

    }
}
