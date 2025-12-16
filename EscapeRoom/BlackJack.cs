using NAudio.Dmo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EscapeRoom
{

    class BlackJack
    {
        #region Variables
        private Game game;
        Random rng = new Random();
        private int dealer;
        new Player _player;
        private int player = 0;
        // private int max = 21;
        private bool playerWin;
        private bool draw = false;
        private bool blackJackWin;
        private string[] houses = { "Hearts", "Spades", "Diamonds", "Clubs" };
        private string[] cards = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
        #endregion

        public BlackJack(Game game)
        {
            this.game = game;
        }
        public int PlayBlackJack(int coins)
        {
            player = 0;
            dealer = 0;
            game.PrintWithEffect("Enter your bet amount:\n");
            int bet = 0;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out bet))
                {
                    game.PrintWithEffect("\nPlease enter a Number");
                }
                else if (bet > coins)
                {
                    game.PrintWithEffect("\nYou aren't that rich");
                }
                else if (bet <= 0)
                {
                    game.PrintWithEffect("\nYou cant bet less than 0");
                }
                else if (bet == coins)
                {
                    game.PrintWithEffect("Very brave of you to go all in.");
                    break;
                }
                else break;
                coins -= bet;
            }

            dealer = rng.Next(1, 11);

            game.PrintWithEffect("\nThe cards are being delt\n");
            game.PrintWithEffect("Your first card is:\n");
            player = Hit(player);
            game.PrintWithEffect("\nThe dealers face card is:\n");
            dealer = Hit(dealer);
            game.PrintWithEffect("\nYour second card is:\n");
            player = Hit(player);
            game.PrintWithEffect($"You have {player}\n");
            game.PrintWithEffect("What do you want to do?\n");
            game.PrintWithEffect("1 - Hit");
            game.PrintWithEffect("2 - Stand\n");

            int input;
            bool standing = false;
            while (!standing)
            {
                int.TryParse(Console.ReadLine(), out input);

                switch (input)
                {
                    case 1:
                        player = Hit(player);
                        if (player >= 21)
                        {
                            standing = true;
                        }
                        game.PrintWithEffect($"You have {player}");
                        break;
                    case 2:
                        game.PrintWithEffect("You stand");
                        standing = true;
                        break;
                    default:
                        game.PrintWithEffect("\nInvalid Input");
                        game.PrintWithEffect("Please enter a Number (1 - 2)");
                        break;
                }
            }
            CheckBlackJack();
            CountPlayer();
            dealer = DealerCheck();
            CountDealer();
            if (draw == true)
            {
                coins += 0;
            }
            else if (blackJackWin == true)
            {
                coins += bet * 2;
            }
            else if (playerWin == true)
            {
                coins += bet;
            }
            else coins -= bet;

            return coins;
        }
        private int Hit(int card)
        {
            int hitCard = 0;
            string cardName;
            string cardHouse;
            int randomCard = rng.Next(1, 13);
            int randomHouse = rng.Next(1, 4);
            cardHouse = houses[randomHouse];
            cardName = cards[randomCard];

            switch (cardName)
            {
                case "2":
                    hitCard = 2;
                    break;
                case "3":
                    hitCard = 3;
                    break;
                case "4":
                    hitCard = 4;
                    break;
                case "5":
                    hitCard = 5;
                    break;
                case "6":
                    hitCard = 6;
                    break;
                case "7":
                    hitCard = 7;
                    break;
                case "8":
                    hitCard = 8;
                    break;
                case "9":
                    hitCard = 9;
                    break;
                case "10":
                    hitCard = 10;
                    break;
                case "Jack":
                    hitCard = 10;
                    break;
                case "Queen":
                    hitCard = 10;
                    break;
                case "King":
                    hitCard = 10;
                    break;
                case "Ace":
                    if (card + hitCard > 21)
                    {
                        hitCard = 1;
                    }
                    else
                        hitCard = 11;
                    break;
            }
            card += hitCard;
            game.PrintWithEffect($"{cardName} of {cardHouse}");
            return card;
        }
        public void IntroToBlackJack()
        {
            game.PrintWithEffect("Welcome to BlackJack.\n");
            game.PrintWithEffect("Double your coins to get out, lose them all and you lose all yout lives!!!\n");
            game.PrintWithEffect("Rules:\n");
            game.PrintWithEffect($"BlackJack Pays Triple your Bet");
            game.PrintWithEffect($"Dealer must stand on 17 and must draw to 16");
            game.PrintWithEffect("The game will now begin\n");
        }
        private void CheckBlackJack()
        {
            if (player == 21 && dealer != 21)
            {
                game.PrintWithEffectGreen("BlackJack!!! You Win!!!");
                blackJackWin = true;
            }
        }
        private void CountPlayer()
        {
            bool playerBlackjack = false;
            if (playerBlackjack == false)
            {
                if (blackJackWin == true)
                {
                    playerBlackjack = true;
                }

                else if (player == 21 && dealer != 21)
                {
                    game.PrintWithEffectGreen("BlackJack!!! You Win!!!");

                    blackJackWin = true;
                }
                else if (player > 21)
                {
                    game.PrintWithEffectRed("You Bust!");
                    playerWin = false;
                    return;
                }
                else game.PrintWithEffect($"You have {player}");
            }


        }
        private int DealerCheck()
        {
            game.PrintWithEffect($"Dealer has {dealer}");
            while (dealer < 17)
            {
                game.PrintWithEffect("Dealer Draws");
                dealer = Hit(dealer);
            }
            return dealer;
        }
        private void CountDealer()
        {
            if (dealer > 21 && player <= 21)
            {
                game.PrintWithEffectGreen("Dealer Bust!!!");
                game.PrintWithEffectGreen("You Win!!!");
                playerWin = true;

            }
            else if (dealer == player)
            {
                game.PrintWithEffect("Push... You get your bet back");
                draw = true;

            }
            else if (dealer == 21)
            {
                game.PrintWithEffectRed("Dealer has BlackJack!");
                playerWin = false;

            }
            else if (dealer > player)
            {
                game.PrintWithEffectRed($"Dealer has {dealer}");
                game.PrintWithEffectRed("Dealer Wins!");
                playerWin = false;

            }
            else if (dealer < player && player <= 21)
            {
                game.PrintWithEffectGreen($"Dealer has {dealer}");
                game.PrintWithEffectGreen("You Win!!!");
                playerWin = true;

            }

        }
    }
}