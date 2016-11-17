namespace WebPush.Core
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    /// <summary>
    /// Repository des push.
    /// </summary>
    public static class PushRepository
    {
        /// <summary>
        /// Gets all push buttons.
        /// </summary>
        /// <value>
        /// All push buttons.
        /// </value>
        public static IEnumerable<PushButton> AllPushButtons { get; private set; }

        /// <summary>
        /// Gets or sets the customers.
        /// </summary>
        /// <value>
        /// The customers.
        /// </value>
        public static Dictionary<int, List<PushButton>> Customers { get; set; }

        /// <summary>
        /// Gets all pushings.
        /// </summary>
        /// <value>
        /// All pushings.
        /// </value>
        public static ConcurrentBag<PushingItem> AllPushings { get; private set; }

        /// <summary>
        /// Initializes the <see cref="PushRepository"/> class.
        /// </summary>
        static PushRepository()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private static void Initialize()
        {
            AllPushButtons = new PushButton[]
            {
                new PushButton { Id = Guid.Parse("5C29EAE0-F716-449F-B980-51C8E9771ED7") },
                new PushButton { Id = Guid.Parse("60572434-8961-43E0-9CDE-9A32B349668C") },
                new PushButton { Id = Guid.Parse("7B504656-B96E-4681-9B53-3E9E30FB47A9") }
            };

            Customers = new Dictionary<int, List<PushButton>>();

            AllPushings = new ConcurrentBag<PushingItem>();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public static void Reset()
        {
            Initialize();
        }

        /// <summary>
        /// Gets the customer push button.
        /// </summary>
        /// <param name="idCustomer">The identifier customer.</param>
        /// <returns></returns>
        public static IEnumerable<PushButton> GetCustomerPushButton(int idCustomer)
        {
            // Est-ce que le client existe ? Si non, on le crée.
            if (!Customers.ContainsKey(idCustomer))
            {
                Customers.Add(idCustomer, new List<PushButton>());
            }

            return Customers[idCustomer];
        }

        /// <summary>
        /// Pairs the specified identifier customer.
        /// </summary>
        /// <param name="idCustomer">The identifier customer.</param>
        /// <param name="pushButtonId">The push button identifier.</param>
        /// <returns></returns>
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
            if (Customers.Values.SelectMany(p => p).Any(p => p.Id == pushButtonId))
            {
                return false;
            }

            // On associe le bouton au client.
            Customers[idCustomer].Add(button);
            return true;
        }

        /// <summary>
        /// Unpairs the specified identifier customer.
        /// </summary>
        /// <param name="idCustomer">The identifier customer.</param>
        /// <param name="pushButtonId">The push button identifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Saves the push.
        /// </summary>
        /// <param name="pushButtonId">The push button identifier.</param>
        /// <returns></returns>
        public static bool SavePush(Guid pushButtonId)
        {
            // A quel compte client le bouton est associé ?
            if (Customers == null || Customers.Count == 0)
            {
                return false;
            }

            int customer = FindCustomerByPushButton(pushButtonId);
            if (customer < 0)
            {
                return false;
            }

            AllPushings.Add(
                new PushingItem
                {
                    Date = DateTime.Now,
                    CustomerId = customer,
                    PushButtonId = pushButtonId,
                });

            return true;
        }

        /// <summary>
        /// Finds the customer by push button.
        /// </summary>
        /// <param name="pushButtonId">The push button identifier.</param>
        /// <returns></returns>
        private static int FindCustomerByPushButton(Guid pushButtonId)
        {
            foreach (var customer in Customers)
            {
                if (customer.Value != null && customer.Value.Exists(p => p.Id == pushButtonId))
                {
                    return customer.Key;
                }
            }

            return -1;
        }
    }
}