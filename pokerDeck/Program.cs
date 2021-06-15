using System;
using System.Collections.Generic;
using System.Linq;

namespace pokerDeck
{
    class Program
    {
        private static readonly List<string> suits, cards;
        private static List<Deck> deck = null;

        static Program()
        {
            suits = new List<string> { "C", "D", "H", "S" };
            cards = new List<string> { "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K", "A" };
            deck = new List<Deck>();

            deck = GenerateDeck();

            Console.WriteLine("Cada jogador pode inserir 5 cartas cada sendo separadas por espaço. ");
            Console.WriteLine("Exemplo: \n Entrada: 2H 3D 5S 9C KD 2C 3H 4S 8C AH \n Saida: White wins \n");
        }

        static void Main(string[] args)
        {
            Poker();
        }


        public static void Poker()
        {
            Console.WriteLine("Insira as cartas: ");
            var input = Console.ReadLine();

            var isValid = ValidateInputDeck(input);
            if (!isValid)
                Console.WriteLine("Uma ou mais cartas inseridas não são validas.");

            if (isValid)
                Console.WriteLine(Winner(input));

            Poker();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardsInput"></param>
        /// <returns></returns>
        public static string Winner(string cardsInput)
        {
            var playerB = cardsInput.Substring(0, cardsInput.Length / 2).Trim().Split(" ").ToList();
            var playerW = cardsInput.Substring(cardsInput.Length / 2).Trim().Split(" ").ToList();

            var playerBCards = deck.Where(x => playerB.Any(y => y == x.Hand)).ToList();
            var playerWCards = deck.Where(x => playerW.Any(y => y == x.Hand)).ToList();

            var result = new ValidateRanks().ValidateRank(playerBCards, playerWCards);

            return result != ResultadoEnum.Tie.ToString() ? result + " wins." : result;
        }

        /// <summary>
        /// Validate if the input cards are real cards
        /// </summary>
        /// <param name="cardsInput"></param>
        /// <returns></returns>
        public static bool ValidateInputDeck(string cardsInput)
        {
            if (string.IsNullOrEmpty(cardsInput))
                return false;

            var listCardInput = cardsInput.Trim().Split(" ").ToList();

            if (listCardInput.Count > 10)
                return false;

            var isValid = deck.Where(x => listCardInput.Any(y => y.ToLower() == x.Hand.ToLower())).ToList();

            if (isValid.Count < 10)
                return false;

            return true;
        }

        /// <summary>
        /// Generate a new deck
        /// </summary>
        /// <returns></returns>
        public static List<Deck> GenerateDeck()
        {
            foreach (var suit in suits)
            {
                foreach (var (card, index) in cards.Select((value, i) => (value, i)))
                {
                    deck.Add(new Deck
                    {
                        HighestValue = index,
                        Suit = suit,
                        Card = card,
                        Hand = string.Concat(card, suit),
                    });
                }
            }

            return deck;
        }
    }
}
