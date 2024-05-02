using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreEventi
{
    internal class Evento
    {
        public string Titolo { get { return Titolo; } 
            set
            {
                if (value == "")
                    throw new Exception("ERRORE: il titolo non puo essere vuoto");
                Titolo = value;
            } 
        }
        public DateTime Data { get { return Data; } 
            set
            {
                if (value < DateTime.Now)
                    throw new Exception("ERRORE: la data non puo essere antecedente ad oggi");
                Data = value;
            } 
        }
        public int MaxPosti { get; }
        public int PostiPrenotati { get; private set; }

        public Evento(string titolo, DateTime data, int maxPosti)
        {
            this.Titolo = titolo;
            this.Data = data;
            if (maxPosti <= 0)
                throw new Exception("ERRORE: il numero massimo di posti non puo essere negativo");
            this.MaxPosti = maxPosti;
            this.PostiPrenotati = 0;
        }

        public void PrenotaPosti(int posti)
        {
            if (this.Data < DateTime.Now)
                throw new Exception("ERRORE: l'evento ha già preso luogo");
            if (posti <= 0)
                throw new Exception("ERRORE: il numero di posti da prenotare non puo essere negativo");
            if (this.PostiPrenotati + posti > this.MaxPosti)
                throw new Exception("ERRORE: il numero di posti da prenotare supera il numero massimo di posti");
            this.PostiPrenotati += posti;
        }

        public void DisdiciPosti(int posti)
        {
            if (this.Data < DateTime.Now)
                throw new Exception("ERRORE: l'evento ha già preso luogo");
            if (posti <= 0)
                throw new Exception("ERRORE: il numero di posti da disdire non puo essere negativo");
            if (this.PostiPrenotati - posti < 0)
                throw new Exception("ERRORE: il numero di posti da disdire supera il numero di posti prenotati");
            this.PostiPrenotati -= posti;
        }

        public override string ToString() => $"{Data.ToString("dd/MM/yyyy")} - {Titolo}";
    }
}
