using System.Collections.Generic;
using System.Linq;

namespace pokerDeck
{
    public class ValidateRanks
    {
        private string winner = null;

        public string ValidateRank(List<Deck> playerB, List<Deck> playerW)
        {
            StraightFlush(playerB, playerW);
            FourOfAKind(playerB, playerW);
            FullHouse(playerB, playerW);
            Flush(playerB, playerW);
            Straight(playerB, playerW);
            ThreeOfAKind(playerB, playerW);
            TwoPairs(playerB, playerW);
            Pair(playerB, playerW);

            if (string.IsNullOrEmpty(winner))
                HighCard(playerB, playerW);

            return winner;
        }

        /// <summary>
        /// When no combination is generated, the highest card is analyzed.
        /// </summary>
        /// <param name="playerB"></param>
        /// <param name="playerW"></param>
        /// <returns></returns>
        public ResultadoEnum HighCard(List<Deck> playerB, List<Deck> playerW)
        {
            var playerBHV = playerB.OrderByDescending(x => x.HighestValue).FirstOrDefault();
            var playerWHV = playerW.OrderByDescending(x => x.HighestValue).FirstOrDefault();

            if (playerBHV.HighestValue == playerWHV.HighestValue)
            {
                winner = ResultadoEnum.Tie.ToString();
                return ResultadoEnum.Tie;
            }
            else if (playerBHV.HighestValue > playerWHV.HighestValue)
            {
                winner = ResultadoEnum.Black.ToString();
                return ResultadoEnum.Black;
            }
            else if (playerBHV.HighestValue < playerWHV.HighestValue)
            {
                winner = ResultadoEnum.White.ToString();
                return ResultadoEnum.White;
            }

            return ResultadoEnum.None;
        }

        /// <summary>
        /// When the player has 2 of the same cards of different suits.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public ResultadoEnum Pair(List<Deck> playerB, List<Deck> playerW)
        {
            var isPlayerB = playerB.GroupBy(x => x.Card).Any(e => e.Count() == 2);
            var isPlayerW = playerW.GroupBy(x => x.Card).Any(e => e.Count() == 2);

            if (isPlayerB && isPlayerW)
            {
                winner = ResultadoEnum.Tie.ToString();
                return ResultadoEnum.Tie;
            }
            if (isPlayerB)
            {
                winner = ResultadoEnum.Black.ToString();
                return ResultadoEnum.Black;
            }
            if (isPlayerW)
            {
                winner = ResultadoEnum.White.ToString();
                return ResultadoEnum.White;
            }

            return ResultadoEnum.None;
        }

        /// <summary>
        /// When the player has 4 of the same cards of different suits.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public ResultadoEnum TwoPairs(List<Deck> playerB, List<Deck> playerW)
        {
            var isPlayerB = playerB.GroupBy(x => x.Card).Any(e => e.Count() == 4);
            var isPlayerW = playerW.GroupBy(x => x.Card).Any(e => e.Count() == 4);

            if (isPlayerB && isPlayerW)
            {
                winner = ResultadoEnum.Tie.ToString();
                return ResultadoEnum.Tie;
            }
            if (isPlayerB)
            {
                winner = ResultadoEnum.Black.ToString();
                return ResultadoEnum.Black;
            }
            if (isPlayerW)
            {
                winner = ResultadoEnum.White.ToString();
                return ResultadoEnum.White;
            }

            return ResultadoEnum.None;
        }

        /// <summary>
        /// When the player has 3 of the same cards of different suits.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public ResultadoEnum ThreeOfAKind(List<Deck> playerB, List<Deck> playerW)
        {
            var isPlayerB = playerB.GroupBy(x => x.Card).Any(e => e.Count() == 3);
            var isPlayerW = playerW.GroupBy(x => x.Card).Any(e => e.Count() == 3);

            if (isPlayerB && isPlayerW)
            {
                winner = ResultadoEnum.Tie.ToString();
                return ResultadoEnum.Tie;
            }
            if (isPlayerB)
            {
                winner = ResultadoEnum.Black.ToString();
                return ResultadoEnum.Black;
            }
            if (isPlayerW)
            {
                winner = ResultadoEnum.White.ToString();
                return ResultadoEnum.White;
            }

            return ResultadoEnum.None;
        }

        /// <summary>
        /// When all cards are in a consecutive order.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public ResultadoEnum Straight(List<Deck> playerB, List<Deck> playerW)
        {
            var playerBOrder = playerB.OrderBy(x => x.HighestValue).ToList();
            var playerWOrder = playerW.OrderBy(x => x.HighestValue).ToList();
            bool isValidB = true, isValidW = true;

            if (playerBOrder[0].HighestValue != playerBOrder[1].HighestValue - 1)
                isValidB = false;
            if (playerBOrder[1].HighestValue != playerBOrder[2].HighestValue - 1)
                isValidB = false;
            if (playerBOrder[2].HighestValue != playerBOrder[3].HighestValue - 1)
                isValidB = false;
            if (playerBOrder[3].HighestValue != playerBOrder[4].HighestValue - 1)
                isValidB = false;

            if (playerWOrder[0].HighestValue != playerWOrder[1].HighestValue - 1)
                isValidW = false;
            if (playerWOrder[1].HighestValue != playerWOrder[2].HighestValue - 1)
                isValidW = false;
            if (playerWOrder[2].HighestValue != playerWOrder[3].HighestValue - 1)
                isValidW = false;
            if (playerWOrder[3].HighestValue != playerWOrder[4].HighestValue - 1)
                isValidW = false;

            if (isValidB && isValidB)
            {
                winner = ResultadoEnum.Tie.ToString();
                return ResultadoEnum.Tie;
            }
            if (isValidB)
            {
                winner = ResultadoEnum.Black.ToString();
                return ResultadoEnum.Black;
            }
            if (isValidB)
            {
                winner = ResultadoEnum.White.ToString();
                return ResultadoEnum.White;
            }

            return ResultadoEnum.None;
        }

        /// <summary>
        /// When the player has a hand with 5 cards of the same suit
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public ResultadoEnum Flush(List<Deck> playerB, List<Deck> playerW)
        {
            var isPlayerB = playerB.GroupBy(x => x.Suit).Any(e => e.Count() == 5);
            var isPlayerW = playerW.GroupBy(x => x.Suit).Any(e => e.Count() == 5);

            if (isPlayerB && isPlayerW)
            {
                winner = ResultadoEnum.Tie.ToString();
                return ResultadoEnum.Tie;
            }
            if (isPlayerB)
            {
                winner = ResultadoEnum.Black.ToString();
                return ResultadoEnum.Black;
            }
            if (isPlayerW)
            {
                winner = ResultadoEnum.White.ToString();
                return ResultadoEnum.White;
            }

            return ResultadoEnum.None;
        }

        /// <summary>
        /// When Three Of A Kind and Pair conditions are fulfilled.
        /// </summary>
        /// <param name="playerB"></param>
        /// <param name="playerW"></param>
        /// <returns></returns>
        public ResultadoEnum FullHouse(List<Deck> playerB, List<Deck> playerW)
        {
            var threeOfAKind = ThreeOfAKind(playerB, playerW);
            var pair = Pair(playerB, playerW);

            if (threeOfAKind == ResultadoEnum.Tie && pair == ResultadoEnum.Tie)
            {
                winner = ResultadoEnum.Tie.ToString();
                return ResultadoEnum.Tie;
            }

            if (threeOfAKind == ResultadoEnum.Black && pair == ResultadoEnum.Black)
            {
                winner = ResultadoEnum.Black.ToString();
                return ResultadoEnum.Black;
            }

            if (threeOfAKind == ResultadoEnum.White && pair == ResultadoEnum.White)
            {
                winner = ResultadoEnum.White.ToString();
                return ResultadoEnum.White;
            }

            return ResultadoEnum.None;
        }

        /// <summary>
        /// There are 4 cards with a higher value than the opponent's 4 cards.
        /// </summary>
        /// <param name="playerB"></param>
        /// <param name="playerW"></param>
        /// <returns></returns>
        public ResultadoEnum FourOfAKind(List<Deck> playerB, List<Deck> playerW)
        {
            var playerBHV = playerB.Select(x => x.HighestValue).ToList();
            var playerWHV = playerW.Select(x => x.HighestValue).ToList();
            var IsvalidB = TwoPairs(playerB, playerW);

            if (IsvalidB == ResultadoEnum.Tie && playerBHV.Sum() == playerWHV.Sum())
            {
                winner = ResultadoEnum.Tie.ToString();
                return ResultadoEnum.Tie;
            }
            else if (IsvalidB == ResultadoEnum.Black && playerBHV.Sum() > playerWHV.Sum())
            {
                winner = ResultadoEnum.Black.ToString();
                return ResultadoEnum.Black;
            }
            else if (IsvalidB == ResultadoEnum.White && playerBHV.Sum() < playerWHV.Sum())
            {
                winner = ResultadoEnum.White.ToString();
                return ResultadoEnum.White;
            }

            return ResultadoEnum.None;
        }

        /// <summary>
        /// When Straight and Flush conditions are fulfilled
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public ResultadoEnum StraightFlush(List<Deck> playerB, List<Deck> playerW)
        {
            var straight = Straight(playerB, playerW);
            var flush = Flush(playerB, playerW);

            if (straight == ResultadoEnum.Tie && flush == ResultadoEnum.Tie)
            {
                winner = ResultadoEnum.Tie.ToString();
                return ResultadoEnum.Tie;
            }

            if (straight == ResultadoEnum.Black && flush == ResultadoEnum.Black)
            {
                winner = ResultadoEnum.Black.ToString();
                return ResultadoEnum.Black;
            }

            if (straight == ResultadoEnum.White && flush == ResultadoEnum.White)
            {
                winner = ResultadoEnum.White.ToString();
                return ResultadoEnum.White;
            }

            return ResultadoEnum.None;
        }
    }
}
