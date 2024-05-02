using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreEventi
{
    internal class Conferenza : Evento
    {
        private string _relatore;
        private double _prezzo;
        public string Relatore 
        {
            get { return _relatore; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("ERRORE: il relatore non puo essere vuoto");
                _relatore = value;
            }
        }
        public double Prezzo
        {
            get { return _prezzo; }
            set
            {
                if (value < 0)
                    throw new Exception("ERRORE: il prezzo non puo essere negativo");
                _prezzo = value;
            }
        }

        public Conferenza(string titolo, DateTime data, int maxPosti, string relatore, double prezzo) : base(titolo, data, maxPosti)
        {
            this.Relatore = relatore;
            this.Prezzo = prezzo;
        }

        public override string ToString() => $"{base.ToString()} - {Relatore} - {Prezzo:0.00} euro";
    }
}
