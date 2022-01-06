using CardsLab.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CardsLab.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(int drawAmount, string shuffleDeck)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://deckofcardsapi.com/api/deck/");

            HttpResponseMessage response;

            if (DeckID.deck_id == null)
            {
                response = await client.GetAsync("new/shuffle/?deck_count=1");
                CardApiResponse deck = await response.Content.ReadAsAsync<CardApiResponse>();
                DeckID.deck_id = deck.deck_id;
                drawAmount = 0;
            }
            if (shuffleDeck == "on")
            {
                response = await client.GetAsync($"{DeckID.deck_id}/shuffle");
                CardApiResponse deck = await response.Content.ReadAsAsync<CardApiResponse>();
                drawAmount = 0;
            }
            response = await client.GetAsync($"{DeckID.deck_id}/draw/?count={drawAmount}");
            CardApiResponse cards = await response.Content.ReadAsAsync<CardApiResponse>();
            return View(cards);
        }

        public async Task<IActionResult> Blackjack(string playing)
        {
            BlackjackGame blackjack = new BlackjackGame();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://deckofcardsapi.com/api/deck/");

            HttpResponseMessage response;

            if (playing == "on")
            {
                BlackjackGame.playerTurn = true;
            }

            if (BlackjackGame.playerTurn && BlackjackGame.player.cards.Count == 0)
            {
                response = await client.GetAsync("new/shuffle/?deck_count=6");
                CardApiResponse deck = await response.Content.ReadAsAsync<CardApiResponse>();
                BlackjackGame.deck_id = deck.deck_id;
                
            }
            return View(blackjack);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
