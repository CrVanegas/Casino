using DataAccess;
using Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class RouletteBL : ICasino, IRoulette
    {
        ImplementDB implementDB;
        const string ROULETTE_DB_KEY = "Roulette";
        const string BET_DB_KEY = "BET";

        public RouletteBL(string DBServer)
        {
            implementDB = new ImplementDB(new RedisHelper(DBServer));
        }

        public string Create()
        {
            try {
                Roulette roulette = new Roulette();
                List<string> roulettes = JsonConvert.DeserializeObject<List<string>>(implementDB.GetValue($"{ROULETTE_DB_KEY}") ?? "[]");
                roulette.Create();
                implementDB.SetValue($"{roulette.Id}{ROULETTE_DB_KEY}", JsonConvert.SerializeObject(roulette));
                roulettes.Add(roulette.Id);
                implementDB.SetValue(ROULETTE_DB_KEY, JsonConvert.SerializeObject(roulettes));

                return roulette.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Ocurrió un error creando la ruleta";
            }
        }

        public string Open(string id)
        {
            try
            {
                Roulette roulette = JsonConvert.DeserializeObject<Roulette>(implementDB.GetValue($"{id}{ROULETTE_DB_KEY}"));
                roulette.Open();
                implementDB.SetValue($"{id}{ROULETTE_DB_KEY}", JsonConvert.SerializeObject(roulette));

                return "Apertura Existosa";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Apertura Denegada";
            }
        }

        public string Bet(string id, Bet bet, double maxAmount, string[] rangeNumbersBet, string[] allowedColors)
        {
            try
            {
                Roulette roulette = JsonConvert.DeserializeObject<Roulette>(implementDB.GetValue($"{id}{ROULETTE_DB_KEY}"));
                List<Bet> bets = JsonConvert.DeserializeObject<List<Bet>>(implementDB.GetValue($"{id}{BET_DB_KEY}") ?? "[]");
                if (roulette.Bet(bet) && ValidateBet(bet, maxAmount, rangeNumbersBet.Select(int.Parse).ToArray(), allowedColors))
                {
                    bets.Add(bet);
                    implementDB.SetValue($"{id}{BET_DB_KEY}", JsonConvert.SerializeObject(bets));
                    return "Apuesta Registrada Correctamente";
                }
                return "No se pudo registrar la apuesta";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return "Ocurrió un error realizando la apuesta";
            }
        }

        public List<Bet> Closed(string id)
        {
            try
            {
                Roulette roulette = JsonConvert.DeserializeObject<Roulette>(implementDB.GetValue($"{id}{ROULETTE_DB_KEY}"));
                List<Bet> bets = JsonConvert.DeserializeObject<List<Bet>>(implementDB.GetValue($"{id}{BET_DB_KEY}") ?? "[]");
                roulette.Closed();
                implementDB.SetValue($"{id}{ROULETTE_DB_KEY}", JsonConvert.SerializeObject(roulette));

                return bets;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Roulette> GetRoulettes()
        {
            List<Roulette> roulettes = new List<Roulette>();
            List<string> rouletteIds = JsonConvert.DeserializeObject<List<string>>(implementDB.GetValue(ROULETTE_DB_KEY) ?? "[]");
            foreach(string id in rouletteIds)
            {
                roulettes.Add(JsonConvert.DeserializeObject<Roulette>(implementDB.GetValue($"{id}{ROULETTE_DB_KEY}")));
            }
            return roulettes;
        }

        private bool ValidateBet(Bet bet, double maxAmount, int[] rangeNumbersBet, string[] allowedColors)
        {
            int betNumber; 
            if (!bet.ValidateMaxAmount(maxAmount))
                return false;
            if(Int32.TryParse(bet.Name, out betNumber))
            {
                if (!bet.ValidateNumber(rangeNumbersBet, betNumber))
                    return false;
            }
            else
            {
                return allowedColors.Contains(bet.Name);
            }
            return true;
        }
    }
}
