using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LigaSiatkarskaProjekt
{
    

    public class League
    {

        public string MyLeague { get; set; }
        private List<Team> myTeams { get; set; }

        public int kolejka = 1;
        private readonly List<Wyniki> historia;
        private IReadOnlyList<Wyniki> RozegraneMecze => historia.AsReadOnly();

        public Status StanGry { get; private  set; }

        public League(string myLeague)
        {
            MyLeague = myLeague;
            this.myTeams = new List<Team>();
            historia = new List<Wyniki>();
            StanGry = Status.Wtrakcie;



        }

        


        public void DodajDruzyne(string name)
        {
            Team t = new Team(name);
            DodajDruzyne(t);
        }
        public void DodajDruzyne(Team t)
        {
            if(t.TeamName.Length <= 24 || t.TeamName.Length < 3)
            {
                if (ZnajdzIndeksDruzyny(t.TeamName) == -1)
                {
                    myTeams.Add(t);

                }

                
            }
            else
            {
                throw new ArgumentException("Nazwa druzyny ma nieodpowiednia wielkosc!");
            }
            
        }
       

        public void WyswietlTabele()
        {
            Console.WriteLine($" TEAM |     |     MATCHES     |       SETS      |");
            Console.WriteLine( " NAME |  P  |  M  |  W  |  L  |  S  |  W  |  L  |");
            myTeams.Sort(); 
            myTeams.Reverse();
            foreach (Team aTeam in myTeams)
            {
                Console.WriteLine(aTeam);
            }
           
        }
        public void WyswietlDruzyny()
        {

            for (int i = 0; i < myTeams.Count; i++)
            {
                Console.WriteLine($"{myTeams[i].TeamName} --> {i}");
            }
        }
       

        public int ZnajdzIndeksDruzyny(string name)
        {
            int result = -1;

            for (int i = 0; i < myTeams.Count; i++)
            {
                if (myTeams[i].TeamName == name)
                {
                    result = i;
                    break;
                }
            }
            return result;

        }
        static int LiczbaSetowA()
        {
            Console.Write("Sets won by Home team:  ");
            int setwon = int.Parse(Console.ReadLine());
            return setwon;
        }
        static int LiczbaSetowB()
        {
            Console.Write("Sets won by guests team ");
            int setwon = int.Parse(Console.ReadLine());
            return setwon;
        }
        
        public void WprowadzMecz()
        {
            bool wynik = true;
            int indeksDruzynyA = 0;
            int indeksDruzynyB = 0;
            int setyDruzynyA = 0;
            int setyDruzynyB = 0;
            var nazwyDruzyn = new List<string>();
            for (int i = 0; i < myTeams.Count; i++)
            {
                nazwyDruzyn.Add(myTeams[i].TeamName);
            }

           

            int numDays = (nazwyDruzyn.Count - 1);
            int halfSize = nazwyDruzyn.Count / 2;

            List<string> teams = new List<string>();

            teams.AddRange(nazwyDruzyn.Skip(halfSize).Take(halfSize));
            teams.AddRange(nazwyDruzyn.Skip(1).Take(halfSize - 1).ToArray().Reverse());

            int teamsSize = teams.Count;
            if (kolejka <= (myTeams.Count - 1) * 2)
            {
               
                    for (int day = kolejka - 1; day < kolejka; day++)
                    {
                        int indeksdruzyny = day % teamsSize;
                        indeksDruzynyA = 0;
                        indeksDruzynyB = ZnajdzIndeksDruzyny(teams[indeksdruzyny]);
                        Console.WriteLine("Kolejka {0}", (day + 1));
                        while (wynik)
                        {

                            Console.WriteLine("{0} vs {1}", nazwyDruzyn[0], teams[indeksdruzyny]);
                            setyDruzynyA = LiczbaSetowA();
                            setyDruzynyB = LiczbaSetowB();
                            
                            if (setyDruzynyA > 3 || setyDruzynyA < 0 || setyDruzynyB > 3 || setyDruzynyB < 0 || setyDruzynyB == setyDruzynyA)
                            {
                                Console.WriteLine("Niepoprawny wynik meczu");
                                wynik = true;
                                
                            }
                            else
                            {
                                
                                wynik = false;
                                Mecz(setyDruzynyA, setyDruzynyB, indeksDruzynyA, indeksDruzynyB);
                            }
                            
                        }
                        wynik = true;
                        
                        for (int indeks = 1; indeks < halfSize; indeks++)
                        {
                            int homeTeam = (day + indeks) % teamsSize;
                            int awayTeam = (day + teamsSize - indeks) % teamsSize;
                            while (wynik)
                            {
                                
                                
                                indeksDruzynyA = ZnajdzIndeksDruzyny(teams[homeTeam]);
                                indeksDruzynyB = ZnajdzIndeksDruzyny(teams[awayTeam]);
                                Console.WriteLine("{0} vs {1}", teams[homeTeam], teams[awayTeam]);
                                setyDruzynyA = LiczbaSetowA();
                                setyDruzynyB = LiczbaSetowB();
                                if (setyDruzynyA > 3 || setyDruzynyA < 0 || setyDruzynyB > 3 || setyDruzynyB < 0 || setyDruzynyB == setyDruzynyA)
                                {
                                    Console.WriteLine("Niepoprawny wynik meczu");
                                    wynik = true;
                                }
                                else
                                {
                                    wynik = false;
                                    Mecz(setyDruzynyA, setyDruzynyB, indeksDruzynyA, indeksDruzynyB);


                                }
                            } 
                            
                            

                            

                        }
                    }
                    
                
                
            }
            else
            {
                Console.WriteLine("Nie ma wiecej meczow do rozegrania");
                StanGry = Status.Zakonczono;
            }
            kolejka++;
        }
      
  
      // Zastosowany algorytm RoundRobin dla parzystej liczby druzyn   
        public void WyswietlTerminarz()

        {
            var nazwyDruzyn = new List<string>();
            for (int i = 0;i<myTeams.Count;i++)
            {
                nazwyDruzyn.Add(myTeams[i].TeamName);
            }
            
            

            int numDays = (nazwyDruzyn.Count - 1);
            int halfSize = nazwyDruzyn.Count / 2;

            List<string> teams = new List<string>();

            teams.AddRange(nazwyDruzyn.Skip(halfSize).Take(halfSize));
            teams.AddRange(nazwyDruzyn.Skip(1).Take(halfSize - 1).ToArray().Reverse());

            int teamsSize = teams.Count;

            for (int day = 0; day < numDays; day++)
            {
                Console.WriteLine("Day {0}", (day + 1));

                int teamIdx = day % teamsSize;

                Console.WriteLine("{0} vs {1}", nazwyDruzyn[0], teams[teamIdx]);
                

                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (day + idx) % teamsSize;
                    int secondTeam = (day + teamsSize - idx) % teamsSize;
                    Console.WriteLine("{0} vs {1}", teams[firstTeam], teams[secondTeam]);
                }
            }

            

            for (int day = 0; day < numDays; day++)
            {
                Console.WriteLine("Day {0}", (day + 1 +numDays));

                int teamIdx = day % teamsSize;

                Console.WriteLine("{0} vs {1}",  teams[teamIdx], nazwyDruzyn[0]);

                for (int idx = 1; idx < halfSize; idx++)
                {
                    int firstTeam = (day + idx) % teamsSize;
                    int secondTeam = (day + teamsSize - idx) % teamsSize;
                    Console.WriteLine("{0} vs {1}",  teams[secondTeam], teams[firstTeam]);
                }
            }
          


        }
        
        private void Mecz(int setyDruzynyA, int setyDruzynyB, int indeksDruzynyA, int indeksDruzynyB)
        {

            Wyniki wynik = new Wyniki(myTeams[indeksDruzynyA].TeamName, myTeams[indeksDruzynyB].TeamName, setyDruzynyA, setyDruzynyB);
            historia.Add(wynik);

            myTeams[indeksDruzynyA].LMeczow += 1;
            myTeams[indeksDruzynyB].LMeczow += 1;
            myTeams[indeksDruzynyA].LiczbaSetow += setyDruzynyA + setyDruzynyB;
            myTeams[indeksDruzynyB].LiczbaSetow += setyDruzynyA + setyDruzynyB;
            if (setyDruzynyA == 3 && setyDruzynyB == 0 || setyDruzynyB == 1)
            {
                TrzyPktDlaGospodarzy(indeksDruzynyA, indeksDruzynyB, setyDruzynyA, setyDruzynyB);

            }
            if (setyDruzynyB == 3 && setyDruzynyA == 0 || setyDruzynyA == 1)
            {
                TrzyPktDlaGosci(indeksDruzynyA, indeksDruzynyB, setyDruzynyA, setyDruzynyB);

            }
            if (setyDruzynyA == 3 && setyDruzynyB == 2)
            {
                DwaPunktyDlaGospodarzy(indeksDruzynyA, indeksDruzynyB, setyDruzynyA, setyDruzynyB);



            }
            if (setyDruzynyB == 3 && setyDruzynyA == 2)
            {
                DwaPunktyDlaGosci(indeksDruzynyA, indeksDruzynyB, setyDruzynyA, setyDruzynyB);


            }







        }
        private void TrzyPktDlaGospodarzy(int indeksDruzynyA, int indeksDruzynyB, int setyDruzynyA, int setyDruzynyB)
        {
            myTeams[indeksDruzynyA].MeczeWygrane += 1;
            myTeams[indeksDruzynyA].SetyWygrane += setyDruzynyA;
            myTeams[indeksDruzynyA].SetyPrzegrane += setyDruzynyB;
            myTeams[indeksDruzynyA].LPunktow += 3;
            myTeams[indeksDruzynyB].SetyWygrane += setyDruzynyB;
            myTeams[indeksDruzynyB].SetyPrzegrane += setyDruzynyA;
            myTeams[indeksDruzynyB].MeczePrzegrane += 1;
            Console.WriteLine($"{myTeams[indeksDruzynyA].TeamName} {setyDruzynyA} : {setyDruzynyB}  {myTeams[indeksDruzynyB].TeamName}");
            
        }
        private void TrzyPktDlaGosci(int indeksDruzynyA, int indeksDruzynyB, int setyDruzynyA, int setyDruzynyB)
        {
            myTeams[indeksDruzynyB].MeczeWygrane += 1;
            myTeams[indeksDruzynyB].SetyWygrane += setyDruzynyB;
            myTeams[indeksDruzynyB].SetyPrzegrane += setyDruzynyA;
            myTeams[indeksDruzynyB].LPunktow += +3;
            myTeams[indeksDruzynyA].SetyWygrane += setyDruzynyA;
            myTeams[indeksDruzynyA].SetyPrzegrane += setyDruzynyB;
            myTeams[indeksDruzynyA].MeczePrzegrane += 1;
            Console.WriteLine($"{myTeams[indeksDruzynyA].TeamName} {setyDruzynyA} : {setyDruzynyB}  {myTeams[indeksDruzynyB].TeamName}");
        }
        private void DwaPunktyDlaGospodarzy(int indeksDruzynyA, int indeksDruzynyB, int setyDruzynyA, int setyDruzynyB)
        {
            myTeams[indeksDruzynyA].MeczeWygrane += 1;
            myTeams[indeksDruzynyA].SetyWygrane += setyDruzynyA;
            myTeams[indeksDruzynyA].SetyPrzegrane += setyDruzynyB;
            myTeams[indeksDruzynyA].LPunktow += 2;
            myTeams[indeksDruzynyB].LPunktow += 1;
            myTeams[indeksDruzynyB].SetyWygrane += setyDruzynyB;
            myTeams[indeksDruzynyB].SetyPrzegrane += setyDruzynyA;
            myTeams[indeksDruzynyB].MeczePrzegrane += 1;
            Console.WriteLine($"{myTeams[indeksDruzynyA].TeamName} {setyDruzynyA} : {setyDruzynyB}  {myTeams[indeksDruzynyB].TeamName}");
        }
        private void DwaPunktyDlaGosci (int indeksDruzynyA, int indeksDruzynyB, int setyDruzynyA, int setyDruzynyB)
        {
            myTeams[indeksDruzynyB].MeczeWygrane += 1;
            myTeams[indeksDruzynyB].SetyWygrane += setyDruzynyB;
            myTeams[indeksDruzynyB].SetyPrzegrane += setyDruzynyA;
            myTeams[indeksDruzynyB].LPunktow += 2;
            myTeams[indeksDruzynyA].LPunktow += 1;
            myTeams[indeksDruzynyA].SetyWygrane += setyDruzynyA;
            myTeams[indeksDruzynyA].SetyPrzegrane += setyDruzynyB;
            myTeams[indeksDruzynyA].MeczePrzegrane += 1;
            Console.WriteLine($"{myTeams[indeksDruzynyA].TeamName} {setyDruzynyA} : {setyDruzynyB}  {myTeams[indeksDruzynyB].TeamName}");
        }

        private void CofnijTrzyPunktyGospodarzom(int indeksDruzynyA, int indeksDruzynyB, int setyDruzynyA, int setyDruzynyB)
        {
            myTeams[indeksDruzynyA].MeczeWygrane -= 1;
            myTeams[indeksDruzynyA].SetyWygrane -= setyDruzynyA;
            myTeams[indeksDruzynyA].SetyPrzegrane -= setyDruzynyB;
            myTeams[indeksDruzynyA].LPunktow -= 3;
            myTeams[indeksDruzynyB].SetyWygrane -= setyDruzynyB;
            myTeams[indeksDruzynyB].SetyPrzegrane -= setyDruzynyA;
            myTeams[indeksDruzynyB].MeczePrzegrane -= 1;
            Console.WriteLine($" Wynik meczu {myTeams[indeksDruzynyA].TeamName} {setyDruzynyA} : {setyDruzynyB}  {myTeams[indeksDruzynyB].TeamName} zostal usuniety");
        }
        private void CofnijTrzyPunktyGosciom(int indeksDruzynyA, int indeksDruzynyB, int setyDruzynyA, int setyDruzynyB)
        {
            myTeams[indeksDruzynyB].MeczeWygrane -= 1;
            myTeams[indeksDruzynyB].SetyWygrane -= setyDruzynyB;
            myTeams[indeksDruzynyB].SetyPrzegrane -= setyDruzynyA;
            myTeams[indeksDruzynyB].LPunktow -= +3;
            myTeams[indeksDruzynyA].SetyWygrane -= setyDruzynyA;
            myTeams[indeksDruzynyA].SetyPrzegrane -= setyDruzynyB;
            myTeams[indeksDruzynyA].MeczePrzegrane -= 1;
            Console.WriteLine($" Wynik meczu {myTeams[indeksDruzynyA].TeamName} {setyDruzynyA} : {setyDruzynyB}  {myTeams[indeksDruzynyB].TeamName} zostal usuniety");
        }
           
        private void CofnijDwaPunktyGospodarzom(int indeksDruzynyA, int indeksDruzynyB, int setyDruzynyA, int setyDruzynyB)
        {
            myTeams[indeksDruzynyA].MeczeWygrane -= 1;
            myTeams[indeksDruzynyA].SetyWygrane -= setyDruzynyA;
            myTeams[indeksDruzynyA].SetyPrzegrane -= setyDruzynyB;
            myTeams[indeksDruzynyA].LPunktow -= 2;
            myTeams[indeksDruzynyB].LPunktow -= 1;
            myTeams[indeksDruzynyB].SetyWygrane -= setyDruzynyB;
            myTeams[indeksDruzynyB].SetyPrzegrane -= setyDruzynyA;
            myTeams[indeksDruzynyB].MeczePrzegrane -= 1;
            Console.WriteLine($" Wynik meczu {myTeams[indeksDruzynyA].TeamName} {setyDruzynyA} : {setyDruzynyB}  {myTeams[indeksDruzynyB].TeamName} zostal usuniety");
        }
        private void CofnijDwaPunktyGosciom(int indeksDruzynyA, int indeksDruzynyB, int setyDruzynyA, int setyDruzynyB)
        {
            myTeams[indeksDruzynyB].MeczeWygrane -= 1;
            myTeams[indeksDruzynyB].SetyWygrane -= setyDruzynyB;
            myTeams[indeksDruzynyB].SetyPrzegrane -= setyDruzynyA;
            myTeams[indeksDruzynyB].LPunktow -= 2;
            myTeams[indeksDruzynyA].LPunktow -= 1;
            myTeams[indeksDruzynyA].SetyWygrane -= setyDruzynyA;
            myTeams[indeksDruzynyA].SetyPrzegrane -= setyDruzynyB;
            myTeams[indeksDruzynyA].MeczePrzegrane -= 1;
            Console.WriteLine($" Wynik meczu {myTeams[indeksDruzynyA].TeamName} {setyDruzynyA} : {setyDruzynyB}  {myTeams[indeksDruzynyB].TeamName} zostal usuniety");
        }
        private void ZmienMecz(int setyDruzynyA, int setyDruzynyB, int indeksDruzynyA, int indeksDruzynyB)
        {

            

            myTeams[indeksDruzynyA].LMeczow += 1;
            myTeams[indeksDruzynyB].LMeczow += 1;
            myTeams[indeksDruzynyA].LiczbaSetow += setyDruzynyA + setyDruzynyB;
            myTeams[indeksDruzynyB].LiczbaSetow += setyDruzynyA + setyDruzynyB;
            if (setyDruzynyA == 3 && setyDruzynyB == 0 || setyDruzynyB == 1)
            {
                TrzyPktDlaGospodarzy(indeksDruzynyA, indeksDruzynyB, setyDruzynyA, setyDruzynyB);

            }
            if (setyDruzynyB == 3 && setyDruzynyA == 0 || setyDruzynyA == 1)
            {
                TrzyPktDlaGosci(indeksDruzynyA, indeksDruzynyB, setyDruzynyA, setyDruzynyB);

            }
            if (setyDruzynyA == 3 && setyDruzynyB == 2)
            {
                DwaPunktyDlaGospodarzy(indeksDruzynyA, indeksDruzynyB, setyDruzynyA, setyDruzynyB);



            }
            if (setyDruzynyB == 3 && setyDruzynyA == 2)
            {
                DwaPunktyDlaGosci(indeksDruzynyA, indeksDruzynyB, setyDruzynyA, setyDruzynyB);


            }







        }
        public void WyswietlStatystykiDruzyny(int indeks)
        {
            Console.WriteLine($"Nazwa druzyny: {myTeams[indeks].TeamName}");
            Console.WriteLine($"Liczba punktow: {myTeams[indeks].LPunktow}");
            Console.WriteLine($"Liczba rozegranych meczow: {myTeams[indeks].LMeczow}");
            Console.WriteLine($"Mecze wygrane: {myTeams[indeks].MeczeWygrane}");
            Console.WriteLine($"Mecze przegrane: {myTeams[indeks].MeczePrzegrane}");
            Console.WriteLine($"Liczba setow: {myTeams[indeks].LiczbaSetow}");
            Console.WriteLine($"Sety wygrane: {myTeams[indeks].SetyWygrane}");
            Console.WriteLine($"Sety przegrane: {myTeams[indeks].SetyPrzegrane}");
        }
        public void KreatorLigi(int iloscdruzyn)
        {
            if (iloscdruzyn % 2 == 0 && iloscdruzyn<=20)
            {
                for (int i = 0; i < iloscdruzyn; i++)
                {
                    Console.Write($"Podaj nazwe druzyny nr {i + 1}: ");
                    string nazwa = Console.ReadLine();
                    DodajDruzyne(nazwa);
                }

            }
            else
            {

                Console.WriteLine("Liczba drużyn musi byc: \n - parzysta \n - nie wieksza niz 20");
                Console.WriteLine("Sprobuj jeszcze raz ...");
                Environment.Exit(0);
            }
            
        }
        public void WyswietlWyniki()



        {
            for (int i = 0; i < RozegraneMecze.Count; i++)
            {
                Console.WriteLine($"i.m ->{i} {historia[i]}");
            }
        }
        public string ZnajdzZwyciezce()
        {
            string zwyciezca = "";
            
            if (StanGry == Status.Zakonczono)
            {
                Team max = new Team("Najlepszy");
                Console.WriteLine("Zwyciezca ligi jest:");
                List<Team> podium = new List<Team>();
                podium = myTeams;
                
                podium.Add(max);

                for (int i = 0; i < podium.Count; i++)
                {
                    if (max.LPunktow < podium[i].LPunktow)
                    {
                        max = podium[i];
                        zwyciezca = max.TeamName;
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Liga jeszcze trwa");
            }
            return zwyciezca;
        }

        public void ZmienWynikMeczu(int indeksMeczu)
        {
            
            int gospodarz = ZnajdzIndeksDruzyny(historia[indeksMeczu].DruzynaGospodarzy);
            int gosc = ZnajdzIndeksDruzyny(historia[indeksMeczu].DruzynaGosci);

            myTeams[gospodarz].LMeczow -= 1;
            myTeams[gosc].LMeczow -= 1;
            int setyDruzynyA = historia[indeksMeczu].LiczbaSetowGospodarzy;
            int setyDruzynyB = historia[indeksMeczu].LiczbaSetowGosci;
            myTeams[gospodarz].LiczbaSetow -= historia[indeksMeczu].LiczbaSetowGospodarzy + historia[indeksMeczu].LiczbaSetowGosci;
            myTeams[gosc].LiczbaSetow -= historia[indeksMeczu].LiczbaSetowGosci + setyDruzynyB;
            if (setyDruzynyA == 3 && setyDruzynyB == 0 || setyDruzynyB == 1)
            {
                CofnijTrzyPunktyGospodarzom(gospodarz, gosc, setyDruzynyA, setyDruzynyB);                             
            }
            if (setyDruzynyB == 3 && setyDruzynyA == 0 || setyDruzynyA == 1)
            {
                CofnijTrzyPunktyGosciom(gospodarz, gosc, setyDruzynyA, setyDruzynyB);                              
            }
            if (setyDruzynyA == 3 && setyDruzynyB == 2)
            {
                CofnijDwaPunktyGospodarzom(gospodarz, gosc, setyDruzynyA, setyDruzynyB);
            }
            if (setyDruzynyB == 3 && setyDruzynyA == 2)
            {
               CofnijDwaPunktyGosciom(gospodarz, gosc, setyDruzynyA, setyDruzynyB);    
            }
            
            Console.WriteLine("Wprowadz nowy wynik:");
            bool wynik = true;
            while (wynik)
            {
                setyDruzynyA = LiczbaSetowA();
                setyDruzynyB = LiczbaSetowB();
                historia[indeksMeczu].LiczbaSetowGospodarzy = setyDruzynyA;
                historia[indeksMeczu].LiczbaSetowGosci = setyDruzynyB;
                if (setyDruzynyA > 3 || setyDruzynyA < 0 || setyDruzynyB > 3 || setyDruzynyB < 0 || setyDruzynyB == setyDruzynyA)
                {
                    Console.WriteLine("Niepoprawny wynik meczu");
                }
                else
                {
                    wynik = false;
                    ZmienMecz(setyDruzynyA, setyDruzynyB, gospodarz, gosc);
                }
            }
            

            
            

            
        }
        
       

        public enum Status { Wtrakcie, Zakonczono}
        
            
        






    }
}






        
    



        




       
















    


   


