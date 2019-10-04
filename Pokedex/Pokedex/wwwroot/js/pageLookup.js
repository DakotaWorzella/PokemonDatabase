function lookupPokemon(pageName, iconLink, pokemonName) {
  $('.active').each(function() {
    $(this).removeClass('active');
  })

  $('#' + pageName.replace('%', '\\%')).addClass('active');
  $('.tabIcon').attr("href", iconLink);
  $('.pageTitle').text(pokemonName + " | Pokemon Database");
}

function lookupGeneration(generationId) {
  if(!$('.active').is($('#Generation' + generationId)))
  {
    $('button').each(function() {
      $(this).removeClass('active');
    });

    $('.pokemonList').removeClass('active');
    $('.pokemonList > .grid-container').empty();
    $('button#Generation' + generationId).addClass('active');

    $('.grid-container').load('get-pokemon-by-generation/' + generationId, function (){
      $('.pokemonList').addClass('active');
    });
  }
}

function lookupAdminGeneration(generationId) {
  if(!$('.active').is($('#Generation' + generationId)))
  {
    $('button').each(function() {
      $(this).removeClass('active');
    });

    $('.pokemonList').removeClass('active');
    $('.pokemonList > .grid-container').empty();
    $('button#Generation' + generationId).addClass('active');

    $('.grid-container').load('get-pokemon-by-generation-admin/' + generationId, function (){
      $('.pokemonList').addClass('active');
    });
  }
}