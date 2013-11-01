﻿temppacking = function () {
    var init = function(options) {
        this.options = options;
        var thisObj = this;

        this.longitude = 0;
        this.latitude = 0;
        
        var elements = {
            globeContainer: $("#content"),
            optionsContainer: $("#tripOptions"),

            skill: $('#skill'),
            destination: $("#country"),
            intro: $("#intro"),
            results: $("#results"),
            button: $("#go")
        };
        var viewModel = {
            skills: ko.observableArray([]),
            destinations: ko.observableArray([]),
            showResults: ko.observable(false),
            addSkill: function () {
                if (thisObj.elements.skill.val() != "") {
                    thisObj.viewModel.skills.push({ name: thisObj.elements.skill.val() });
                    thisObj.elements.skill.val("");
                } else {
                    alert("Enter a skill!");
                }
            },
            removeSkill: function() {
                thisObj.viewModel.skills.remove(this);
            },
            addDestination: function () {
                if (thisObj.elements.destination.val() != "") {
                thisObj.viewModel.destinations.push({ name: thisObj.elements.destination.val(), from: '', to: '', longitude: thisObj.longitude, latitude: thisObj.latitude, jobs: [] });
                thisObj.elements.destination.val("");
                    // ajax call
                var skillsString = "";
                for (var i = 0; i < thisObj.viewModel.skills().length; i++) {
                    skillsString += thisObj.viewModel.skills()[i].name + ',';
                }
                skillsString = skillsString.substring(0, skillsString.length - 1);

                $.ajax({
                    url: '/home/getjobs',
                    type: 'POST',
                    data: {
                        longitude: thisObj.viewModel.destinations()[0].longitude,
                        latitude: thisObj.viewModel.destinations()[0].latitude,
                        startdate: thisObj.viewModel.destinations()[0].from,
                        enddate: thisObj.viewModel.destinations()[0].to,
                        city: thisObj.viewModel.destinations()[0].name,
                        skills: skillsString
                    },
                    success: function (response) {
                        thisObj.viewModel.destinations()[0].jobs = response.jobs;
                    }

                }
                );
                } else {
                    alert("Enter a destination!");
                }
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

        thisObj.viewModel.showResults.subscribe(function(newValue) {
            if (newValue) {
                thisObj.elements.globeContainer.css("background-image", 'url("/Content/images/bg-02.jpg")');
            } else {
                thisObj.elements.globeContainer.css("background-image", "/Content/images/bg-01.jpg");
            }
        });

        thisObj.elements.button.on("click", function () {
            if (thisObj.viewModel.skills().length < 1 || thisObj.viewModel.destinations().length < 1) {
                alert("Add some skills and give me some destinations!");
            } else {
                thisObj.viewModel.showResults(true);
             }
        });

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
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': thisObj.elements.destination.val() }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    var locInfo = results[0].geometry.location;
                    thisObj.latitude = locInfo.lb;
                    thisObj.longitude = locInfo.mb;
                } else {
                    alert("Geocode was not successful for the following reason: " + status);
                }
            });
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

        thisObj.elements.skill.on("keypress", function(e) {
            if (e.which == 13) {
                thisObj.viewModel.addSkill();
            }
        });

        thisObj.elements.destination.on("keypress", function (e) {
            if (e.which == 13) {
                thisObj.viewModel.addDestination();
            }
        });


    };
    
    return {
        init: init
    };

}();
