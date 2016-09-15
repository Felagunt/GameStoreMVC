using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GameStore.Domain.Entities;
using System.Linq;

namespace GameStore.UnitTests
{
    /// <summary>
    /// Summary description for CartTests
    /// </summary>
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            Game game1 = new Game { GameId = 1, Name = "Game1" };
            Game game2 = new Game { GameId = 2, Name = "Game2" };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            List<CartLine> results = cart.Lines.ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Game, game1);
            Assert.AreEqual(results[1].Game, game2);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Game game1 = new Game { GameId = 1, Name = "Game1" };
            Game game2 = new Game { GameId = 2, Name = "Game2" };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Game.GameId).ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            Game game1 = new Game { GameId = 1, Name = "Game1" };
            Game game2 = new Game { GameId = 2, Name = "Game2" };
            Game game3 = new Game { GameId = 3, Name = "Game3" };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 4);
            cart.AddItem(game3, 2);
            cart.AddItem(game2, 1);

            cart.RemoveLine(game2);

            Assert.AreEqual(cart.Lines.Where(c => c.Game == game2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            Game game1 = new Game { GameId = 1, Name = "Game1", Price = 399 };
            Game game2 = new Game { GameId = 2, Name = "Game2", Price = 1569};

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            decimal result = cart.ComputeTotalValue();

            Assert.AreEqual(result, 665);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            Game game1 = new Game { GameId = 1, Name = "Game1", Price = 441 };
            Game game2 = new Game { GameId = 2, Name = "Game2", Price = 331 };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 6);
            cart.Clear();

            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }
}
