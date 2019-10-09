$('#PokemonId').on('change', function(){
    $.ajax({
        url: '/get-pokemon-abilities/',
        method: "POST",
        data: { 'pokemonId': $(this).val() }
    })
    .done(function(data) {
        $('#AbilityId').empty();
        $.each(data, function(index, item) {
            $('#AbilityId').append($('<option>').val(item.id).text(item.name));
        });
    })
    .fail(function() {
        alert("Failed to grab abilities!");
    });
});