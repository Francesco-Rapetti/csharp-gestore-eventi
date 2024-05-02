using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreEventi
{
    internal class ProgrammaEventi
    {
        private string _titolo;
        public string Titolo { get { return _titolo; } 
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("ERRORE: il titolo non puo essere vuoto");
                _titolo = value;
            } 
        }
        public List<Evento> Eventi { get; set; }

        public ProgrammaEventi(string titolo)
        {
            this.Titolo = titolo;
            this.Eventi = new List<Evento>();
        }

        public void AggiungiEvento(Evento evento) => this.Eventi.Add(evento);
        public List<Evento> EventiData(DateTime data) => this.Eventi.Where(e => e.Data == data).ToList();
        public static string StampaEventi(List<Evento> eventi, bool consoleLog = true)
        {
            //string output = "";
            //foreach (var evento in eventi)
            //{
            //    output += $"{evento}\n";
            //}
            string output = String.Join("\n", eventi);
            if (consoleLog) Console.WriteLine(output);
            return output;
        }
        public int NumeroEventi() => this.Eventi.Count;
        public void SvuotaEventi() => this.Eventi.Clear();
        public override string ToString() => $"{this.Titolo}\n\t{string.Join("\n\t", StampaEventi(this.Eventi, false).Split("\n"))}";
    }
}
