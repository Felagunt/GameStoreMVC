using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Moq;
using GameStore.WebUI.Models;
using GameStore.WebUI.HtmlHelpers;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;

namespace GameStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game1" },
                new Game { GameId = 2, Name = "Game2" },
                new Game { GameId = 3, Name = "Game3" },
                new Game { GameId = 4, Name = "Game4" },
                new Game { GameId = 5, Name = "Game5" }
            });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            //act
            GamesListViewModel result = (GamesListViewModel)controller.List(null, 2).Model;

            //assert
            List<Game> games = result.Games.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].Name, "Game4");
            Assert.AreEqual(games[2].Name, "Game5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            //neccesary for using ext method
            HtmlHelper myHelper = null;

            //creating object PagingIngo
            PageInfo pagingInfo = new PageInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            //act
            //wtf with versions
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);
            //
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                    + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                    + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                    result.ToString());
        }

        [TestMethod]
        public void Can_Filter_Games()
        {
            //arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Game1", Category = "Cat1" },
                new Game { GameId = 2, Name = "Game2", Category = "Cat2" },
                new Game { GameId = 3, Name = "Game3", Category = "Cat3" },
                new Game { GameId = 4, Name = "Game4", Category = "Cat4" },
                new Game { GameId = 5, Name = "Game5", Category = "Cat5" }
            });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            //act
            List<Game> result = ((GamesListViewModel)controller.List("Cat2", 1).Model)
                .Games.ToList();

            //assert
            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Game4" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "Game5" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Can_Crete_Categories()
        {
            //fake repo
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game {GameId = 1, Name = "Game1", Category = "RPG" },
                new Game {GameId = 2, Name = "Game3", Category = "Шутер" },
                new Game {GameId = 2, Name = "Game4", Category = "Симулятор" },
                new Game {GameId = 2, Name = "Game5", Category = "Симулятор" }
            });

            //
            NavController target = new NavController(mock.Object);

            //
            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToString();

            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "RPG");
            Assert.AreEqual(results[1], "Симулятор");
            Assert.AreEqual(results[2], "Шутер");
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new Game[]
            {
                new Game { GameId = 1, Name = "Game1", Category = "Симулятор" },
                new Game { GameId = 2, Name = "Game2", Category = "Шутер" }
            });

            NavController target = new NavController(mock.Object);

            string categoryToSelet = "Шутер";

            string result = target.Menu(categoryToSelet).ViewBag.SelectedCategory;

            Assert.AreEqual(categoryToSelet, result);
        }

        [TestMethod]
        public void Generete_Category_Specific_Game_Count()
        {
            //arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game {GameId = 1, Name = "Game1", Category="Cat1" },
                new Game {GameId = 1, Name = "Game2", Category="Cat2" },
                new Game {GameId = 1, Name = "Game3", Category="Cat3" },
                new Game {GameId = 1, Name = "Game4", Category="Cat4" },
                new Game {GameId = 1, Name = "Game5", Category="Cat5" }
            });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            int res1 = ((GamesListViewModel)controller.List("Cat1").Model).PageInfo.TotalItems;
            int res2 = ((GamesListViewModel)controller.List("Cat2").Model).PageInfo.TotalItems;
            int res3 = ((GamesListViewModel)controller.List("Cat3").Model).PageInfo.TotalItems;
            int resAll = ((GamesListViewModel)controller.List(null).Model).PageInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}
