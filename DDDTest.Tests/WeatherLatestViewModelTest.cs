using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.Infrastructure.Repositories;
using DDD.WinForm.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;

namespace DDDTest.Tests
{
    [TestClass]
    public class WeatherLatestViewModelTest
    {
        [TestMethod]
        public void シナリオ()
        {
            var weatherMock = new Mock<IWeatherRepository>();
            var areasMock = new Mock<IAreasRepository>();

            var areas = new List<AreaEntity>();
            areas.Add(new AreaEntity(1,"東京"));
            areas.Add(new AreaEntity(2,"神戸"));
            areasMock.Setup(x => x.GetData()).Returns(areas);

            var viewModel = new WeatherLatestViewModel(weatherMock.Object,areasMock.Object);


            Assert.IsNull(viewModel.SelectedAreaId);
            Assert.AreEqual("", viewModel.DataDateText);
            Assert.AreEqual("", viewModel.ConditionText);
            Assert.AreEqual("", viewModel.TemperatureText);
            Assert.AreEqual(2,  viewModel.Areas.Count);

            weatherMock.Setup(x => x.GetLatest(1)).Returns(new WeatherEntity(1, Convert.ToDateTime("2022/12/13 15:00:00"), 2, 12.3f));
            viewModel.SelectedAreaId = 1;
            viewModel.Search(); ;
            Assert.AreEqual(1, viewModel.SelectedAreaId);
            Assert.AreEqual("2022/12/13 15:00:00", viewModel.DataDateText);
            Assert.AreEqual("曇り", viewModel.ConditionText);
            Assert.AreEqual("12.30 ℃", viewModel.TemperatureText);

            weatherMock.Setup(x => x.GetLatest(2)).Returns(new WeatherEntity(2, Convert.ToDateTime("2022/12/17 18:00:00"), 1, 12.378f));
            viewModel.SelectedAreaId = 2;
            viewModel.Search(); ;
            Assert.AreEqual(2, viewModel.SelectedAreaId);
            Assert.AreEqual("2022/12/17 18:00:00", viewModel.DataDateText);
            Assert.AreEqual("晴れ", viewModel.ConditionText);
            Assert.AreEqual("12.38 ℃", viewModel.TemperatureText);

            viewModel.SelectedAreaId = 3;
            viewModel.Search(); ;
            Assert.AreEqual(3, viewModel.SelectedAreaId);
            Assert.AreEqual("", viewModel.DataDateText);
            Assert.AreEqual("", viewModel.ConditionText);
            Assert.AreEqual("", viewModel.TemperatureText);
        }
    }
}
