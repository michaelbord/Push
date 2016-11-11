using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebPush.Models;

namespace WebPush.Core
{
    public static class PushRepository
    {
        public static IEnumerable<PushButton> AllPushButtons { get; private set; }

        private static Dictionary<int, List<PushButton>> Customers { get; set;  }

        public static ConcurrentBag<KeyValuePair<DateTime, Guid>> AllPushings { get; private set;  }


        static PushRepository()
        {
            Initialize();
        }

        private static void Initialize()
        {
            AllPushButtons = new PushButton[]
            {
                new PushButton { Id = Guid.Parse("5C29EAE0-F716-449F-B980-51C8E9771ED7") },
                new PushButton { Id = Guid.Parse("60572434-8961-43E0-9CDE-9A32B349668C") },
                new PushButton { Id = Guid.Parse("7B504656-B96E-4681-9B53-3E9E30FB47A9") }
            };

            Customers = new Dictionary<int, List<PushButton>>();
        }

        public static IEnumerable<PushButton> GetCustomerPushButton(int idCustomer)
        {
            return Customers[idCustomer];
        }

        public static bool Pair(int idCustomer, Guid pushButtonId)
        {
            // Est-ce que le client existe ? Si non, on le crée.
            if (!Customers.ContainsKey(idCustomer))
            {
                Customers.Add(idCustomer, new List<PushButton>());
            }

            // Est-ce que le bouton existe ?
            PushButton button = AllPushButtons.FirstOrDefault(p => p.Id == pushButtonId);
            if (button == null)
            {
                return false;
            }

            // Est-ce que le bouton est déjà associé à un compte client ?
            if (Customers.Values.SelectMany(p => p).Any(p  => p.Id == pushButtonId))
            {
                return false;
            }

            // On associe le bouton au client.
            Customers[idCustomer].Add(button);
            return true;
        }

        public static bool Unpair(int idCustomer, Guid pushButtonId)
        {
            // Est-ce que le client existe ?
            if (!Customers.ContainsKey(idCustomer))
            {
                return false;
            }

            PushButton button = Customers[idCustomer].FirstOrDefault(p => p.Id == pushButtonId);
            if (button == null)
            {
                return false;
            }

            Customers[idCustomer].Remove(button);
            return true;
        }

        public static void SavePush(Guid pushButton)
        {
            // A quel compte client le bouton est associé ?


        }
    }
}
