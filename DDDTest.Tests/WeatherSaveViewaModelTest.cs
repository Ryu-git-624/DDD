﻿using DDD.Domain.Entities;
using DDD.Domain.Repositories;
using DDD.Infrastructure.Repositories;
using DDD.WinForm.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace DDDTest.Tests
{
    [TestClass]
    public class WeatherSaveViewaModelTest
    {
        [TestMethod]
        public void 天気登録シナリオ()
        {
            var weatherMock = new Mock<IWeatherRepository>();
            var areasMock = new Mock<IAreasRepository>();

            var areas = new List<AreaEntity>();
            areas.Add(new AreaEntity(1, "東京"));
            areas.Add(new AreaEntity(2, "神戸"));
            areasMock.Setup(x => x.GetData()).Returns(areas);

            var viewModelMock = new Mock<WeatherSaveViewModel>(weatherMock.Object,areasMock.Object);
            viewModelMock.Setup(x => x.GetDateTime()).Returns(Convert.ToDateTime("2022/12/18 16:27:00"));

            var viewModel = viewModelMock.Object;
            viewModel.SelectedAreaId.IsNull();
            viewModel.DataDateValue.Is(Convert.ToDateTime("2022/12/18 16:27:00"));
            viewModel.SelectedCondition.Is(1);
            viewModel.TemperatureText.Is("");
            viewModel.TemperatureUnitName.Is("℃");

            viewModel.Areas.Count.Is(2);
            viewModel.Conditions.Count.Is(4);

            var ex = AssertEx.Throws<InputException>(() => viewModel.Save());
            ex.Message.Is("エリアを選択してください");

            viewModel.SelectedAreaId = 2;
            ex = AssertEx.Throws<InputException>(() => viewModel.Save());
            ex.Message.Is("温度の入力に誤りがあります");

            viewModel.TemperatureText = "12.345";

            weatherMock.Setup(x => x.Save(It.IsAny<WeatherEntity>())).Callback<WeatherEntity>(saveValue =>
            {
                saveValue.AreaId.Value.Is(2);
                saveValue.DataDate.Is(Convert.ToDateTime("2022/12/18 16:27:00"));
                saveValue.Condition.Value.Is(1);
                saveValue.Temperature.Value.Is(12.345f);

            });
            viewModel.Save();


        }
    }
}
