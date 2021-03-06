function updateGameAvailability(gameId) {
    var pokemon = [];
    $('.gameAvailability input').each(function () {
        if ($(this).is(':checked')) {
            pokemon.push(this.value);
        }
    });

    $.ajax({
        url: '/update-game-availability/',
        method: "POST",
        data: { "gameId": gameId, "pokemonList": pokemon }
    })
        .done(function (data) {
            window.location = data;
        })
        .fail(function () {
            alert("Update Failed!");
        });
}