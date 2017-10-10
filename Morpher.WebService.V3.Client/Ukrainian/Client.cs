﻿using System;

namespace Morpher.WebService.V3.Ukrainian
{
    using System.Globalization;

    public class Client
    {
        readonly Func<MyWebClient> _newClient;

        internal Client(Func<MyWebClient> newClient)
        {
            _newClient = newClient;
        }

        public DeclensionResult Parse(string lemma)
        {
            using (var client = _newClient())
            {
                client.AddParam("s", lemma);

                var declensionResult = client.GetObject<DeclensionResult>("/ukrainian/declension");

                declensionResult.Nominative = lemma;

                return declensionResult;
            }
        }

        public NumberSpellingResult Spell(decimal number, string unit)
        {
            using (var client = _newClient())
            {
                client.AddParam("n", number.ToString(new CultureInfo("en-US")));
                client.AddParam("unit", unit);

                return client.GetObject<NumberSpellingResult>("/ukrainian/spell");
            }
        }
    }
}
