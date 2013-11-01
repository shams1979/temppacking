temppacking = function() {
    var init = function(options) {
        this.options = options;
        var thisObj = this;
        

        var elements = {
            globeContainer: $("#globe"),
            optionsContainer: $("#tripOptions"),
            skill: $("#skill"),
            countries: $("#countries")
        };
        var viewModel = {
            skills: ko.observableArray([{name: "GB"}]),
            addSkill: function() {
                thisObj.viewModel.skills.push({ name: thisObj.elements.skill.val() });
                thisObj.elements.skill.val("");
            },
            removeSkill: function() {
                thisObj.viewModel.skills.remove(this);
            }
            
        };


        this.elements = elements;
        this.viewModel = viewModel;

        ko.applyBindings(thisObj.viewModel, $("#body")[0]);
        
        initCountryLoad(thisObj);

    };

    var initCountryLoad = function (thisObj) {
        //http://tosbourn.com/2013/08/javascript/upgrading-from-bootstraps-typeahead-to-typeahead-js/
        //http://twitter.github.io/typeahead.js/examples/
        thisObj.elements.countries.typeahead({
            //source: function (query, process) {
            //    return $.get('/home/FindSkill', { query: query }, function (data) {
            //        return process(data.options);
            //    });
            //}
            name: 'countries',
            prefetch: '../data/countries.json',                                         
            limit: 10
        });

        thisObj.elements.countries.bind('typeahead:selected', function(obj, datum) {
            alert(JSON.stringify(obj)); // object
        });
    };
    
    return {
        init: init
    };

}();
