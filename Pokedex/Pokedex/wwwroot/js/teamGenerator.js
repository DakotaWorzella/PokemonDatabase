var altCheck, legendCheck, megaCheck, pokemonList, fillGeneratedTable = function() {
    $('.teamGeneratorTable tbody').remove();
    $('.teamGeneratorTable').append($('<tbody>'));

    if($(window).width() >= 1000)
    {
        for (var i = 0; i < 2; i++) {
            $('.teamGenerator tbody').append($('<tr>'));
        }

        for (var i = 0; i < 3; i++) {
            $('.teamGenerator tr:nth-child(1)').append($('<td>', {
                class: "generatorPicture shadowed pokemon" + (i + 1)
            }));
            $('.teamGenerator tr:nth-child(2)').append($('<td>', {
                class: "generatorPicture shadowed pokemon" + (i + 4)
            }));
        }
    }
    else if($(window).width() >= 768)
    {
        for (var i = 0; i < 3; i++) {
            $('.teamGenerator tbody').append($('<tr>'));
        }

        for (var i = 0; i < 2; i++) {
            $('.teamGenerator tr:nth-child(1)').append($('<td>', {
                class: "generatorPicture shadowed pokemon" + (i + 1)
            }));
            $('.teamGenerator tr:nth-child(2)').append($('<td>', {
                class: "generatorPicture shadowed pokemon" + (i + 3)
            }));
            $('.teamGenerator tr:nth-child(3)').append($('<td>', {
                class: "generatorPicture shadowed pokemon" + (i + 5)
            }));
        }
    }
    else
    {
        for (var i = 0; i < 6; i++) {
            $('.teamGenerator tbody').append($('<tr>'));
        }

        for (var i = 0; i < 6; i++) {
            $('.teamGenerator tr:nth-child(' + (i + 1) + ')').append($('<td>', {
                class: "generatorPicture shadowed pokemon" + (i + 1)
            }));
        }
    }

    for (var i = 0; i < originalNames.length; i++)
    {
        $('.pokemon' + (i + 1)).append('<a href="/' + originalNames[i].name.replace(": ", "_").replace(' ', '_').toLowerCase() + '/" target="_blank"><img title="' + pokemonList[i].name.replace('_', ' ') + ' (Click to learn more)" src="https://www.pokemondatabase.net/images/pokemon/' + pokemonList[i].id + '.png" /></a>');
    }
}, checkLegendaryChecks = function() {
    var boxChecked = false;
    $('.legendaryCheckbox input').each(function() {
        if ($(this).is(':checked'))
        {
            boxChecked = true;
            return false;
        }
    });

    if(!boxChecked)
    {
        if($('.legendaryBoolCheckbox').is(':visible'))
        {
            $(".legendaryBoolCheckbox").hide();
            $("#legendaryBool").prop('checked', false);
        }
    }
    else
    {
        $(".legendaryBoolCheckbox").show();
    }

    return boxChecked;
}, checkAltFormChecks = function() {
    var boxChecked = false;
    $('.alternateFormCheckbox input').each(function() {
        if ($(this).is(':checked'))
        {
            boxChecked = true;
            return false;
        }
    });

    if(!boxChecked)
    {
        if ($('.altFormBoolCheckbox').is(':visible'))
        {
            $(".altFormBoolCheckbox").hide();
            $("#altFormBool").prop('checked', false);
        }
    }
    else
    {
        $(".altFormBoolCheckbox").show();
    }

    return boxChecked;
}, checkMegaCheck = function() {
    var boxChecked = false;
    if ($('#Mega').is(':checked'))
    {
        boxChecked = true;
    }

    if(!boxChecked)
    {
        if ($('.multipleMegaBoolCheckbox').is(':visible'))
        {
            $(".multipleMegaBoolCheckbox").hide();
            $("#multipleMegaBool").prop('checked', false);
        }
    }
    else
    {
        $(".multipleMegaBoolCheckbox").show();
    }

    return boxChecked;
}, checkOtherOptions = function() {
    if (altCheck || legendCheck || megaCheck)
    {
        $('.otherOptions').show();
    }
    else
    {
        $('.otherOptions').hide();
    }
};

$('.generatorDropdown').on('mouseover', function() {
    altCheck = checkAltFormChecks();
    legendCheck = checkLegendaryChecks();
    megaCheck = checkMegaCheck();
    checkOtherOptions();
});

$('.alternateFormCheckbox').on('click', function() {
    altCheck = checkAltFormChecks();
    checkOtherOptions();
});

$('.legendaryCheckbox').on('click', function() {
    legendCheck = checkLegendaryChecks();
    checkOtherOptions();
});

$('.megaCheckbox').on('click', function() {
    megaCheck = checkMegaCheck();
    checkOtherOptions();
});

$(window).on('resize', function() {
    if(
        ($('.teamGenerator tr').length == 2 && $(window).width() < 1000) ||
        ($('.teamGenerator tr').length == 3 && $(window).width() >= 1000) ||
        ($('.teamGenerator tr').length == 3 && $(window).width() < 751) ||
        ($('.teamGenerator tr').length == 6 && $(window).width() >= 751)
    )
    {
        fillGeneratedTable();
    }
});

$('.generatorButton').on('click', function() {
    var selectedGens = [], selectedLegendaries = [], selectedForms = [], selectedEvolutions;
    $('.generationCheckbox input').each(function() {
        if($(this).is(':checked')){
            selectedGens.push(this.value);
        }
    });

    $('.alternateFormCheckbox input').each(function() {
        if($(this).is(':checked')){
            selectedForms.push(this.value);
        }
    });

    $('.legendaryCheckbox input').each(function() {
        if($(this).is(':checked')){
            selectedLegendaries.push(this.value);
        }
    });

    $('.evolutionRadio input').each(function() {
        if($(this).is(':checked')){
            selectedEvolutions = this.value;
        }
    });

    $.ajax({
        url: '/get-pokemon-team/',
        method: 'POST',
        data: { 'selectedGens': selectedGens, 'selectedLegendaries': selectedLegendaries, 'selectedForms': selectedForms, 'selectedEvolutions': selectedEvolutions, 'onlyLegendaries': $("#legendaryBool").is(":checked"), 'onlyAltForms':  $("#altFormBool").is(":checked"), 'multipleMegas':  $("#multipleMegaBool").is(":checked") }
    })
    .done(function(data) {
        pokemonList = data.allPokemonChangedNames;
        originalNames = data.allPokemonOriginalNames;
        fillGeneratedTable();
    })
    .fail( function() {
        alert("Failed To Get Team!");
    });
});