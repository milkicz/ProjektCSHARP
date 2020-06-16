using System;
using System.Collections.Generic;
using System.Text;

namespace LigaSiatkarskaProjekt
{
    /*//w celu sortowania listy konieczne bylo przeciazenie metody Sort,
    //wzorowalem sie na przykladzie dostepnym w dokumentacji C#
    */
    public class Team : IEquatable<Team>, IComparable<Team>
    {

        private static int counter = 1;
        public int counter1 = counter;
        public string TeamName { get; set; }
         
            
                
        public int LMeczow { get; set; }
        public int LPunktow { get; set; }
        public int MeczeWygrane { get; set; }
        public int MeczePrzegrane { get; set; }
        public int LiczbaSetow { get; set; }
        public int SetyWygrane { get; set; }
        public int SetyPrzegrane { get; set; }
        char[] skrot;


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Team objAsTeam = obj as Team;
            if (objAsTeam == null) return false;
            else return Equals(objAsTeam);
        }
        public int SortByNameAscending(string name1, string name2)
        {

            return name1.CompareTo(name2);
        }
        
        public int CompareTo(Team compareTeam)
        {
            
            if (compareTeam == null)
                return 1;

            else
                return this.LPunktow.CompareTo(compareTeam.LPunktow);
        }
        public override int GetHashCode()
        {
            return LPunktow;
        }
        public bool Equals(Team other)
        {
            if (other == null) return false;
            return (this.LPunktow.Equals(other.LPunktow));
        }


        public Team(string teamName)
        {


            counter++;
            TeamName = teamName.ToUpper();           
            try
            {
                skrot = teamName.ToCharArray(0, 3);
            }
            catch
            {
                throw new ArgumentException("Nazwa druzyny musi zawierac przynajmniej 3 znaki!");
            }
            LiczbaSetow = 0;
            LMeczow = 0;
            LPunktow = 0;
            MeczeWygrane = 0;
            MeczePrzegrane = 0;
            SetyWygrane = 0;
            SetyPrzegrane = 0;


        }




        public override string ToString() => $" {string.Join("", skrot)}  |  {LPunktow}  |  {LMeczow}  |  {MeczeWygrane}  |" +
            $"  {MeczePrzegrane}  |  {LiczbaSetow}  |  {SetyWygrane}  |  {SetyPrzegrane}  |";

    }
}
