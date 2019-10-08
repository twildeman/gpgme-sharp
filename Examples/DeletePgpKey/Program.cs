using Libgpgme;
using System;

namespace DeletePgpKey
{
    class Program
    {
        static void Main(string[] args)
        {
            Context ctx = new Context();

            KeyStore store = ctx.KeyStore;

            Key[] publickeys = store.GetKeyList("", false);

            Console.WriteLine("Public PGP keys currently saved in your store:");
            PrintKeys(publickeys);
            
            if (publickeys.Length > 0)
            {
                var keyToDelete = publickeys[publickeys.Length - 1];
                store.DeleteKey(keyToDelete, true, true);
                Console.WriteLine("\nThe last key in the key ring has been deleted");
                PrintKeys(new[] { keyToDelete });
            }
            else
            {
                Console.WriteLine("\nNo keys to delete");
                Console.WriteLine("Please add one via e.g. the CreatePgpKey example");
                Environment.Exit(-1);
            }

            publickeys = store.GetKeyList("", false);
            Console.WriteLine("\nPublic PGP keys currently saved in your store:");
            PrintKeys(publickeys);
        }

        static void PrintKeys(Key[] keys)
        {
            foreach (Key key in keys)
            {
                Console.WriteLine("Key " + key.Fingerprint);
                Console.WriteLine("\tUser: {0}\n", key.Uid);
            }
        }
    }
}
