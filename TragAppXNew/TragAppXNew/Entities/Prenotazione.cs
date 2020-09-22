using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TragAppXNew.Entities
{
    public class Prenotazione
    {
        public int oraAnd { get; set; }
        public int minutiAnd { get; set; }
        public int oraRit { get; set; }
        public int minutiRit { get; set; }
        public string data { get; set; }
        public int numAd { get; set; }


        public Prenotazione(int oraAnd, int minutiAnd, int oraRit, int minutiRit, string data, int numAd)
        {
            this.oraAnd = oraAnd;
            this.minutiAnd = minutiAnd;
            this.oraRit = oraRit;
            this.minutiRit = minutiRit;
            this.data = data;
            this.numAd = numAd;
        }
        public Prenotazione()
        {

        }

        public int GetOraAnd() { return oraAnd; }
        public int GetMinutiAnd() { return minutiAnd; }
        public int GetOraRit() { return oraRit; }
        public int GetMinutiRit() { return minutiRit; }
        public string GetData() { return data; }
        public int GetNumAd() { return numAd; }
    }
}