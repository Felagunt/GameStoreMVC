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
            GamesListViewModel result = (GamesListViewModel)controller.List(null,2).Model;

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
    }
}
