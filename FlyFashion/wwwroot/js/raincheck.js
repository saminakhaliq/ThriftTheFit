function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showWeather);
    } else {
        x.innerHTML = "Geolocation is not supported by this browser.";
    }
}


function showWeather(position) {

    var latitude = position.coords.latitude;
    var longitude = position.coords.longitude;
    var apiCall = 'https://api.openweathermap.org/data/2.5/weather?lat=' + latitude + '&lon=' + longitude + '&appid=56ca5f931d46d52125b542392e02a718';
    var tempAPI = 'https://api.openweathermap.org/data/2.5/weather?q=Los Angeles&appid=56ca5f931d46d52125b542392e02a718';

    $.getJSON(apiCall, weatherCallback);
}

function weatherCallback(weatherData) {
    var cityName = weatherData.name;
    var country = weatherData.sys.country;
    var description = weatherData.weather[0].main;
    var details = weatherData.weather[0].description;
    var temperature = Math.floor(weatherData.main.temp - 273.15);
    setMessage(temperature, description, details);
    showIcon(description);
}

function setMessage(temperature, description, details) {
    $("#temperature").html("<h1>" + temperature + "</h1>");
    $("#weather-text").html("<h2>" + details + "</h2>");
    var cold = false;
    var mild = false;
    var hot = false;
    if (temperature <= 8) {
        cold = true;
    } else if (temperature <= 20) {
        mild = true;
    } else {
        hot = true;
    }

    if (cold && description == "Rain") {
        $("#welcome").html("It's cold and rainy today, wear a jacket and some boots.");
    } else if (cold && description == "Clear") {
        $("#welcome").html("It's cold but sunny today, wear a jacket at least :) ");
    } else if (cold && description == "Clouds") {
        $("#welcome").html("It's cold and gloomy today, wear a jacket.");
    } else if (mild && description == "Rain") {
        $("#welcome").html("It's mild and rainy today, wear a raincoat.");
    } else if (mild && description == "Clear") {
        $("#welcome").html("It's mild and sunny, Throw on a sweater!");
    } else if (mild && description == "Clouds") {
        $("#welcome").html("It's mild and cloudy, sweater weather!");
    } else if (hot && description == "Rain") {
        $("#welcome").html("It's warm but rainy today, wear a raincoat.");
    } else if (hot && description == "Clear") {
        $("#welcome").html("Hot and sunny! Throw on some shorts.");
    } else if (hot && description == "Clouds") {
        $("#welcome").html("It's warm but cloudy today, consider a longsleeve");
    } else if (hot && description == "Rain") {
        $("#welcome").html("It's warm, but it's raining. Wear a light coat");
    } else {
        $("#welcome").html("It's snowy! Bundle up!");
    }


}

function showIcon(description) {
    if (description == "Clear") {
        $("#icon").html("<img src='https://i.ibb.co/JtfNgVj/sun.png'>");
    } else if (description == "Clouds") {
        $("#icon").html("<img src='https://i.ibb.co/h7J6sKT/cloudy.png'>");
    } else if (description == "Rain") {
        $("#icon").html("<img src='https://i.ibb.co/LSxhbjq/rainy.png'>");
    } else if (description == "Snow") {
        $("#icon").html("<img src='https://i.ibb.co/FY0Jfwb/snowy.png'>");
    }
}