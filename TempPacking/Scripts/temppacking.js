temppacking = function () {
    var init = function(options) {
        this.options = options;
        var thisObj = this;
        

        var elements = {
            globeContainer: $("#globe"),
            optionsContainer: $("#tripOptions"),

            countries: $("#country"),
            skill: $('#skill'),
            destination: $("#country")
        };
        var viewModel = {
            skills: ko.observableArray([]),
            destinations: ko.observableArray([]),
            addSkill: function() {
                thisObj.viewModel.skills.push({ name: thisObj.elements.skill.val() });
                thisObj.elements.skill.val("");
            },
            removeSkill: function() {
                thisObj.viewModel.skills.remove(this);
            },
            addDestination: function () {
                thisObj.viewModel.destinations.push({ name: thisObj.elements.destination.val(), from: '', to: '' });
                thisObj.elements.destination.val("");
            },
            removeDestination: function () {
                thisObj.viewModel.destinations.remove(this);
            }

            
        };


        this.elements = elements;
        this.viewModel = viewModel;

        ko.applyBindings(thisObj.viewModel, $("#body")[0]);
        
        initCountryLoad(thisObj);
        initSkillsLoad(thisObj);

    };

    var initCountryLoad = function (thisObj) {
        ////http://tosbourn.com/2013/08/javascript/upgrading-from-bootstraps-typeahead-to-typeahead-js/
        ////http://twitter.github.io/typeahead.js/examples/

        //thisObj.elements.countries.typeahead([{
        //    remote: "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=%QUERY&sensor=false&types=(cities)&language=en_UK&key=AIzaSyDftozbK86qlycMZzBFBuZ99fP10hV-edY",
        //    limit: 5
        //}]);

        //thisObj.elements.countries.bind('typeahead:selected', function (obj, datum) {
        //    value(datum.value);
        //});

        var autocomplete = new google.maps.places.Autocomplete(document.getElementById('country'));
        google.maps.event.addListener(autocomplete, 'place_changed', function () {
            console.log(thisObj.elements.countries.val());
        });

      
    };
    
    var initSkillsLoad = function (thisObj) {
       
        thisObj.elements.skill.typeahead([{
            remote: "/home/findskill?query=%QUERY",
            limit: 5
        }]);

        thisObj.elements.skill.bind('typeahead:selected', function (obj, datum) {
            thisObj.elements.skill.val(datum.value);
        });
        


    };
    
    return {
        init: init
    };

}();
