temppacking = function() {
    var init = function(options) {
        this.options = options;
        var thisObj = this;
        

        var elements = {
            globeContainer: $("#globe"),
            optionsContainer: $("#tripOptions"),
            countries: $("#country"),
            skills: $('#skill')
        };
        var viewModel = {
            
        };


        this.elements = elements;
        this.viewModel = viewModel;

        initCountryLoad(thisObj);
        initSkillsLoad(thisObj);

    };

    var initCountryLoad = function (thisObj) {
        //http://tosbourn.com/2013/08/javascript/upgrading-from-bootstraps-typeahead-to-typeahead-js/
        //http://twitter.github.io/typeahead.js/examples/
        
        thisObj.elements.countries.typeahead([{
            remote: "https://maps.googleapis.com/maps/api/place/autocomplete/json?input=%QUERY&sensor=false&types=(cities)&language=en_UK&key=AIzaSyD3JtusVUU4QaA_hKXRJZh48ovdfAEyYNc",
            limit: 5
        }]);

        thisObj.elements.countries.bind('typeahead:selected', function (obj, datum) {
            value(datum.value);
        });

      
    };
    
    var initSkillsLoad = function (thisObj) {
       
        thisObj.elements.skills.typeahead([{
            remote: "/home/findskill?query=%QUERY",
            limit: 5
        }]);

        thisObj.elements.skills.bind('typeahead:selected', function (obj, datum) {
            value(datum);
        });


    };
    
    return {
        init: init
    };

}();
