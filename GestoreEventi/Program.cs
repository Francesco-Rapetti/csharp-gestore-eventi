using ConsoleTables;

namespace GestoreEventi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool control = true;
            // evento creato per sollevare eccezioni dopo ogni singolo step
            Evento evento = new Evento("Prova", DateTime.MaxValue, 10);
            string titolo = "";
            DateTime data = DateTime.Now;
            int maxPosti;

            // fase di input
            while (control)
            {
                try
                {
                    Console.Write("Inserisci il nome dell'evento: ");
                    titolo = Console.ReadLine();
                    evento.Titolo = titolo;
                    control = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            control = true;

            while (control)
            {
                try
                {
                    Console.Write("Inserisci la data dell'evento (gg/mm/aaaa): ");
                    data = DateTime.Parse(Console.ReadLine());
                    evento.Data = data;
                    control = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            control = true;

            while (control)
            {
                try
                {
                    Console.Write("Inserisci il numero massimo di posti: ");
                    maxPosti = int.Parse(Console.ReadLine());
                    evento = new Evento(titolo, data, maxPosti);
                    control = false;
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            control = true;

            // prenotazione posti
            int postiPrenotati;
            while (control)
            {
                try
                {
                    Console.Write("Vuoi effettuare una prenotazione? (s/n): ");
                    char risposta = char.Parse(Console.ReadLine());
                    switch (risposta)
                    {
                        case 's':
                            Console.Write("Inserisci il numero di posti da prenotare: ");
                            postiPrenotati = int.Parse(Console.ReadLine());
                            evento.PrenotaPosti(postiPrenotati);
                            control = false;
                            break;
                        case 'n':
                            control = false;
                            break;
                        default:
                            break;
                    }
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            control = true;
            var table = new ConsoleTable("Data", "Titolo", "Posti prenotati", "Posti disponibili");
            table.AddRow(evento.Data.ToString("dd/MM/yyyy"), evento.Titolo, evento.PostiPrenotati, evento.MaxPosti - evento.PostiPrenotati);
            Console.WriteLine(table);

            // disdetta posti
            while (control)
            {
                if (evento.PostiPrenotati == 0) break;
                try
                {
                    Console.Write("Vuoi disdire dei posti? (s/n): ");
                    char risposta = char.Parse(Console.ReadLine());
                    switch (risposta)
                    {
                        case 's':
                            Console.Write("Inserisci il numero di posti da disdire: ");
                            int postiDisdetti = int.Parse(Console.ReadLine());
                            evento.DisdiciPosti(postiDisdetti);
                            break;
                        case 'n':
                            control = false;
                            break;
                        default:
                            break;
                    }
                    table = new ConsoleTable("Data", "Titolo", "Posti prenotati", "Posti disponibili");
                    table.AddRow(evento.Data.ToString("dd/MM/yyyy"), evento.Titolo, evento.PostiPrenotati, evento.MaxPosti - evento.PostiPrenotati);
                    Console.WriteLine(table);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            control = true;
        }
    }
}
