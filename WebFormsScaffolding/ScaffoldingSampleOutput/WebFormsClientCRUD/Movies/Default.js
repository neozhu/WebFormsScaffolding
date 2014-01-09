/// <reference path="~/Scripts/jquery-2.0.3.min.js" />
/// <reference path="~/Scripts/knockout-3.0.0.js" />
/// <reference path="Scaffolding.WebForms.js" />

function Movie() {
    this.Title = ko.observable('');
    this.Director = ko.observable('');
    this.TicketPrice = ko.observable(0);

    this.reset = function () {
        this.Title('');
        this.Director('');
        this.TicketPrice(0);
    };
}

var defaultViewModel = {

    /* List */
    movies: ko.observableArray(),
    
    getMovies: function () {
        var self = this;
        Scaffolding.WebForms.invokeAction('/Api/Movies').done(function (movies) {
            self.movies(movies);
        });
    },

    /* Create */
    createModal: ko.observable(false),

    movieToCreate: new Movie(),

    movieToCreateValidationErrors: ko.observableArray(),

    showCreateModal: function() {
        this.movieToCreate.reset();
        this.movieToCreateValidationErrors([]);
        this.createModal(true);
    },

    hideCreateModal: function() {
        this.createModal(false);
    },

    create: function () {
        var self = this;
        var data = ko.toJSON(this.movieToCreate);
        
        Scaffolding.WebForms.invokeAction('/Api/Movies', 'POST', data).done(function(movieCreated) {
            // success!
            self.hideCreateModal();
            self.getMovies();
        }).fail(function (errors) {
            self.movieToCreateValidationErrors(errors);
        });
    }
};

