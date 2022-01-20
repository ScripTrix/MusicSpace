var headerBurger = document.getElementsByClassName('header__burger');
var headerMenu = document.getElementsByClassName('header__menu');
var currentSelect = null;

headerBurger[0].addEventListener("click", () => {
    headerMenu[0].classList.toggle('active');
    headerBurger[0].classList.toggle('active');
    document.body.classList.toggle('lock');
});

// ===================================================================================

var songs = document.getElementsByClassName('song');
var duration = document.getElementById('duration').firstElementChild;
var durations = document.getElementsByClassName('song__duration');
var currentTime = document.getElementById('currentTime').firstElementChild;
var songTitle = document.getElementById('song__title').firstElementChild;
var songsTitle = document.getElementsByClassName('song__title');
var players = [];

var slider = document.getElementById("slider").firstElementChild;
slider.addEventListener("click", () => {
    if (currentSelect != null)
        players[currentSelect].currentTime = (players[currentSelect].duration / 100) * slider.value;
    else
        slider.value = 0;
});

for (let i = 0; i < songs.length; i++) {
    var singlePlayer = {};
    singlePlayer = songs[i].firstElementChild;
    singlePlayer.addEventListener("timeupdate", () => {
        slider.value = (100 / players[currentSelect].duration) * players[currentSelect].currentTime;
        currentTime.innerHTML = getFormated(players[currentSelect].currentTime);
    });
    singlePlayer.addEventListener("ended", () => {
        if (i + 1 < songs.length)
            songs[i + 1].click();
    });
    players.push(singlePlayer);
}

for (let i = 0; i < songs.length; i++) {
    songs[i].addEventListener("click", () => {
        if (currentSelect == null) {
            currentSelect = i;
            songs[i].classList.add("active");
            playPause(i);
        }
        else if (currentSelect == i) {
            playPause(currentSelect);
        }
        else {
            songs[currentSelect].classList.remove("active");
            if (!players[currentSelect].paused)
            playPause(currentSelect);
            players[currentSelect].currentTime = 0;
            songs[i].classList.add("active");
            playPause(i);
            currentSelect = i;
        }
        songTitle.innerHTML = songsTitle[currentSelect].firstElementChild.innerHTML;
        duration.innerHTML = getFormated(players[currentSelect].duration);
        UpdateSongListensCount(players[currentSelect].id);
    });
}

function getFormated(value) {
    let minutes = value / 60;
    let seconds = value % 60;
    minutes = Math.floor(minutes);
    seconds = Math.floor(seconds);
    if (minutes < 10) { minutes = "0" + minutes };
    if (seconds < 10) { seconds = "0" + seconds };
    return `${minutes}:${seconds}`;
}

function playPause(i) {
    if (players[i].paused) {
        players[i].play();
    }
    else {
        players[i].pause();
    }
}

function UpdateSongListensCount(id) {
/// todo ///
}