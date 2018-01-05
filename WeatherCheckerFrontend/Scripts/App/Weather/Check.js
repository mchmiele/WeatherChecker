$('#loadingModal').modal({
    show: false,
    backdrop: 'static',
    keyboard: false
});

var WeatherForm = function () {
    var self = this;

    self.Country = ko.observable("United Kingdom");
    self.City = ko.observable("London");
}

var WeatherResult = function () {
    var self = this;

    self.Country = ko.observable("");
    self.City = ko.observable("");
    self.Temperature = ko.observable(0.0);
    self.Humidity = ko.observable(0.0);
};

var ViewModel = {
    WeatherResult: ko.observable(new WeatherResult()),
    WeatherForm: ko.observable(new WeatherForm()),
    MessageWarining: ko.observable(""),

    CheckWeather: function () {
        var self = this;

        if (self.WeatherForm().City() == "" || self.WeatherForm().Country() == "") {
            self.MessageWarining("You must type 'Country' and 'City'");
        } else {
            $('#loadingModal').modal('show');

            setTimeout(function () {
                $.ajax({
                    url: "/Weather/CheckWeather",
                    type: "GET",
                    contentType: "text/plain",
                    data: { country: self.WeatherForm().Country(), city: self.WeatherForm().City() },
                    success: function (data) {
                        if (data != null && data.WeatherResult.IsSucceeded) {
                            self.MessageWarining("");
                            self.WeatherResult().Country(data.WeatherResult.Country);
                            self.WeatherResult().City(data.WeatherResult.City);
                            self.WeatherResult().Temperature(data.WeatherResult.Temperature);
                            self.WeatherResult().Humidity(data.WeatherResult.Humidity);
                            $("#temperatureBar").data("kendoProgressBar").value(data.WeatherResult.Temperature);
                            $("#humidityBar").data("kendoProgressBar").value(data.WeatherResult.Humidity);
                            $('#loadingModal').modal('hide');
                        } else {
                            self.MessageWarining(data.WeatherResult.Message);
                            self.ClearWeatherResult();
                            $('#loadingModal').modal('hide');
                        }

                    },
                    error: function (err) {
                        alert("ERROR");
                    }
                });
            }, 1000);
        }
    },
    ClearWeatherResult: function() {
        var self = this;

        self.WeatherResult().Country(" - ");
        self.WeatherResult().City(" - ");
        self.WeatherResult().Temperature(0.0);
        self.WeatherResult().Humidity(0.0);
        $("#temperatureBar").data("kendoProgressBar").value(0);
        $("#humidityBar").data("kendoProgressBar").value(0);
    },
};

$(document).ready(function () {
    ko.applyBindings(ViewModel);
    initializeKendoPanelbars();
});

function initializeKendoPanelbars() {
    createKendoProgressBar("#temperatureBar",0,50, "value");
    createKendoProgressBar("#humidityBar",0,200, "percent");
}

function createKendoProgressBar(selector,min,max,type) {
    $(selector).kendoProgressBar({
        type: type,
        animation: {
            duration: 600
        },
        max: max,
        min: min
    });
}

function initializeKendoLoadingWindow() {
    $(function () {
        $("#loadingWindow").kendoWindow({
            title: "Kendo UI Window",
            actions: ["refresh"],
            width: 300,
            height: 160
        });
    });
}