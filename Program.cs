using System;
using LigaSiatkarskaProjekt;

namespace ConsoleAppLigaSIatkarska
{
    class Program
    {

        static void Main(string[] args)
        {
            League liga = new League("Liga");                                 
                 MenuPoczatkowe();                  
             int action = int.Parse(Console.ReadLine());
            switch (action)
            {
                case 1:
                    liga.KreatorLigi( WybierzIloscDruzyn());
                    
                    do
                    {                       
                        MenuLigi();
                        int action2 = int.Parse(Console.ReadLine());
                        switch (action2)
                        {
                            case 1:                                                                
                                liga.WprowadzMecz();
                                break;
                            case 2:                                
                                liga.WyswietlTabele();                                
                                break;
                            case 3:
                                liga.WyswietlTerminarz();
                                break;
                            case 4:
                                liga.WyswietlDruzyny();
                                liga.WyswietlStatystykiDruzyny(IndeksDruzyny());
                                break;
                            case 5:
                                liga.WyswietlWyniki();
                                break;
                            case 6:
                                liga.WyswietlWyniki();
                                liga.ZmienWynikMeczu(ZmienWynikMeczu());
                                break;
                            case 7:
                                Console.WriteLine(liga.ZnajdzZwyciezce());
                                break;
                            case 8:
                                Console.WriteLine("Shutting down ...");
                                Environment.Exit(0);
                                break;

                        }
                    }
                    while (true);
                    
                case 2:
                    Instrukcje();
                    break;
                case 3:
                    Console.WriteLine("Shutting down ...");
                    Environment.Exit(0);
                    break;
                case 4:
                    OProgramie();
                    break;
                

            }
             

        }

        static void MenuPoczatkowe()
        {
            Console.WriteLine("Witaj w kreatorze ligi siatkarskiej");
            Console.WriteLine("Dostepne mozliwosci:");
            Console.WriteLine("1 - Stworz lige");
            Console.WriteLine("2 - Instrukcje");
            Console.WriteLine("3 - Exit");
            Console.WriteLine("4 - O programie");
            Console.Write("Wybierz: ");
            
        }
        private static Team KreatorLigi()
        {
            string name = Console.ReadLine();        
            return new Team(name);
        }

        
            
        static int WybierzIloscDruzyn()
        {
            Console.Write("Wybierz parzysta ilosc druzyn: ");
            int ilosc = int.Parse(Console.ReadLine());
            return ilosc;
        }
        static void MenuLigi()
        {

            Console.WriteLine("Menu");
            Console.WriteLine("Dostepne mozliwosci:");
            Console.WriteLine("1 - Dodaj wyniki nastepnej kolejki");
            Console.WriteLine("2 - Tabela");
            Console.WriteLine("3 - Terminarz");
            Console.WriteLine("4 - Pokaz statystyki druzyny");
            Console.WriteLine("5 - Wyswietl wyniki rozegranych meczow");
            Console.WriteLine("6 - Zmien wynik meczu");
            Console.WriteLine("7 - Wyswietl Zwyciezce");
            Console.WriteLine("8 - Exit");
            Console.Write("Wybierz: ");
        }
      
        static int NumerKolejki()
        {
            Console.Write("Wybierz numer kolejki: ");
            int round = int.Parse(Console.ReadLine());
            return round;
        }
        static int IndeksDruzyny()
        {
            Console.Write("Podaj indeks druzyny: ");
            int indeks = int.Parse(Console.ReadLine());
            return indeks;

        }
        static void Instrukcje()
        {
            Console.WriteLine("1. Liga nie moze zawierac nieparzystej liczby drużyn");
            Console.WriteLine("2. Nazwa druzyny : \n- ma skladac sie z minimalnie z 3 znakow, maksymalnie z 24 znaków \n- pierwsze trzy znaki nazwy druzyny powinny byc inne od pozostalych ");
            Console.WriteLine("3. Aby wyswietlic zwyciezce nalezy \n wprowadzac wyniki do momentu uzyskania komunikatu \"Nie ma wiecej meczow do rozegrania\"  ");
        }
        static void OProgramie()
        {
            Console.WriteLine("Aplikacja ma na celu stworzenie ligi, oraz prowadzenie jej przebiegu.");
                
        }
        static int ZmienWynikMeczu()
        {
            Console.Write("Podaj indeks meczu, ktorego wynik chcesz zmienic: ");
            int indeks = int.Parse(Console.ReadLine());
            return indeks;

        }
    }
}
    



