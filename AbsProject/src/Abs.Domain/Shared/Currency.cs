using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abs.Domain.Shared
{
    public record Currency
    {
        internal static readonly Currency None = new("");
        public static readonly Currency Usd = new("USD");
        public static readonly Currency Eur = new("EUR");



        private Currency(string code) => Code = code;

        public string Code { get; init; }

        public static Currency FromCode(string code)
        {
            return CurrencyList.FirstOrDefault(c => c.Code == code) ?? throw new ApplicationException("Currency code is invalid");
        }

        public static readonly IReadOnlyCollection<Currency> CurrencyList = new[]
        {
            Usd,
            Eur
        };
    }
}
