namespace GestoreEventi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Prettifier("Benvenuto in GestoreEventi!"));
            ProgrammaEventi programma = new("Prova");
            bool control = true;
            while (control)
            {
                try
                {
                    Console.Write("Inserisci il nome del programma: ");
                    string titolo = Console.ReadLine();
                    programma = new ProgrammaEventi(titolo);
                    control = false;
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            control = true;

            while (control)
            {
                try
                {
                    Console.Write("Inserisci il numero di eventi da aggiungere al programma: ");
                    int numEventi = int.Parse(Console.ReadLine());
                    if (numEventi <= 0) throw new Exception("ERRORE: il numero di eventi da aggiungere non puo essere negativo o nullo");
                    Console.WriteLine("\n");
                    for (int i = 0; i < numEventi; i++)
                    {
                        Console.WriteLine(Prettifier("Inserisci l'evento numero " + (i + 1)));
                        int eventType = -1;
                        while (control)
                        {
                            try
                            {
                                Console.WriteLine("Seleziona una tipologia di evento:");
                                Console.WriteLine("1. Conferenza");
                                Console.WriteLine("2. Evento generico");
                                eventType = int.Parse(Console.ReadLine());
                                if (eventType < 1 || eventType > 2) throw new Exception("ERRORE: la tipologia di evento selezionata non esiste");
                                switch (eventType)
                                {
                                    // istanzio prima gli eventi in modo da lanciare subito le relative eccezioni quando viene chiamata la funzione InserisciEvento()
                                    case 1:
                                        programma.AggiungiEvento(InserisciEvento(new Conferenza("asd", DateTime.MaxValue, 1, "asd", 0)));
                                        break;
                                    case 2:
                                        programma.AggiungiEvento(InserisciEvento(new Evento("asd", DateTime.MaxValue, 1)));
                                        break;
                                    default:
                                        break;
                                }
                                control = false;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                        control = true;

                       
                        Console.WriteLine("\n");
                    }
                    control = false;
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            control = true;
            Console.WriteLine(Prettifier($"Eventi del programma: {programma.NumeroEventi()}"));
            if (programma.NumeroEventi() > 0) Console.WriteLine(programma);
            else Console.WriteLine("Nessun evento presente");

            while (control)
            {
                try
                {
                    Console.Write("\nInserisci una data per sapere che eventi ci saranno (gg/mm/aaaa): ");
                    DateTime data = DateTime.Parse(Console.ReadLine());
                    List<Evento> eventi = programma.EventiData(data);
                    Console.WriteLine("\n");
                    Console.WriteLine(Prettifier($"Eventi del {data:dd/MM/yyyy}: {eventi.Count}"));
                    if (eventi.Count > 0)ProgrammaEventi.StampaEventi(eventi);
                    else Console.WriteLine("Nessun evento presente nella data selezionata");
                    control = false;
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            control = true;
            programma.SvuotaEventi();

            Console.ReadKey();
        }

        static Evento InserisciEvento(Evento evento) 
        {
            bool control = true;
            

            string titolo = "";
            DateTime data = DateTime.Now;
            string relatore = "";
            double prezzo = 0;
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

            // caso conferenza
            if (evento is Conferenza conferenza)
            {
                while (control)
                {
                    try
                    {
                        Console.Write("Inserisci il nome del relatore: ");
                        relatore = Console.ReadLine();
                        conferenza.Relatore = relatore;
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
                        Console.Write("Inserisci il prezzo del biglietto: ");
                        prezzo = double.Parse(Console.ReadLine());
                        conferenza.Prezzo = prezzo;
                        control = false;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                control = true;
            }

            while (control)
            {
                try
                {
                    Console.Write("Inserisci il numero massimo di posti: ");
                    maxPosti = int.Parse(Console.ReadLine());
                    if (evento is Evento e)
                    {
                        e = new Evento(titolo, data, maxPosti);
                    } else if (evento is Conferenza c)
                    {
                        c = new Conferenza(titolo, data, maxPosti, relatore, prezzo);
                    }
                    control = false;
                }
                catch (Exception e)
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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            control = true;
            Console.WriteLine($"Posti Prenotati: {evento.PostiPrenotati}");
            Console.WriteLine($"Posti Disponibili: {evento.MaxPosti - evento.PostiPrenotati}");

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
                            Console.WriteLine($"Posti Prenotati: {evento.PostiPrenotati}");
                            Console.WriteLine($"Posti Disponibili: {evento.MaxPosti - evento.PostiPrenotati}");
                            break;
                        case 'n':
                            control = false;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            control = true;


            return evento;
        }

        private static string Prettifier(string input)
              => $"{String.Concat(Enumerable.Repeat("-", input.Length + 2))} \n {input.ToUpper()} \n{String.Concat(Enumerable.Repeat("-", input.Length + 2))}";
    }
}
