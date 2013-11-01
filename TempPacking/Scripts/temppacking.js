temppacking = function() {
    var init = function(options) {
        this.options = options;
        var thisObj = this;
        

        var elements = {
            globeContainer: $("#globe"),
            optionsContainer: $("#tripOptions"),
            countries: $("#countries")
        };
        var viewModel = {
            
        };


        this.elements = elements;
        this.viewModel = viewModel;

        initCountryLoad(thisObj);

    };

    var initCountryLoad = function (thisObj) {
        //http://tosbourn.com/2013/08/javascript/upgrading-from-bootstraps-typeahead-to-typeahead-js/
        //http://twitter.github.io/typeahead.js/examples/
        thisObj.elements.countries.typeahead({
            source: function (query, process) {
                return $.get('/home/FindSkill', { query: query }, function (data) {
                    return process(data.options);
                });
            }
            //name: 'countries',
            //prefetch: '../data/countries.json',                                         
            //limit: 10
        });

        thisObj.elements.countries.bind('typeahead:selected', function(obj, datum) {
            alert(JSON.stringify(obj)); // object
        });
    };
    
    return {
        init: init
    };

}();
